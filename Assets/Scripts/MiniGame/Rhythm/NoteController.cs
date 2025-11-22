using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    [SerializeField] private GameObject singleNotePrefab;
    [SerializeField] private GameObject longNotePrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float bpm = 75f;
    [SerializeField] private JudgeController judgeController;
    [SerializeField] private FlowerUI flowerUI;
    [SerializeField] private int totalNotes = 8;

    private float beatInterval;
    private int spawnedCount = 0;
    private int activeNotes = 0;


    void Start()
    {
        beatInterval = 60f / bpm;
        StartCoroutine(SpawnNotes());
    }

    IEnumerator SpawnNotes()
    {
        yield return new WaitForSeconds(beatInterval * 4);

        while (spawnedCount < totalNotes)
        {
            Note note;
            bool isLong;
            SpawnNote(out isLong, out note);
            spawnedCount++;

            float waitTime = beatInterval;
            if (isLong)
            {
                LongNote ln = note as LongNote;
                if (ln != null)
                    waitTime += ln.GetrequiredHoldTime();
            }

            yield return new WaitForSeconds(waitTime);
        }

        yield return new WaitUntil(() => activeNotes == 0);
        judgeController.EndGame();
    }

    
    void SpawnNote(out bool isLong, out Note note)
    {
        isLong = Random.value < 0.3f;
        GameObject prefab = isLong ? longNotePrefab : singleNotePrefab;
        GameObject noteObj = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        note = noteObj.GetComponent<Note>();
        if (isLong)
        {
            LongNote ln = note as LongNote;
            if (ln != null)
            {
                ln.SetRequireHoldTime(beatInterval);
            }
        }
        flowerUI.SpawnPot(isLong);

        var renderer = noteObj.GetComponentInChildren<Renderer>();
        renderer.enabled = false;

        note = noteObj.GetComponent<Note>();

        if (note != null)
        {
            activeNotes++;
            judgeController.RegisterNote(note);
            note.OnNoteJudged += (_, _, isFinal) =>
            {
                if (isFinal) activeNotes--;
            };
        }
    }

}

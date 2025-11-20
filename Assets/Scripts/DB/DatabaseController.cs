using System.Collections.Generic;
using System.IO;
using SQLite4Unity3d;
using UnityEngine;

public class DatabaseController : MonoBehaviour
{
    public static DatabaseController Instance { get; private set; }
    private SQLiteConnection _connection;
    private string dbName = "egDB.db";
    private int _characterId;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        string dbPath = GetDatabasePath(dbName);
        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        _characterId = GetOrCreateDefaultCharacter();
    }

    
    private string GetDatabasePath(string dbName)
    {
        string persistentPath = Path.Combine(Application.persistentDataPath, dbName);
        string streamingPath = Path.Combine(Application.streamingAssetsPath, dbName);

    #if UNITY_EDITOR
        bool forceReset = false;

        if (forceReset && File.Exists(persistentPath))
        {
            File.Delete(persistentPath);
        }

        if (!File.Exists(persistentPath))
        {
            File.Copy(streamingPath, persistentPath);
        }

        return persistentPath;
    #else
        if (!File.Exists(persistentPath))
        {
            if (streamingPath.Contains("://") || streamingPath.Contains(":///"))
            {
                var www = UnityEngine.Networking.UnityWebRequest.Get(streamingPath);
                www.SendWebRequest();
                while (!www.isDone) { }
                File.WriteAllBytes(persistentPath, www.downloadHandler.data);
            }
            else
            {
                File.Copy(streamingPath, persistentPath);
            }
        }
        return persistentPath;
    #endif
    }

    private int GetOrCreateDefaultCharacter()
    {
        var character = _connection.Table<Character>().FirstOrDefault();
        if (character != null)
            return character.Id;

        character = new Character { emotion_points = 0 };
        _connection.Insert(character);
        return character.Id;
    }

    // 캐릭터 아이템 조회
    public List<Item> GetMyItems()
    {
        string sql = @"SELECT Item.* FROM Item 
                       INNER JOIN CharacterItem ON Item.item_id = CharacterItem.item_id";
        return _connection.Query<Item>(sql);
    }
    
    // 꽃 아이템 조회
    public List<Item> GetFlowerItems()
    {
        string sql = @"SELECT Item.* FROM Item";
        return _connection.Query<Item>(sql);
    }
    // 특정 아이템 조회
    public Item GetMyItemById(int itemId)
    {
        return GetFlowerItems().Find(item => item.item_id == itemId);
    }

    // 아이템 추가
    public void AddItem(int itemId)
    {
        var exists = _connection.Table<CharacterItem>()
                                .Where(ci => ci.item_id == itemId)
                                .FirstOrDefault();
        if (exists == null)
        {
            _connection.Insert(new CharacterItem { item_id = itemId });
        }
    }

    // 감정 포인트 조회
    public int GetEmotionPoints()
    {
        var character = _connection.Table<Character>().FirstOrDefault();
        return character != null ? character.emotion_points : 0;
    }

    // 감정 포인트 업데이트
    public void UpdateEmotionPoints(int points)
    {
        var character = _connection.Table<Character>().FirstOrDefault();
        if (character != null)
        {
            character.emotion_points = points;
            _connection.Update(character);
        }
    }
    
    // 감정저장소 조회
    public Diary GetDiary(string date)
    {
        return _connection.Table<Diary>().Where(d => d.date == date).FirstOrDefault();
    }


}



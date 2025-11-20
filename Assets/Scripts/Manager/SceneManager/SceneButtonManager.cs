using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneButtonManager : MonoBehaviour
{
    public void LoadScene(string targetScene){
        SceneManager.LoadScene(targetScene);
    }

    public void LoadSceneWithLoading(string targetScene)
    {
        SceneChanger.NextScene = targetScene;
        SceneManager.LoadScene("LoadingScene");
    }
}

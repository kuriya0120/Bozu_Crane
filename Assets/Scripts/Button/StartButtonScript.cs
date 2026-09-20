using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //シーンをゲームに変更
    public void ChangeSceneToGame()
    {
        SceneManager.LoadScene("SceneGame");
    }
}

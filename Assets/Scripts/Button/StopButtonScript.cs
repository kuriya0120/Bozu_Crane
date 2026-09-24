using UnityEngine;
using UnityEngine.SceneManagement;

public class StopButtonScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ボタンが押されたとき呼び出すシーンチェンジ関数
    public void ChangeSceneToTitle()
    {
        SceneManager.LoadScene("SceneTitle");
        Debug.Log("press");
    }
}

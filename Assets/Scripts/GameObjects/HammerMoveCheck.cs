using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


//ハンマーの速度を確認してシーン移行するスクリプト
public class HammerMoveCheck : MonoBehaviour
{
    [SerializeField]
    Object hammer;  //ハンマー本体

    [SerializeField]
    TextMeshProUGUI almost_break_text; //折れそう...テキスト

    public float denger_speed = 30;
    public float gameover_speed = 40;

    private ArticulationBody articulation_body; //ArticulationBody

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //コンポーネント取得
        articulation_body = GetComponent<ArticulationBody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //ArticulationBodyからハンマーの場所のVelocityを取得
        Vector3 velocity = articulation_body.GetPointVelocity(hammer.GameObject().transform.position);

        //速度の大きさがゲームオーバーのスピードを超えていたら
        if(Vector3.Distance(velocity,new Vector3(0,0,0)) > gameover_speed)
        {
            //シーンをゲームオーバーに
            SceneManager.LoadScene("SceneGameOver");
        }
        //速度の大きさが危険な域に達していたら
        if(Vector3.Distance(velocity,new Vector3(0,0,0)) > denger_speed)
        {
            almost_break_text.enabled = true;
        }
        //達していなかったら非表示
        else 
        {
            almost_break_text.enabled = false;
        }
    }
}

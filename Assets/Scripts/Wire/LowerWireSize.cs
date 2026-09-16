using Unity.VisualScripting;
using UnityEngine;

public class LowerWireSize : MonoBehaviour
{
    [SerializeField]
    Object Hook; //フック

    [SerializeField]
    Object Hammer; //ハンマー

    private Vector3 first_scale;    //自身の生成時のスケール
    private float first_z;  //自身の生成時のz座標
    private Transform this_t;   //自身のトランスフォーム
    private Transform hook_t;   //フックのトランスフォーム
    private Transform hammer_t; //ハンマーのトランスフォーム

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //各トランスフォームを取得
        this_t = GetComponent<Transform>();
        hook_t = Hook.GetComponent<Transform>();
        hammer_t = Hammer.GetComponent<Transform>();

        //生成時のスケールを取得
        first_scale = this_t.localScale;

        //生成時のz座標を取得
        first_z = this_t.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //フックのワールド座標を取得
        Vector3 hook_pos = hook_t.position;
        //z成分を除去
        hook_pos.z = 0;

        //ハンマーのワールド座標を取得
        Vector3 hammer_pos = hammer_t.position;
        //z成分を除去
        hook_pos.z = 0;

        //距離を計算
        float rp_distance = Vector3.Distance(hook_pos, hammer_pos);

        //自身のサイズに適用
        this_t.localScale = new Vector3(first_scale.x, rp_distance * 1.8f, 1);

        //フック⇒ハンマーの中心座標を取得
        Vector3 center_pos = Vector3.Lerp(hook_pos, hammer_pos, 0.5f);

        //z座標を更新
        center_pos.z = first_z;

        //計算した座標を自身のワールド座標に設定
        this_t.position = center_pos;
    }
}

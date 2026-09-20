using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BellSwayingScript : MonoBehaviour
{
    [SerializeField]
    Object HitEffectPrefab;

    private const float spring = 5;        //first_posに戻るバネの強さ
    private const float damping = 5.0f;     //動きを減速させる強さ

    private Vector2 first_pos;  //固定する先の座標

    private Rigidbody2D rb; //RigidBody

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //初期位置を保存しておく
        first_pos = new Vector2(transform.position.x, transform.position.y);

        //RigidBody取得
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {



    }

    private void FixedUpdate()
    {
        //バネ+ダンパーの挙動で一定箇所にバネのように固定
        Vector2 displacement = first_pos - rb.position;
        Vector2 force = displacement * spring;
        force -= rb.linearVelocity * damping;

        rb.AddForce(force);
            
        //鐘の角度を初期位置と現在位置のxを参照して変更
        float range = transform.position.x - first_pos.x;

        transform.eulerAngles = new Vector3( 0, 0, range * 10);
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D other)
    {
        //ぶつかったオブジェクトがハンマーだった時
        if(other.gameObject.tag == "Hammer")
        {
            //ハンマーの親のArticulationBodyを取得
            var hammer_pivot_ab = other.GetComponentInParent<ArticulationBody>();

            //ArticulationBodyからハンマーの場所のVelocityを取得
            Vector3 velocity = hammer_pivot_ab.GetPointVelocity(other.transform.position);

            //計算した分のveloityを与える
            rb.AddForce(new Vector2(velocity.x * 10,velocity.y * 10));

            //衝突方向を取得
            Vector3 hit_normal = Vector3.Normalize(velocity);

            //エフェクトを出現させる角度
            float rad_angle = Mathf.Atan2(hit_normal.y, hit_normal.x);

            //ベクトルの大きさを測る
            float velocity_distance = Vector3.Distance(velocity,new Vector3(0,0,0));

            //Degree角に変更
            float degree_angle = Mathf.Rad2Deg * rad_angle;

            //エフェクトを出現させる
            Object effect_obj = Instantiate(HitEffectPrefab,
                transform.position + new Vector3(0,0,1) + hit_normal * (velocity_distance / 5),
                Quaternion.AngleAxis(degree_angle + 90, Vector3.forward));

            //エフェクトのトランスフォーム
            var effect_t = effect_obj.GetComponent<Transform>();

            //エフェクトのサイズを変更
            effect_t.localScale = new Vector3(velocity_distance / 20, velocity_distance / 20, 1);

            //消滅までの時間をサイズによって設定
            Destroy(effect_obj, velocity_distance / 30);

            //カメラを振動させる
            var camera_comp = Camera.main.GetComponent<CameraMove>();
            camera_comp.SetVibrationWithTime(1.0f, 5);

            Debug.Log(velocity.ToString());
        }
    }
}

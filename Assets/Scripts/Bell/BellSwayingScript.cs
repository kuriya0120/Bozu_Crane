using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BellSwayingScript : MonoBehaviour
{
    [SerializeField]
    Object HitEffectPrefab; //出現させるエフェクトのPrefab

    [SerializeField]
    Object HitCountPrefab;   //出現させる煩悩カウントのPrefab

    [SerializeField]
    Object HitTextPrefab;   //出現させる煩悩テキストのPrefab

    [SerializeField]
    Object Canvas;      //テキストを出現させるキャンバスを指定

    [SerializeField]
    Object bonno_count; //煩悩カウント管理オブジェクト

    [SerializeField]
    Object GoriekiOrb_Prefab;   //ご利益タイム時に叩いた時に出るオーブのPrefab

    [SerializeField]
    HammerKindData hammer_kind_data;    //ハンマーの種類のデータ

    private AudioSource audio_source;  //音コンポーネント
    private Rigidbody2D rb; //RigidBody
    private float damage_coefficient = 0.0f;   //ダメージ係数

    public float damage_scale = 1.0f;  //ダメージに掛ける最終補正(難易度調整用)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //RigidBody取得
        rb = GetComponent<Rigidbody2D>();

        //Audio取得
        audio_source = GetComponent<AudioSource>();

        if(hammer_kind_data.kind == Hammer_Kind.IRON_HAMMER)
        {
            damage_coefficient = 1.0f;
        }
        else
        {
            damage_coefficient = 0.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {



    }

    private void FixedUpdate()
    {

    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D other)
    {
        //ぶつかったオブジェクトがハンマーだった時
        if(other.gameObject.tag == "Hammer" && enabled)
        {
            HitHammer(other);
        }
    }

    private void HitHammer(UnityEngine.Collider2D hammer)
    {
        //ハンマーの親のArticulationBodyを取得
        var hammer_pivot_ab = hammer.GetComponentInParent<ArticulationBody>();

        //ArticulationBodyからハンマーの場所のVelocityを取得
        Vector3 velocity = hammer_pivot_ab.GetPointVelocity(hammer.transform.position);

        //計算した分のveloityを与える
        rb.AddForce(new Vector2(velocity.x * 10, velocity.y * 10));

        //衝突方向を取得
        Vector3 hit_normal = Vector3.Normalize(velocity);

        //エフェクトを出現させる角度
        float rad_angle = Mathf.Atan2(hit_normal.y, hit_normal.x);

        //ベクトルの大きさを測る
        float velocity_distance = Vector3.Distance(velocity, new Vector3(0, 0, 0));

        //Degree角に変更
        float degree_angle = Mathf.Rad2Deg * rad_angle;

        //エフェクトを出現させる
        Object effect_obj = Instantiate(HitEffectPrefab,
            transform.position + new Vector3(0, 0, 1) + hit_normal * (velocity_distance / 5),
            Quaternion.AngleAxis(degree_angle + 90, Vector3.forward));

        //エフェクトのトランスフォーム
        var effect_t = effect_obj.GetComponent<Transform>();

        //エフェクトのサイズを変更
        effect_t.localScale = new Vector3(velocity_distance / 20, velocity_distance / 20, 1);

        //消滅までの時間をサイズによって設定
        Destroy(effect_obj, velocity_distance / 30);

        //ダメージに係数をかけて計算
        int sub_count = (int)(velocity_distance * 0.5f * damage_coefficient * damage_scale);

        //煩悩カウントを減少させる
        var bonno_count_script = bonno_count.GetComponent<BonnoCountManager>();
        bonno_count_script.SubtructCount((int)sub_count);

        //減った煩悩カウントを表示する
        var count = Instantiate(HitCountPrefab,
            Canvas.GetComponent<Transform>());

        //ご利益タイムかどうかで場合分け
        if (!bonno_count_script.GetGoriekiTimeFlag())
        {
            //テキストの文字を変更
            var text_comp = count.GetComponentInChildren<TextMeshProUGUI>();
            text_comp.text = "<size=20>" + sub_count.ToString() + "</size><cspace=-0.5em>煩</cspace>悩";
        }
        else
        {
            //テキストの文字を変更
            var text_comp = count.GetComponentInChildren<TextMeshProUGUI>();
            text_comp.text = "<color=#91ea00><size=20>" + sub_count.ToString() + "</size><cspace=-0.5em>ご</cspace>利益</color>";

            //オーブを出現させる
            var orb = Instantiate(GoriekiOrb_Prefab, 
                transform.position + new Vector3(0,0,2), 
                transform.rotation);

            //オーブのサイズを決める
            float scale = Mathf.Clamp((float)sub_count / 20.0f,0.4f,5.0f);
            orb.GetComponent<Transform>().localScale = new Vector3(scale, scale, 1);

            //オーブの初速を決める
            var orb_rb = orb.GetComponent<Rigidbody2D>();
            orb_rb.AddForce(velocity * 3);
        }

        //カウントのローカルサイズを変更
        var count_t = count.GetComponent<RectTransform>();
        float size = Mathf.Clamp((float)sub_count / 2, 0.8f, 10.0f);
        count_t.localScale = new Vector3(size, size, 1);

        //カメラを振動させる
        var camera_comp = Camera.main.GetComponent<CameraMove>();
        camera_comp.SetVibrationWithTime(0.5f + 0.5f * (sub_count / 5), sub_count);

        //音量をダメージによって調整する
        float volume = Mathf.Clamp(sub_count / 15.0f, 0.2f, 1.0f);
        audio_source.volume = volume;

        //音を出す
        audio_source.Play();
    }
}

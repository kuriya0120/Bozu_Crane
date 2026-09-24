using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SceneGameEventManager : MonoBehaviour
{
    private enum GoriekiTimeEventState
    {
        Normal_time,
        Normal_Time_End,
        Slow_Time,
        Gorieki_Time,
        Gorieki_Time_End,
    }
    [SerializeField]
    Object main_camera; //Cameraオブジェクト

    [SerializeField]
    BonnoCountManager bonnoCountManager;    //煩悩カウント管理オブジェクト

    [SerializeField]
    RemainingTimeManager remainingTimeManager;  //残り時間管理オブジェクト

    [SerializeField]
    Object BackGround_Game_Normal;      //通常時の背景

    [SerializeField]
    Object BackGround_Game_Gorieki;     //ご利益タイム時の背景

    [SerializeField]
    Object BackGround_GameOver;         //ゲームオーバー時の背景

    [SerializeField]
    Object HappyNewYear;                //HappyNewYear

    [SerializeField]
    Object SuperGoriekiTimePrefab;      //スーパーご利益タイムテキストのPrefab

    [SerializeField]
    Object RemainingCountObject;        //残り時間オブジェクト群

    [SerializeField]
    Object BonnoBar;                    //煩悩バーUI

    [SerializeField]
    Object RemainingBonnoCount;         //残り煩悩カウントUI

    [SerializeField]
    Object Bell;                        //鐘

    [SerializeField]
    Object ClearUIPrefab;               //クリア時のUIPrefab

    [SerializeField]
    Object GoriekiCountText;            //ご利益カウントのUI

    [SerializeField]
    Transform Canvas_Transform;         //テキストを出すキャンバスのTransform

    public Sprite newBellSprite;    //鐘に適用する新しいスプライト

    private int gorieki_slow_time = 5;      //ご利益タイムに入ったときのスローの時間
    private GoriekiTimeEventState state;    //現在のイベント状態を表すステート

    private AudioSource audio_source;   //音コンポーネント

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio_source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //状態によって更新処理を分ける
        switch (state)
        {
            case GoriekiTimeEventState.Normal_time:
                UpdateNormalTime();
                break;
            case GoriekiTimeEventState.Normal_Time_End:
                UpdateNormalEndTime();
                break;
            case GoriekiTimeEventState.Slow_Time:
                UpdateSlowTime();
                break;
            case GoriekiTimeEventState.Gorieki_Time:
                UpdateGoriekiTime();
                break;
            case GoriekiTimeEventState.Gorieki_Time_End:
                UpdateGoriekiEndTime();
                break;
        }
    }
    //通常時の更新
    private void UpdateNormalTime()
    {
        //ご利益タイムに入ったらステートを変更
        if(bonnoCountManager.GetGoriekiTimeFlag())
        {
            state = GoriekiTimeEventState.Slow_Time;

            //煩悩バーを削除
            Destroy(BonnoBar);

            //残り時間を追加
            remainingTimeManager.AddRemainingTime();

            //音楽を流す
            audio_source.Play();

            //一時的にtimeScaleを調整してスローに
            Time.timeScale = 0.2f;
        }
        if(remainingTimeManager.GetEndTimeFlag())
        {
            state = GoriekiTimeEventState.Normal_Time_End;

            //鐘のテクスチャを変更
            var bell_sprite = Bell.GetComponent<SpriteRenderer>();
            bell_sprite.sprite = newBellSprite;

            //鐘のスクリプトを無効化
            var bell_script = Bell.GetComponent<BellSwayingScript>();
            bell_script.enabled = false;

            //通常の背景を無効化
            BackGround_Game_Normal.GameObject().SetActive(false);

            //ゲームオーバーの背景を有効化
            BackGround_GameOver.GameObject().SetActive(true);

            //HappyNewYearの文字を有効化
            var string_sprite = HappyNewYear.GetComponent<SpriteRenderer>();
            string_sprite.enabled = true;

            //残り煩悩の数を表示
            int remaining_bonno = bonnoCountManager.GetBonnoCount();
            var bonno_count_text = RemainingBonnoCount.GetComponent<TextMeshProUGUI>();
            bonno_count_text.text = "祓えなかった煩悩  <size=40>" + remaining_bonno.ToString() + "</size>";

            //残り時間オブジェクトを削除
            Destroy(RemainingCountObject);

            //カメラの振動をセット
            main_camera.GetComponent<CameraMove>().SetVibrationWithTime(0.5f, 5);
        }
    }

    //残り時間が尽きたときの更新処理
    private void UpdateNormalEndTime()
    {

    }
    //スローの時の更新
    private void UpdateSlowTime()
    {
        gorieki_slow_time--;

        //カメラ背景の色を徐々に白にしていく
        //最初の色をVector3に詰めて、白のカラーに線形補完
        Vector4 now_color = Vector4.Lerp(new Vector4(48, 60, 97,0), new Vector4(100, 100, 100, 1.0f), 1.0f - (gorieki_slow_time / 5.0f));
        Debug.Log(1.0f - (gorieki_slow_time / 5.0f));

        //色を適用
        var camera_comp = main_camera.GetComponent<Camera>();
        camera_comp.backgroundColor = new Color(now_color.x / 255.0f,now_color.y / 255.0f, now_color.z / 255.0f, now_color.w);

        //スロータイムが終わったらご利益タイムステートにする
        if(gorieki_slow_time == 0)
        {
            state = GoriekiTimeEventState.Gorieki_Time;

            //通常の背景を無効化
            BackGround_Game_Normal.GameObject().SetActive(false);

            //ご利益タイムの背景を有効化
            BackGround_Game_Gorieki.GameObject().SetActive(true);

            //ご利益タイムUIを出現する
            Instantiate(SuperGoriekiTimePrefab, Canvas_Transform);

            //timeScaleを戻して通常速度に
            Time.timeScale = 1.5f;
        }
    }
    //ご利益タイムの更新
    private void UpdateGoriekiTime()
    {
        //残り時間が無くなったら
        if (remainingTimeManager.GetEndTimeFlag())
        {
            //ステート変更
            state = GoriekiTimeEventState.Gorieki_Time_End;

            //鐘のテクスチャを変更
            var bell_sprite = Bell.GetComponent<SpriteRenderer>();
            bell_sprite.sprite = newBellSprite;

            //鐘のスクリプトを無効化
            var bell_script = Bell.GetComponent<BellSwayingScript>();
            bell_script.enabled = false;

            //残り時間オブジェクトを削除
            Destroy(RemainingCountObject);

            //クリア時のUIを出現
            var clear_ui_obj = Instantiate(ClearUIPrefab,Canvas_Transform);

            //ご利益カウントを設定
            clear_ui_obj.GetComponentInChildren<GetGoriekiValue>().
                SetGoriekiCount(bonnoCountManager.GetBonnoCount());

            //ご利益カウントのテキストを削除
            Destroy(GoriekiCountText);
        }
    }

    //ご利益タイムが終わったときの更新処理
    private void UpdateGoriekiEndTime()
    {

    }
}

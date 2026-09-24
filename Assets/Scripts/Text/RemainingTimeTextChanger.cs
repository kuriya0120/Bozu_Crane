using System.Drawing;
using TMPro;
using UnityEngine;

public class RemainingTimeTextChanger : MonoBehaviour
{
    [SerializeField]
    RemainingTimeManager time_manager;  //残り時間管理オブジェクト

    private int old_remaining_time = 30; //文字を変えるか判定をするための残り時間保存変数
    private int color_change_time = 0;

    private TextMeshProUGUI text;   //TextMeshProコンポーネント
    private RectTransform rect_t;   //RectTransform

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //コンポーネントを取得
        rect_t = GetComponent<RectTransform>();
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        //残り時間を取得
        int now_remaining_time = time_manager.GetRemainingTimeSecond();

        //1つずらす
        now_remaining_time--;

        if (now_remaining_time != old_remaining_time)
        {
            text.text = now_remaining_time.ToString();

            //残り時間が5秒以下だったら
            if (now_remaining_time <= 5)
            {
                //白に戻すまでの時間を設定
                color_change_time = 25;

                //数字のサイズを計算
                float size = (1.0f + (1.0f - (float)now_remaining_time / 5));

                //色を適用した数字を適用
                text.text = "<color=#FF0000>" + now_remaining_time.ToString() + "</color>";

                //サイズを変更
                rect_t.localScale = new Vector3(size, size, size); 
            }
            else
            {
                //サイズを通常に
                rect_t.localScale = new Vector3(1, 1, 1);

                text.text = now_remaining_time.ToString();
            }
            old_remaining_time = now_remaining_time;
        }

        //色を白に戻すまでの残りフレーム
        if(color_change_time > 0)
        {
            color_change_time--;

            //色を白に戻す
            if(color_change_time == 0)
            {
                text.text = now_remaining_time.ToString();
            }
        }
    }
}

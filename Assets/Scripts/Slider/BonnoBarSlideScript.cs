using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BonnoBarSlideScript : MonoBehaviour
{
    [SerializeField]
    Object bonno_manager;   //煩悩管理オブジェクト

    private int old_bonno_count = 108;    //更新前の煩悩カウント
    private int max_bonno = 108;  //最大煩悩カウント

    private int since_change_width_count = 0;   //widthを変更するまでのカウントダウン
    private int since_change_width_count_max = 60;   //widthを変更するまでのカウントダウン

    private float first_width;  //最初の横幅
    private Vector3 first_pos;  //最初の場所

    private RectTransform rect; //自身のトランスフォーム
    private Slider slider;      //自身のスライダーコンポーネント

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //各コンポーネントを取得
        rect = GetComponent<RectTransform>();
        slider = GetComponent<Slider>();

        //生成時の情報を格納
        first_width = rect.sizeDelta.x;
        first_pos = rect.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        //煩悩カウントを取得する
        var bonno = bonno_manager.GetComponent<BonnoCountManager>();
        int bonno_count = bonno.GetBonnoCount();

        //更新されていたら
        if(bonno_count != old_bonno_count)
        {
            //横幅に変更を加えるまでのカウントダウンを設定
            since_change_width_count = since_change_width_count_max;

            //SlideBarの値を変更する
            slider.value = bonno_count;

            old_bonno_count = bonno_count;
        }

        //最後の更新から一定期間経ったら
        if(since_change_width_count > 0)
        {
            since_change_width_count--;

            //カウントが0になったら
            if(since_change_width_count == 0)
            {
                //最大値を変更する
                slider.maxValue = bonno_count;

                //Widthを修正
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,
                    ((((float)bonno_count) / max_bonno) * first_width));

                //中心点を修正
                rect.position = first_pos - new Vector3(((((float)max_bonno - bonno_count) / max_bonno) * first_width) / 0.75f, 0, 0);

            }
        }
    }
}

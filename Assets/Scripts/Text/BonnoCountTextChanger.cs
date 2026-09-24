using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BonnoCountTextChanger : MonoBehaviour
{

    [SerializeField]
    Object bonno_manager;   //煩悩管理オブジェクト

    private int old_bonno = 108;  //変更を掛けるか判定する一つ前のカウント

    private TextMeshProUGUI text;   //テキストコンポーネント

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //煩悩カウントを取得
        var bonno_manager_script = bonno_manager.GetComponent<BonnoCountManager>();
        int bonno_count = bonno_manager_script.GetBonnoCount();

        //前フレームと異なっていたらテキストを更新
        if(old_bonno != bonno_count)
        {
            //ご利益タイムじゃないとき
            if (!bonno_manager_script.GetGoriekiTimeFlag())
            {
                //煩悩カウントを文字列に変換してテキストに
                text.text = bonno_count.ToString();

                old_bonno = bonno_count;
            }
            //ご利益タイムのとき
            else
            {
                //煩悩カウントを絶対値にしたのち文字列に変換してテキストに
                int abs_bonno = Mathf.Abs(bonno_count);

                //黄緑にして表示
                text.text = "<color=#91ea00>" + abs_bonno.ToString() + "</color>";

                old_bonno = bonno_count;
            }
        }
    }
}

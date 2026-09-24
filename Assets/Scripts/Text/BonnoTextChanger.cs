using TMPro;
using UnityEngine;

public class BonnoTextChanger : MonoBehaviour
{
    [SerializeField]
    BonnoCountManager count_manager;    //煩悩カウント参照先

    private bool changed_text = false;  //処理数を減らすためのテキスト変更済フラグ

    private TextMeshProUGUI textMeshPro;    //TextMeshProコンポーネント

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (changed_text) return;

        //最初のご利益タイムに入ったときに
        if(count_manager.GetGoriekiTimeFlag())
        {
            //テキストを黄緑のご利益の文字に変更
            textMeshPro.text = "<color=#91ea00>ご利益</color>";

            //次以降入らないようフラグを更新
            changed_text = true;
        }
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class ChangeHammerUI_Iron : MonoBehaviour
{
    [SerializeField]
    Object hammer_object;   //ハンマーのオブジェクト

    [SerializeField]
    Object ChangeHammerUI_Wood; //ハンマーチェンジUIの木のほう

    [SerializeField]
    HammerKindData hammer_kind_data;    //ハンマーの種類情報

    public Sprite Hammer_Sprite;    //切り替えるハンマーのスプライト
    public Sprite ChooseUI_Sprite;  //自身の選ばれたスプライト
    public Sprite NotChooseUI_Sprite;  //相方の選ばれなかったスプライト

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ロード時の情報によって切り替える
        if (hammer_kind_data.kind == Hammer_Kind.IRON_HAMMER)
        {
            //ハンマーのスプライトを切り替える
            var hammer_sprite = hammer_object.GetComponent<SpriteRenderer>();
            hammer_sprite.sprite = Hammer_Sprite;

            //自分のスプライトを切り替える
            var this_sprite = GetComponent<SpriteRenderer>();
            this_sprite.sprite = ChooseUI_Sprite;

            //相方のスプライトを切り替える
            var partner_sprite = ChangeHammerUI_Wood.GetComponent<SpriteRenderer>();
            partner_sprite.sprite = NotChooseUI_Sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    //クリックされたとき
    public void OnClick()
    {
        //ハンマーのスプライトを切り替える
        var hammer_sprite = hammer_object.GetComponent<SpriteRenderer>();
        hammer_sprite.sprite = Hammer_Sprite;

        //自分のスプライトを切り替える
        var this_sprite = GetComponent<SpriteRenderer>();
        this_sprite.sprite = ChooseUI_Sprite;

        //相方のスプライトを切り替える
        var partner_sprite = ChangeHammerUI_Wood.GetComponent<SpriteRenderer>();
        partner_sprite.sprite = NotChooseUI_Sprite;

        //種類を変更
        hammer_kind_data.kind = Hammer_Kind.IRON_HAMMER;
    }
}

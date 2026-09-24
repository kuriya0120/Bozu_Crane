using UnityEngine;

public class HammerDataReceiver : MonoBehaviour
{
    [SerializeField]
    ClaneAngleData clane_angle_data;  //シーン間で受け渡しするデータ

    [SerializeField]
    HammerKindData hammer_kind_data;    //ハンマーの種類のデータ

    public Sprite Hammer_Iron_Sprite;   //鉄ハンマーのスプライト
    public Sprite Hammer_Wood_Sprite;   //木ハンマーのスプライト

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ハンマーの種類によってスプライトを切り替える
        if(hammer_kind_data.kind == Hammer_Kind.IRON_HAMMER)
        {
            var this_sprite = GetComponent<SpriteRenderer>();
            this_sprite.sprite = Hammer_Iron_Sprite;
        }
        else if(hammer_kind_data.kind == Hammer_Kind.WOOD_HAMMER)
        {
            var this_sprite = GetComponent<SpriteRenderer>();
            this_sprite.sprite = Hammer_Wood_Sprite;
        }

        //保存してあるデータを適用する
        transform.localPosition = new Vector3(0, clane_angle_data.Hammer_Position.position_y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

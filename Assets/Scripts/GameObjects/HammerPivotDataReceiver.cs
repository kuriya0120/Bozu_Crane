using UnityEngine;

public class HammerPivotDataReceiver : MonoBehaviour
{
    [SerializeField]
    ClaneAngleData clane_angle_data;  //シーン間で受け渡しするデータ

    [SerializeField]
    HammerKindData hammer_kind_data;    //ハンマーの種類データ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //保存してあるデータを適用する
        transform.localEulerAngles = new Vector3(0, 0, clane_angle_data.Hammer_Position.rotation_z);

        var art_body = GetComponent<ArticulationBody>();
        art_body.enabled = true;

        if (hammer_kind_data.kind == Hammer_Kind.IRON_HAMMER)
        {
            art_body.mass = 0.2f;
        }
        if (hammer_kind_data.kind == Hammer_Kind.WOOD_HAMMER)
        {
            art_body.mass = 0.1f;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

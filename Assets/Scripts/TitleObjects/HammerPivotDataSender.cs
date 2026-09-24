using UnityEngine;

public class HammerPivotDataSender : MonoBehaviour
{
    [SerializeField]
    ClaneAngleData clane_angle_data;  //シーン間で受け渡しするデータ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //保存してあるデータを適用する
        transform.localEulerAngles = new Vector3(0, 0, clane_angle_data.Hammer_Position.rotation_z);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //破棄時
    private void OnDestroy()
    {
        //ローカル角度をシーン間データに保存
        Vector3 angle = GetComponent<Transform>().localEulerAngles;
        clane_angle_data.Hammer_Position.rotation_z = angle.z;
    }
}

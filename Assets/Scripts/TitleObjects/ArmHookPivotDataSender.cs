using UnityEngine;

public class ArmHookPivotDataSender : MonoBehaviour
{
    [SerializeField]
    ClaneAngleData clane_angle_data;  //シーン間で受け渡しするデータ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
        clane_angle_data.Hook_Position.rotation_z = angle.z;
    }
}

using UnityEngine;

public class ClaneArmDataReceiver : MonoBehaviour
{
    [SerializeField]
    ClaneAngleData clane_angle_data;  //シーン間で受け渡しするデータ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //保存してあるデータを適用する
        transform.localEulerAngles = new Vector3(0, 0, clane_angle_data.Arm_Angle);

        GetComponent<ArticulationBody>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

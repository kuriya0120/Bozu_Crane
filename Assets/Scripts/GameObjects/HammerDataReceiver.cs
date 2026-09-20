using UnityEngine;

public class HammerDataReceiver : MonoBehaviour
{
    [SerializeField]
    ClaneAngleData clane_angle_data;  //シーン間で受け渡しするデータ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //保存してあるデータを適用する
        transform.localPosition = new Vector3(0, clane_angle_data.Hammer_Position.position_y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

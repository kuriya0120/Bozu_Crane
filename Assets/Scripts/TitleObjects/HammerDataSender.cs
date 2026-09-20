using UnityEngine;

public class HammerDataSender : MonoBehaviour
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
        //ローカルy座標をシーン間データに保存
        Vector3 position = GetComponent<Transform>().localPosition;
        clane_angle_data.Hammer_Position.position_y = position.y;
    }
}

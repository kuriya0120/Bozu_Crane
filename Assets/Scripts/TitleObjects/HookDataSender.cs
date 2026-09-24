using UnityEngine;

public class HookDataSender : MonoBehaviour
{
    [SerializeField]
    ClaneAngleData clane_angle_data;  //シーン間で受け渡しするデータ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //保存してあるデータを適用する
        transform.localPosition = new Vector3(0, clane_angle_data.Hook_Position.position_y, 0);
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
        clane_angle_data.Hook_Position.position_y = position.y;
    }
}

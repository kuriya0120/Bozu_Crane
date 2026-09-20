using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private int vibration_timer = 0;    //振動させるタイマフレームー
    private float vibration_width = 0;  //振動させる幅
    private Vector3 first_pos;          //最初のPosition

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //カメラをズームする
        var camera = GetComponent<Camera>();
        camera.orthographicSize = 4;

        //ゲームシーン用にPositionを設定
        transform.position = new Vector3(0, -1, -10);

        first_pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (vibration_timer > 0)
        {
            vibration_timer--;

            float rand_x = (Random.value - 0.5f) * vibration_width;
            float rand_y = (Random.value - 0.5f) * vibration_width;

            Vector3 rand_pos = transform.position + new Vector3(rand_x, rand_y, 0);


            transform.position = Vector3.Lerp(transform.position, rand_pos, 0.5f);

            return;
        }

        transform.position = Vector3.Lerp(transform.position, first_pos, 0.8f);
    }

    public void SetVibrationWithTime(float width,int time)
    {
        vibration_width = width;
        vibration_timer = time;
    }
}

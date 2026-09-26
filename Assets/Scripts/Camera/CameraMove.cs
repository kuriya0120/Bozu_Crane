using Unity.VisualScripting;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    Object Vibration_Canvas;    //振動させるキャンバス

    private int vibration_timer = 0;    //振動させるタイマフレームー
    private float vibration_width = 0;  //振動させる幅
    private Vector3 first_pos;          //最初のPosition

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //カメラをズームする
        var camera = GetComponent<Camera>();
        camera.orthographicSize = 3.5f;

        //ゲームシーン用にPositionを設定
        transform.position = new Vector3(0, -1.6f, -10);

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

            //ランダムに振幅を決定
            float rand_x = (Random.value - 0.5f) * vibration_width;
            float rand_y = (Random.value - 0.5f) * vibration_width;

            Vector3 rand_pos = transform.position + new Vector3(rand_x, rand_y, 0);

            //振幅を適用
            //transform.position = Vector3.Lerp(transform.position, rand_pos, 0.5f);
            transform.position = rand_pos;

            //キャンバスにも振動を適用
            var canvas_script = Vibration_Canvas.GetComponent<UIVibration>();
            canvas_script.SetUIVibrationOffset(rand_x * 10,rand_y * 10);
            

            return;
        }

        //振動が終わったら最初の場所に滑らかに戻す
        transform.position = Vector3.Lerp(transform.position, first_pos, 0.8f);
    }

    public void SetVibrationWithTime(float width,int time)
    {
        vibration_width = width;
        vibration_timer = time;
    }
}

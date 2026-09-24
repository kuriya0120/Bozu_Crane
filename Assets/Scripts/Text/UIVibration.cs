using UnityEngine;

public class UIVibration : MonoBehaviour
{
    private Vector3 first_pos;  //生成時のRectTransform.position
    private RectTransform rect_transform;   //RectTranform

    private int back_firstpos_timer;    //最初の場所に戻すタイマー

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //コンポーネントを取得
        rect_transform = GetComponent<RectTransform>();
        //最初のPositionを取得
        first_pos = rect_transform.localPosition;

        back_firstpos_timer = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (back_firstpos_timer > 0)
        {
            back_firstpos_timer--;

            if (back_firstpos_timer == 0)
            {
                rect_transform.localPosition = first_pos;
            }
        }
    }

    //バイブレーションを設定
    public void SetUIVibrationOffset(float offset_x,float offset_y)
    {
        rect_transform.localPosition = new Vector3(
            first_pos.x + offset_x, first_pos.y + offset_y, first_pos.z);

        back_firstpos_timer = 2;
    }
}

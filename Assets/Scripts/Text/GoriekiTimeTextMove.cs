using UnityEngine;

public class GoriekiTimeTextMove : MonoBehaviour
{
    private enum GoriekiTimeTextMoveState
    {
        Move,
        Slow,
        After_Slow,
        End_Move,
    }


    private float move_speed = -200;   //スライド移動する速度

    private int now_frame = 0;  //現在の経過フレーム
    private int start_slow_time = 10;   //スローになるまでの時間
    private int end_slow_time = 120;   //スローが終わるまでの時間
    private int since_delete_time = 10;    //自身を削除するまでの時間

    private GoriekiTimeTextMoveState state = GoriekiTimeTextMoveState.Move; //現在の状態
    private RectTransform rect_t;   //自身のRectTransform

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //コンポーネント取得
        rect_t = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        //状態によって更新処理を分ける
        switch(state)
        {
            case GoriekiTimeTextMoveState.Move:
                UpdateStateMove();
                break;
            case GoriekiTimeTextMoveState.Slow:
                UpdateStateSlow();
                break;
            case GoriekiTimeTextMoveState.After_Slow:
                UpdateStateAfterSlow();
                break;
            case GoriekiTimeTextMoveState.End_Move:
                //自身を削除
                Destroy(gameObject);
                break;
        }
    }

    private void UpdateStateMove()
    {
        now_frame++;

        //左にスライドしていく
        rect_t.transform.position = rect_t.transform.position + new Vector3(move_speed, 0, 0);

        //一定時間が経過したらステートを切り替え
        if(now_frame == start_slow_time)
        {
            state = GoriekiTimeTextMoveState.Slow;

            now_frame = 0;
        }
    }

    private void UpdateStateSlow()
    {
        now_frame++;

        //左にスライドしていく
        rect_t.transform.position = rect_t.transform.position + new Vector3(move_speed / 1000, 0, 0);

        //一定時間が経過したらステートを切り替え
        if (now_frame == end_slow_time)
        {
            state = GoriekiTimeTextMoveState.After_Slow;
        }
    }

    private void UpdateStateAfterSlow()
    {
        now_frame++;

        //左にスライドしていく
        rect_t.transform.position = rect_t.transform.position + new Vector3(move_speed, 0, 0);

        //一定時間が経過したらステートを切り替え
        if (now_frame == since_delete_time)
        {
            state = GoriekiTimeTextMoveState.End_Move;

            now_frame = 0;
        }
    }
}

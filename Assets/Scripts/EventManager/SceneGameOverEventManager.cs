using Unity.VisualScripting;
using UnityEngine;

public class SceneGameOverEventManager : MonoBehaviour
{
    enum SceneGameOverEventState
    {
        Animation,
        Text_1,
        Wait_Text_2,
        Text_2,
        Wait_Text_3,
        Text_3,
    }

    [SerializeField]
    Animator Illust_Anime;  //イラストのアニメーションスクリプト

    [SerializeField]
    Object Title_Text_1;    //テキスト1

    [SerializeField]
    Object Title_Text_2;    //テキスト2

    [SerializeField]
    Object Title_Text_3;    //テキスト3

    private SceneGameOverEventState state;  //ゲームオーバーシーンのイベント状態
    private float now_time_count = 0;       //1秒を測るための変数
    private int now_time_count_second = 0;  //経過時間(秒数)

    private int text_proceed_time = 3;  //テキストを次に進めるまでの時間
    private int text_wait_time = 1; //次のテキストを出すまでの時間

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        //ステート別に処理を分ける
        switch (state)
        {
            case SceneGameOverEventState.Animation:
                AnimationUpdate();
                break;
            case SceneGameOverEventState.Text_1:
                Text1Update();
                break;
            case SceneGameOverEventState.Wait_Text_2:
                WaitText2Update();
                break;
            case SceneGameOverEventState.Text_2:
                Text2Update();
                break;
            case SceneGameOverEventState.Wait_Text_3:
                WaitText3Update();
                break;
            case SceneGameOverEventState.Text_3:
                Text3Update();
                break;
        }
    }

    private void AnimationUpdate()
    {
        //アニメの進捗を取得
        float normalized_time = Illust_Anime.GetCurrentAnimatorStateInfo(0).normalizedTime;

        //アニメが終了していたらステートを進める
        if(normalized_time >= 1.0f)
        {
            state = SceneGameOverEventState.Text_1;

            //テキスト1を出す
            Title_Text_1.GameObject().SetActive(true);
        }
    }

    private void Text1Update()
    {
        now_time_count += Time.deltaTime;

        if(now_time_count >= 1.0f)
        {
            now_time_count = 0;

            now_time_count_second++;

            //一定時間たったらステートを進める
            if (now_time_count_second > text_proceed_time)
            {
                now_time_count_second = 0;

                state = SceneGameOverEventState.Wait_Text_2;

                //テキスト1を無効化
                Title_Text_1.GameObject().SetActive(false);
            }
        }
    }

    private void WaitText2Update()
    {
        now_time_count += Time.deltaTime;

        if (now_time_count >= 1.0f)
        {
            now_time_count = 0;

            now_time_count_second++;

            //一定時間たったらステートを進める
            if (now_time_count_second > text_wait_time)
            {
                now_time_count_second = 0;

                state = SceneGameOverEventState.Text_2;

                //テキスト2を出す
                Title_Text_2.GameObject().SetActive(true);
            }
        }
    }

    private void Text2Update()
    {
        now_time_count += Time.deltaTime;

        if (now_time_count >= 1.0f)
        {
            now_time_count = 0;

            now_time_count_second++;

            //一定時間たったらステートを進める
            if (now_time_count_second > text_proceed_time)
            {
                now_time_count_second = 0;

                state = SceneGameOverEventState.Wait_Text_3;

                //テキスト2を無効化
                Title_Text_2.GameObject().SetActive(false);
            }
        }
    }

    private void WaitText3Update()
    {
        now_time_count += Time.deltaTime;

        if (now_time_count >= 1.0f)
        {
            now_time_count = 0;

            now_time_count_second++;

            //一定時間たったらステートを進める
            if (now_time_count_second > text_wait_time)
            {
                now_time_count_second = 0;

                state = SceneGameOverEventState.Text_3;

                //テキスト3を出す
                Title_Text_3.GameObject().SetActive(true);
            }
        }
    }
    private void Text3Update()
    {
    }

}

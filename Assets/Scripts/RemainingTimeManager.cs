using UnityEngine;

public class RemainingTimeManager : MonoBehaviour
{
    private int time_scale;
    private int remaining_time;   //残り時間(フレーム数)
    public int remaining_time_second = 30; //残り時間(秒数)
    private bool end_time = false;          //時間切れかどうかのフラグ
    private float time_count = 0;   //1秒をカウントするための変数

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time_scale = (int)(1.0f / Time.deltaTime);
        remaining_time = remaining_time_second * time_scale;   //残り時間(フレーム数)S

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //残り時間がすでに0秒だったら処理しない
        if (end_time) return;

        //残り時間を減らしていく
        time_count += Time.deltaTime / 1.5f;

        if(time_count >= 1.0f)
        {
            time_count = 0.0f;

            remaining_time_second--;

            if (remaining_time_second == 0)
            {
                end_time = true;
            }
        }
    }

    //ゲームが終わっているかどうかのフラグを取得
    public bool GetEndTimeFlag()
    {
        return end_time;
    }

    //残り時間を秒数で取得
    public int GetRemainingTimeSecond()
    {
        return remaining_time_second;
    }

    //ご利益タイム突入時に残り時間を追加
    public void AddRemainingTime()
    {
        remaining_time += 10 * time_scale;
        remaining_time_second += 10;
    }
}

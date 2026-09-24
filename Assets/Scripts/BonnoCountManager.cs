using UnityEngine;

public class BonnoCountManager : MonoBehaviour
{
    //煩悩カウントの唯一の所有者
    public int bonno_count = 108;
    private bool gorieki_time = false;  //ご利益タイムかどうかのフラグ
    private bool sounded_cheer = false; //サウンドをもう鳴らしたかのフラグ

    public int cheer_sound_count = 100; //効果音を鳴らすカウント

    private AudioSource audio_source;   //サウンドコンポーネント

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio_source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //外部から煩悩カウントを取得する関数
    public int GetBonnoCount()
    {
        return bonno_count;
    }

    //煩悩カウントを減らす
    public void SubtructCount(int count)
    {
        bonno_count -= count;

        //煩悩カウントが0以下だったら
        if(bonno_count <= 0)
        {
            //ご利益タイムフラグを建てる
            gorieki_time = true;
        }
        if (-bonno_count >= cheer_sound_count && !sounded_cheer)
        {
            //歓声サウンドを鳴らす
            audio_source.Play();

            sounded_cheer = true;
        }
    }

    //ご利益タイムかどうかのフラグを取得
    public bool GetGoriekiTimeFlag()
    {
        return gorieki_time;
    }
}

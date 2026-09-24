using UnityEngine;

public class HitBonnoCountTextMove : MonoBehaviour
{
    private Vector3 first_pos;  //生成された時点のposition
    private Vector3 target_pos; //移動目標場所

    private int now_frame = 0;  //生成されてからのフレーム数
    private int max_frame = 60;  //消滅までのフレーム数

    private RectTransform rectTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        first_pos = rectTransform.position;
        target_pos = first_pos + new Vector3(0, 50, 0);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        rectTransform.position = Vector3.Lerp(first_pos, target_pos, (float)now_frame / max_frame);

        now_frame++;

        if(now_frame == max_frame)
        {
            Destroy(gameObject);
        }
    }
}

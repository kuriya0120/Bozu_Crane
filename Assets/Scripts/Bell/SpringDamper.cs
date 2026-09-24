using UnityEngine;

public class SpringDamper : MonoBehaviour
{
    private const float spring = 5;        //first_posに戻るバネの強さ
    private const float damping = 5.0f;     //動きを減速させる強さ

    private Vector2 first_pos;  //固定する先の座標

    private Rigidbody2D rb; //RigidBody

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //初期位置を保存しておく
        first_pos = new Vector2(transform.position.x, transform.position.y);

        //RigidBody取得
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //バネ+ダンパーの挙動で一定箇所にバネのように固定
        Vector2 displacement = first_pos - rb.position;
        Vector2 force = displacement * spring;
        force -= rb.linearVelocity * damping;

        rb.AddForce(force);

        //鐘の角度を初期位置と現在位置のxを参照して変更
        float range = transform.position.x - first_pos.x;

        transform.eulerAngles = new Vector3(0, 0, range * 10);
    }
}

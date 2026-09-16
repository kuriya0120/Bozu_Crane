using Unity.VisualScripting;
using UnityEngine;

public class AngleArrowLeft : MonoBehaviour
{
    [SerializeField]
    Object ClaneArm;    //角度を変更するクレーンのアーム

    private bool is_click = false;  //クリックされているかのフラグ
    private const float angle_move_speed = 1.0f;   //アームが動くスピード
    private const float max_angle = 55.0f;          //アームの最大角度
    private Transform clane_arm_t;                  //アームのTransform

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        //アームのTransformを取得
        clane_arm_t = ClaneArm.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (is_click)
        {
            LowerArmAngle();
        }
    }

    public void LowerArmAngle()
    {
        //現在のz角度を取得
        float now_angle = clane_arm_t.localRotation.eulerAngles.z;

        //zの値を一定数上げて丸め込む
        now_angle = Mathf.Clamp(now_angle + (angle_move_speed), 0.0f, max_angle);

        //値を適用
        //clane_arm_t.eulerAngles = new Vector3(0,0,now_angle);
        
        clane_arm_t.localEulerAngles = new Vector3(0.0f, 0.0f, now_angle);
    }

    //クリックされたとき
    public void OnPointerDown()
    {
        is_click = true;
    }

    //クリックが離されたとき
    public void OnPointerUp()
    {
        is_click = false;
    }
}

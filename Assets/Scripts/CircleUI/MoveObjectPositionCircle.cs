using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveObjectPositionCircle : MonoBehaviour
{
    [SerializeField]
    Object base_obj;    //起点となるオブジェクト

    [SerializeField]
    Object move_obj;    //実際に動かすオブジェクト

    [SerializeField]
    Object Camera;

    private bool is_click = false;
    private Camera mainCamera;
    private Transform base_obj_t;//起点となるオブジェクトのトランスフォーム
    private Transform move_obj_t;//実際に動かすオブジェクトのトランスフォーム

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //カメラコンポーネントを取得
        mainCamera = Camera.GetComponent<Camera>();

        //各トランスフォームを取得
        base_obj_t = base_obj.GetComponent<Transform>();
        move_obj_t = move_obj.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (is_click)
        {
            MovePosition();
        }
    }

    public void MovePosition()
    {
        //マウスのスクリーン座標（画面上のピクセル位置）を取得
        Vector2 screenPosition = Pointer.current.position.ReadValue();

        //スクリーン座標をワールド座標に変換
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, Mathf.Abs(mainCamera.transform.position.z)));

        //z成分を消す
        Vector3 mouse_pos = new Vector3(worldPosition.x, worldPosition.y, 0);

        //マウス座標を修正
        mouse_pos = new Vector3(mouse_pos.x / 1.2f, mouse_pos.y / 1.2f, 0);

        //起点の座標を取得
        Vector3 base_pos = base_obj_t.position;

        //動かすオブジェクトの座標を取得
        //Vector3 move_pos = move_obj_t.position;

        //起点からマウスクリック箇所までの距離を計測
        float distance = Vector3.Distance(base_pos, mouse_pos);

        //距離を動かすオブジェクトのローカルy座標に距離を設定
        move_obj_t.localPosition = new Vector3(0, -distance, 0);

        //base_posから見たmove_posのベクトルを正規化して取得
        Vector3 rv = Vector3.Normalize(mouse_pos - base_pos);

        //起点の角度を計算
        float rad_angle = Mathf.Atan2(rv.y,rv.x);

        //Degree角に変更
        float degree_angle = Mathf.Rad2Deg * rad_angle;

        //起点の角度を変更
        base_obj_t.eulerAngles = new Vector3(0, 0, degree_angle + 90);

        //自身のワールド角度は常に無回転にする
        transform.eulerAngles = new Vector3(0, 0, 0);
    }

    //マウスクリックが押されたとき
    public void OnPointerDown()
    {
        is_click = true;
    }

    //マウスクリックが離されたとき
    public void OnPointerUp()
    {
        is_click = false;
    }
}

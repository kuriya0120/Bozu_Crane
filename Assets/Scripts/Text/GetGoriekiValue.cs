using TMPro;
using UnityEngine;

public class GetGoriekiValue : MonoBehaviour
{
    private int gorieki_count = 0;  //ご利益カウント

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //テキストを変更
        var text = GetComponent<TextMeshProUGUI>();
        text.text = "獲得したご利益  <size=45>" + gorieki_count.ToString() + "</size>";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGoriekiCount(int count)
    {
        gorieki_count = Mathf.Abs(count);
    }
}

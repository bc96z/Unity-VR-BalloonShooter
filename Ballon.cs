using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ballon : MonoBehaviour
{
    public float riseSpeed; // 上升速度，可在 Inspector 調整
    private float initialHeight; // 氣球的初始高度
    private bool isRising = false; // 是否正在上升
    public bool isHit = false; // 是否被擊中


    public Material normalMaterial; // 普通氣球材質
    public Material goldenMaterial; // 金色氣球材質
    private Renderer balloonRenderer; // 氣球的渲染器
    private bool isGolden = false; // 是否是金色氣球

    
    public Score sss;

    private void Start()
    {
        initialHeight = transform.position.y; // 記錄初始高度
        riseSpeed = Random.Range(2f, 7f);
        Transform balloonChild = transform.Find("Baloon_baloon");
        balloonRenderer = balloonChild.GetComponent<Renderer>();
        StartCoroutine(RandomRise()); // 開始隨機上升
    }

    private void Update()
    {
        if (isRising && !isHit)
        {
            transform.Translate(Vector3.up * riseSpeed * Time.deltaTime); // 氣球上升

            if (transform.position.y >= initialHeight + 25f)
            {
                // 回到初始高度
                ResetBalloon();
            }
        }
    }

    private System.Collections.IEnumerator RandomRise()
    {
        yield return new WaitForSeconds(Random.Range(1f, 4f)); // 隨機等待 1 到 5 秒
        isRising = true; // 開始上升
    }
    public void HitBalloon()
    {
        isHit = true; // 標記氣球被擊中
        if(isGolden)
        {
            sss.AddScore(100);
        }
        else
        {
            sss.AddScore(10);
        }
        gameObject.SetActive(false); // 暫時隱藏氣球
        Invoke(nameof(ResetBalloon), 1f); // 2 秒後重置氣球
    }

    private void ResetBalloon()
    {
        riseSpeed = Random.Range(2f, 7f);
        if (Random.value < 0.05f) // 10% 機率
        {
            isGolden = true;
            balloonRenderer.material = goldenMaterial; // 更改為金色材質
        }
        else
        {
            isGolden = false;
            balloonRenderer.material = normalMaterial; // 更改為普通材質
        }
        isHit = false; // 重置標記
        transform.position = new Vector3(transform.position.x, initialHeight, transform.position.z); // 回到初始高度
        gameObject.SetActive(true); // 再次顯示氣球
        isRising = false; // 停止上升
        StartCoroutine(RandomRise()); // 等待重新上升
    }
}

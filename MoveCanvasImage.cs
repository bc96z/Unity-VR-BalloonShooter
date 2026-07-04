using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCanvasImage : MonoBehaviour
{
    public Transform rayOrigin; // 手把的 Transform（通常是 Ray Origin Transform）
    public RectTransform imageToMove; // 目標圖片的 RectTransform
    public Canvas targetCanvas; // 目標 Canvas

    public Vector2 bottomLeftCorner = new Vector2(-16f, -38.9f); // 左下角
    public Vector2 topRightCorner = new Vector2(27.6f, -5.9f);   // 右上角
    void Update()
    {
        if (rayOrigin == null || imageToMove == null || targetCanvas == null) return;

        // 計算雷射方向的射線
        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);

        // 定義 Canvas 平面
        Plane canvasPlane = new Plane(targetCanvas.transform.forward, targetCanvas.transform.position);

        // 計算射線與 Canvas 平面的交點
        if (canvasPlane.Raycast(ray, out float enter))
        {
            // 計算交點的世界座標
            Vector3 hitPoint = ray.GetPoint(enter);

            // 將世界座標轉換為 Canvas 的本地座標
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                targetCanvas.GetComponent<RectTransform>(),
                Camera.main.WorldToScreenPoint(hitPoint),
                Camera.main,
                out Vector2 localPoint
            );
            localPoint.x = Mathf.Clamp(localPoint.x, bottomLeftCorner.x, topRightCorner.x);
            localPoint.y = Mathf.Clamp(localPoint.y, bottomLeftCorner.y, topRightCorner.y);
            // 更新圖片的位置
            imageToMove.anchoredPosition = localPoint;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject crosshair; // 準心物件
    public Transform rayInteractor; // Ray Interactor 的起點
    public LayerMask planeLayerMask; // 平面層，用於碰撞檢測

    private void Update()
    {
        Ray ray = new Ray(rayInteractor.position, rayInteractor.forward); // 生成射線
        RaycastHit hit;

        // 檢查射線是否與指定平面碰撞
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, planeLayerMask))
        {
            crosshair.SetActive(true); // 顯示準心
            crosshair.transform.position = hit.point; // 將準心移動到射線碰撞點
            crosshair.transform.rotation = Quaternion.LookRotation(hit.normal); // 讓準心面向碰撞的法線方向
        }
        else
        {
            crosshair.SetActive(false); // 如果射線沒有打到平面，隱藏準心
        }
    }
}

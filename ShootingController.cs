using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;

public class ShootingController : MonoBehaviour
{
    private AudioSource audioSource;
    public Transform rayOrigin; // 手把的 Transform，用於生成雷射
    public float triggerDelay = 0f; // 按下 Trigger 後的延遲時間
    public float cooldownTime = 0.3f; // 冷卻時間，防止連續射擊
    public XRNode controllerNode = XRNode.RightHand; // 右手控制器
    public LayerMask balloonLayer; // 氣球所在的圖層

    private bool isShooting = false; // 標記是否正在射擊
    private bool canShoot = true; // 標記是否可以射擊


    
    //public GameObject bulletPrefab; // 
    //public float bulletSpeed = 20f; // 

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        // 檢測右手控制器 Trigger 按鍵
        InputDevice controller = InputDevices.GetDeviceAtXRNode(controllerNode);
        if (controller.TryGetFeatureValue(CommonUsages.triggerButton, out bool isPressed))
        {
            if (isPressed && !isShooting && canShoot)
            {
                StartCoroutine(Shoot());
                //count++; // 數字加 1
                //debugText.text = count.ToString();
            }
        }
        //StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        isShooting = true; // 標記為正在射擊
        canShoot = false; // 暫時禁止射擊
        //yield return new WaitForSeconds(triggerDelay); // 延遲時間
        //GameObject bullet = Instantiate(bulletPrefab, rayOrigin.position, Quaternion.identity);

        
        //StartCoroutine(MoveBullet(bullet, rayOrigin.forward));
        // 使用手把的雷射生成射線
        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);

        // 檢測射線是否擊中氣球
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, balloonLayer))
        {
            //count++; // 數字加 1
            //debugText.text = count.ToString();
            // 如果射線擊中了氣球，執行擊中邏輯
            Ballon balloon = hit.collider.GetComponentInParent<Ballon>();
            if (balloon != null)
            {
                //count++; // 數字加 1
                //debugText.text = count.ToString();
                balloon.HitBalloon(); // 氣球被擊中
                audioSource.Play();
            }
        }

        isShooting = false; // 射擊完成
        yield return new WaitForSeconds(cooldownTime); // 冷卻時間
        canShoot = true; // 冷卻結束後允許射擊
    }
    /*
    private IEnumerator MoveBullet(GameObject bullet, Vector3 direction)
    {
        while (bullet != null && bullet.transform.position.z > -20)
        {
            bullet.transform.Translate(direction * bulletSpeed * Time.deltaTime, Space.World);
            yield return null;
        }

        
        if (bullet != null)
        {
            Destroy(bullet);
        }
    }
    */
}

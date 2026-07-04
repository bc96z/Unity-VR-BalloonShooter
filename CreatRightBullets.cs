using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class CreatRightBullets : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform rayOrigin;
    public float bulletSpeed = 20f;

    public float shootCooldown = 0.05f; // 射擊冷卻時間
    private bool canShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //PicoInput.GetButtonDown(PicoInput.PicoButton.TriggerR)
        if (InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.primaryButton, out bool a) && a)
        {
            if(canShoot)
            {
                Vector3 currentPosition = rayOrigin.position;
                Instantiate(bulletPrefab.gameObject, currentPosition, rayOrigin.rotation).GetComponent<RightBullet>();
                StartCoroutine(ShootCooldown());
            }
            
        }
    }

    private IEnumerator ShootCooldown()
    {
        canShoot = false; // 在冷卻期間禁用射擊
        yield return new WaitForSeconds(shootCooldown); // 等待冷卻時間
        canShoot = true; // 冷卻結束後允許射擊
    }
}

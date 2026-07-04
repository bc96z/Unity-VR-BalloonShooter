using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightBullet : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(-90,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(transform.forward * -speed * Time.deltaTime);
    }
}

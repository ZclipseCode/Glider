using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    float timer;
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 3) {
            Destroy(gameObject);
        }
    }
}

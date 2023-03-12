using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    [SerializeField] GameObject player, spawn;
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider obj) {
        if (obj.CompareTag("Enemy")) {
            player.transform.position = spawn.transform.position;
        }
    }
}

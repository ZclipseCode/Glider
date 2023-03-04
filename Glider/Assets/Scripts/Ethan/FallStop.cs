using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallStop : MonoBehaviour
{
    [SerializeField] GameObject player, spawn;
    void Update()
    {
        if(player.transform.position.y < 0) {
            player.transform.position = spawn.transform.position;
        }
    }
}

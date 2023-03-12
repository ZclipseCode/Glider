using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetect : MonoBehaviour {
    [SerializeField] EnemyShoot es;

    private void OnTriggerStay(Collider collision) {
        if (collision.gameObject.tag == "Player") {
            es.playerInZone = true;
            es.player = collision.gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider collision) {
        if (collision.gameObject.tag == "Player") {
            es.playerInZone = false;
        }
    }
}

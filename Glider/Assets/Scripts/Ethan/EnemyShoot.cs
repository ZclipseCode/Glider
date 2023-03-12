using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletPoint;
    [SerializeField] float timeBetweenShots;
    [SerializeField] float bulletSpeed;
    float timer;
    public bool playerInZone;
    public Transform player;

    void Start() {
        timer = timeBetweenShots;
    }

    void Update() {
        if (playerInZone) {
            timer += Time.deltaTime;
            if (timer >= timeBetweenShots) {
                Shoot(player);
                timer = 0;
            }
        }
    }

    public void Shoot(Transform player) {
        GameObject newbullet = Instantiate(bullet, bulletPoint.position, Quaternion.identity);
        newbullet.GetComponent<Rigidbody>().velocity = (player.position - bulletPoint.transform.position).normalized * bulletSpeed;
    }
}

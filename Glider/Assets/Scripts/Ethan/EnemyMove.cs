using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] GameObject enemy, pos1;
    [SerializeField] GameObject[] positions;
    [SerializeField] float moveSpeed;
    int posNum;
    void Start() {
        posNum = 0;
    }
    void Update()
    {
        if(posNum == positions.Length) {
            //lerp to pos1 then reset posNum
            enemy.transform.position = Vector3.Lerp(enemy.transform.position, pos1.transform.position, moveSpeed / 1000);
            if (Vector3.Distance(enemy.transform.position, pos1.transform.position) < 0.001) {
                posNum = 0;
            }
        }
        else {
            //lerp to next pos then append posNum
            enemy.transform.position = Vector3.Lerp(enemy.transform.position, positions[posNum].transform.position, moveSpeed / 1000);
            if (Vector3.Distance(enemy.transform.position, positions[posNum].transform.position) < 0.001) {
                posNum++;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemys : MonoBehaviour
{
    public Transform[] RespawnPoints;

    float time = 0.0f;

    int 
        TimeNewEnemy = 2,
        Position;

    public int TimeBetweenRespawn;

    bool Warning;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        MethodShowMessageWarningSpawn();
        MethodRespawnEnemys();
    }

    void MethodShowMessageWarningSpawn()
    {
        if ((int)time == (TimeNewEnemy - 2) && !Warning)
        {
            Warning = true;
            Position = Random.Range(0, RespawnPoints.Length);
            RespawnPoints[Position].gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
            SpriteRenderer WarningRespawn = RespawnPoints[Position].GetComponentInChildren<SpriteRenderer>();
            WarningRespawn.transform.LookAt(WarningRespawn.transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        }
        else if ((int)time == (TimeNewEnemy - 1) && Warning)
        {
            Warning = false;
            RespawnPoints[Position].GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }

    void MethodRespawnEnemys()
    {
        if ((int)time == TimeNewEnemy)
        {
            TimeNewEnemy = TimeNewEnemy + TimeBetweenRespawn;
            GameObject enemy = Instantiate(Resources.Load("Enemy"), RespawnPoints[Position].position, Quaternion.identity) as GameObject;
            enemy.name = enemy.name.Replace("(Clone)", "");
        }
    }
}

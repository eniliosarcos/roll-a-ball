using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    // Use this for initialization
    public bool PlayerIsSafe;

    GameObject SpawnEnemy;

    void Awake()
    {
        SpawnEnemy = GameObject.Find("Spawn Enemy");
        GameObject player = Instantiate(Resources.Load("Player"),transform.position,Quaternion.identity) as GameObject;
        player.name = player.name.Replace("(Clone)", "");
    }

    private void Update()
    {
        MethodCheckPlayerSafe();
    }

    void MethodCheckPlayerSafe()
    {
        if (PlayerIsSafe && SpawnEnemy.activeInHierarchy)
        {
            SpawnEnemy.SetActive(false);
        }

        if (!PlayerIsSafe && !SpawnEnemy.activeInHierarchy)
        {
            SpawnEnemy.SetActive(true);
        }
    }
}



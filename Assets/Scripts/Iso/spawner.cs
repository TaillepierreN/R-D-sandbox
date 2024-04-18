using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject mobs;
    public float spawnRadius = 20.0f;

    public void spawnMobs()
    {
        Vector3 spawnPos = Random.onUnitSphere * spawnRadius + player.transform.position;
        spawnPos.y = player.transform.position.y;

        Instantiate(mobs, spawnPos, Quaternion.identity);
    }
}

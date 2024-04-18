using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtPlayer : MonoBehaviour
{
    [SerializeField] Transform player;
    void Update()
    {
        transform.LookAt(player.position);
    }
}

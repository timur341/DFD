using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checpoint : MonoBehaviour
{
    public Transform player;
    public int index;
    private void Awake()
    {
        if (DataCntainer.checpointIndex == index)
        {
            player.position = transform.position;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (index > DataCntainer.checpointIndex)
            {
               DataCntainer.checpointIndex = index;
            }
            
        } 
    }
}

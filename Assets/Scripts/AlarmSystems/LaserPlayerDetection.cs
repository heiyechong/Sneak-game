using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPlayerDetection : MonoBehaviour
{
    //Íæ¼Ò
    private GameObject player;

    //
    private LastPlayerSighting lastPlayerSighting;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player);
        lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (GetComponent<Renderer>().enabled)
        {
            if (other.gameObject ==player)
            {
                lastPlayerSighting.position = other.transform.position;
            } 
        } 
    }
}

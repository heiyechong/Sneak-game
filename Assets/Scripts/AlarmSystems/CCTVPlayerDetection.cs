using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVPlayerDetection : MonoBehaviour
{
    //用于侦测玩家
    private GameObject player;
    //用于更新玩家信息
    private LastPlayerSighting lastPlayerSighting;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player);
        lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag ==Tags.player)
        {
            Vector3 vector = player.transform.position - transform.position;
            RaycastHit raycastHit;
            if (Physics.Raycast(transform.position,vector,out raycastHit))
            {
                if (raycastHit.collider.gameObject ==player)
                {
                    lastPlayerSighting.position = player.transform.position;
                }
            }
        }
    }
}

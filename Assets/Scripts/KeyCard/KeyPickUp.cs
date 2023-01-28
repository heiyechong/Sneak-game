using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ¼ñÆðÔ¿³×¿¨
/// </summary>
public class KeyPickUp : MonoBehaviour
{
    private PlayerInventory playerInventory;
    private GameObject player;
    public AudioClip audioClip; 
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player);
        playerInventory = player.GetComponent<PlayerInventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject==player)
        {
            AudioSource.PlayClipAtPoint(audioClip, transform.position);
            playerInventory.hasInventory = true;
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSwitchdDeactivation : MonoBehaviour
{
    //激光门
    public GameObject laser;
    //通过开关屏幕提示激光门已打开
    public Material unlockedMat;
    //玩家
    private GameObject player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player);

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject ==player)
        {
            if (Input.GetButton("Switch"))
            {
                LaserDeactivation();
            }
        }
    }

    void LaserDeactivation()
    {
        laser.SetActive(false);
        Renderer screen = transform.Find("prop_switchUnit_screen_001").GetComponent<MeshRenderer>();
        screen.material = unlockedMat;
        GetComponent<AudioSource>().Play();
    }
}

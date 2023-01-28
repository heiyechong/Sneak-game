using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSwitchdDeactivation : MonoBehaviour
{
    //������
    public GameObject laser;
    //ͨ��������Ļ��ʾ�������Ѵ�
    public Material unlockedMat;
    //���
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

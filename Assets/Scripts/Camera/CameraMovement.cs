using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    private Vector3 newPos;
    private Transform player;
    private float smooth = 1.5f;
    //摄像机与角色的相对位置
    private Vector3 relCameraPos;
    //摄像机与角色的相对位置的固定长度
    private float relCameraPosMag;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<Transform>();
        relCameraPos = transform.position - player.position;
        relCameraPosMag = relCameraPos.magnitude - 0.5f;
    }
    private void FixedUpdate()
    {
        //摄像机的初始位置
        Vector3 vector = player.position + relCameraPos;
        //摄像机俯视角色的位置
        Vector3 vector1 = player.position + player.up * relCameraPosMag;
        Vector3[] vectors = new Vector3[5];
        vectors[0] = vector;
        vectors[1] = Vector3.Lerp(vector, vector1, 0.25f);
        vectors[2] = Vector3.Lerp(vector, vector1, 0.50f);
        vectors[3] = Vector3.Lerp(vector, vector1, 0.75f);
        vectors[4] = vector1;
        for (int i = 0; i < vectors.Length; i++)
        {
            if (RayCasting(vectors[i]))
                break;
        }
        transform.position = Vector3.Lerp(transform.position, newPos, smooth * Time.fixedDeltaTime);
        LookAt();
    }
    private bool RayCasting(Vector3 vector)
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(vector, player.position, out raycastHit, relCameraPosMag))
        {
            if (raycastHit.transform != player)
            {
                return false;
            }
        }
        newPos = vector;
        return true;
    }
    private void LookAt()
    {
        Vector3 vector = player.position - transform.position;
        Quaternion quaternion = Quaternion.LookRotation(vector);
        transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, smooth*Time.deltaTime);
    }

}

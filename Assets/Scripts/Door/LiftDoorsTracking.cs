using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// µçÌÝÒÆ¶¯
/// </summary>
public class LiftDoorsTracking : MonoBehaviour
{
    public float doorSpeed = 7f;
    private Transform leftOuterDoor;
    private Transform rightOutDoor;
    private Transform leftInnerDoor;
    private Transform rightInnerDoor;
    private float leftClosedPosX;
    private float rightClosePosX;
    private void Awake()
    {
        leftOuterDoor = GameObject.Find("door_exit_outer_left_001").transform;
        rightOutDoor = GameObject.Find("door_exit_outer_right_001").transform;
        leftInnerDoor = GameObject.Find("door_exit_inner_left_001").transform;
        rightInnerDoor = GameObject.Find("door_exit_inner_right_001").transform;
        leftClosedPosX = leftInnerDoor.position.x;
        rightClosePosX = rightInnerDoor.position.x;
    }
    void MoveDoors(float newLeftTarget,float newRightTarget)
    {
        float newX = Mathf.Lerp(leftInnerDoor.position.x, newLeftTarget, doorSpeed * Time.deltaTime);
        leftInnerDoor.position = new Vector3(newX, leftInnerDoor.position.y, leftInnerDoor.position.z);
        newX = Mathf.Lerp(rightInnerDoor.position.x, newRightTarget, doorSpeed * Time.deltaTime);
        rightInnerDoor.position = new Vector3(newX, rightInnerDoor.position.y, rightInnerDoor.position.z);
    }
   public void DoorFollowing()
    {
        MoveDoors(leftOuterDoor.position.x, rightOutDoor.position.x);
    }
    public void CloseDoors()
    {
        MoveDoors(leftClosedPosX, rightClosePosX);
    }
}


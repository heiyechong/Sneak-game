using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用于设置缓冲时间以及设置动画控制器的参数
/// </summary>
public class AnimatorSetUp 
{
    public float speedDampTime = 0.1f;
    public float angularSpeedDampTime = 0.7f;
    public float angleResonseTime = 0.6f;

    private Animator ani;
    private HashIDs hash;

    
    public AnimatorSetUp(Animator animator,HashIDs hashIDs)
    {
        ani = animator;
        hash = hashIDs;
    }

    public void SetUp(float speed,float angule)
    {
        float angularSpeed = angule / angleResonseTime;
        ani.SetFloat(hash.speedFloat, speed, speedDampTime, Time.deltaTime);
        ani.SetFloat(hash.angularSpeedFloat, angularSpeed, angularSpeedDampTime, Time.deltaTime);
    }
}

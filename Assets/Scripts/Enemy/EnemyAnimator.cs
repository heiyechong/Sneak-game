using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimator : MonoBehaviour
{
    //敌人移动时无视的区间值
    //当敌人前进方向与期望速度小于一定的值时停止转向
    public float deadZone = 5f;
    private Transform player;
    private EnemySight enemySight;
    private NavMeshAgent nav;
    private Animator animator;
    private HashIDs hash;
    private AnimatorSetUp setUp;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        enemySight = GetComponent<EnemySight>();
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();
        //禁止navmeshAgent的自动转向，采用动画的
        nav.updateRotation = false;
        setUp = new AnimatorSetUp(animator, hash);

        animator.SetLayerWeight(1, 1);
        animator.SetLayerWeight(2, 1);
        //将deadZone从角度转成弧度
        deadZone *= Mathf.Deg2Rad;
    }
    private void Update()
    {
        NavAnimSetup();
    }
    
    //这个函数可以手动控制Root Motion
    private void OnAnimatorMove()
    {
        //每帧移动的距离
        nav.velocity = animator.deltaPosition / Time.deltaTime;
        transform.rotation = animator.rootRotation;
    }
    void NavAnimSetup()
    {
        float speed;
        float angle;
        //当玩家被敌人发现
        if (enemySight.playerInSight)
        {
            speed = 0f;
            angle = FindAngle(transform.forward,player.position - transform.position , transform.up);
        }
        else
        {
            speed = Vector3.Project(nav.desiredVelocity, transform.forward).magnitude;
            angle = FindAngle(transform.forward, nav.desiredVelocity, transform.up);
            if (Mathf.Abs(angle)<deadZone)
            {
                transform.LookAt(transform.position + nav.desiredVelocity);
                angle = 0f;
            }
        }
        setUp.SetUp(speed, angle);
    }
    
    //计算敌人应该面对的方向和当前所在的方向的角度
    //toVector:要面对的方向
    //fromVector:当前方向
    //upVector:法向量哪个方向为上
    float FindAngle(Vector3 fromVector,Vector3 toVector,Vector3 upVector)
    {   //期望速度等于零时会报错，所以要规定一下
        if (toVector ==Vector3.zero)
        {
            return 0f;
        }
       float angle = Vector3.Angle(fromVector, toVector);
       Vector3 noral = Vector3.Cross(fromVector, toVector);
        angle *= Mathf.Sign(Vector3.Dot(noral, upVector));
        angle *= Mathf.Deg2Rad;
        return angle;
    }
}

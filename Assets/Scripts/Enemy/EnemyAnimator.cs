using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimator : MonoBehaviour
{
    //�����ƶ�ʱ���ӵ�����ֵ
    //������ǰ�������������ٶ�С��һ����ֵʱֹͣת��
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
        //��ֹnavmeshAgent���Զ�ת�򣬲��ö�����
        nav.updateRotation = false;
        setUp = new AnimatorSetUp(animator, hash);

        animator.SetLayerWeight(1, 1);
        animator.SetLayerWeight(2, 1);
        //��deadZone�ӽǶ�ת�ɻ���
        deadZone *= Mathf.Deg2Rad;
    }
    private void Update()
    {
        NavAnimSetup();
    }
    
    //������������ֶ�����Root Motion
    private void OnAnimatorMove()
    {
        //ÿ֡�ƶ��ľ���
        nav.velocity = animator.deltaPosition / Time.deltaTime;
        transform.rotation = animator.rootRotation;
    }
    void NavAnimSetup()
    {
        float speed;
        float angle;
        //����ұ����˷���
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
    
    //�������Ӧ����Եķ���͵�ǰ���ڵķ���ĽǶ�
    //toVector:Ҫ��Եķ���
    //fromVector:��ǰ����
    //upVector:�������ĸ�����Ϊ��
    float FindAngle(Vector3 fromVector,Vector3 toVector,Vector3 upVector)
    {   //�����ٶȵ�����ʱ�ᱨ������Ҫ�涨һ��
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

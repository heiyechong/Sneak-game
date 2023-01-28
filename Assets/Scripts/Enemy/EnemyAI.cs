using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    //Ѳ���ٶ�
    public float patrolSpeed = 2f;
    //׷���ٶ�
    public float chaseSpeed = 5f;
    //������һ���������ͣ����ʱ��
    public float partrolWaitTime = 1f;
    public float chaseWaitTime = 5f;
    public Transform[] patrolWayPoints;

    private EnemySight enemySight;
    private NavMeshAgent nav;
    private Transform player;
    private PlayerHealth playerHealth;
    private LastPlayerSighting lastPlayerSighting;
    
    private float chaseTimer;
    private float partrolTimer;
    //����һ��·����������������ж������е��ĸ�·���ǵ��˵�Ŀ�ĵ�
    private int wayPointIndex;

    private void Awake()
    {
        enemySight = GetComponent<EnemySight>();
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
    }

    private void Update()
    {
        if (enemySight.playerInSight&&playerHealth.hp>0f)
        {
            Shooting();
        }
        else if (enemySight.personalLastSighting!= lastPlayerSighting.rePosition && playerHealth.hp > 0f)
        {
            Chasing();
        }
        else
        {
            Patrolling();
        }
    }

    void Shooting()
    {
        nav.isStopped = true;
    }

    /// <summary>
    /// ֻ���ڵ��˵�ǰλ������������λ�õľ������һ����ֵʱ����׷��
    /// </summary>
    void Chasing()
    {
        Vector3 vector = enemySight.personalLastSighting - transform.position;
        if (vector.sqrMagnitude>4f)
        {
            nav.destination = enemySight.personalLastSighting;
        }
        nav.speed = chaseSpeed;
        if (nav.remainingDistance<nav.stoppingDistance)
        {
            chaseTimer += Time.deltaTime;
            if (chaseTimer>chaseWaitTime)
            {
                lastPlayerSighting.position = lastPlayerSighting.rePosition;
                enemySight.personalLastSighting = lastPlayerSighting.rePosition;
                chaseTimer = 0f;
            }
        }
        else
        {
            chaseTimer = 0f;
        }
        nav.isStopped = false;
    }

    void Patrolling()
    {
        nav.speed = patrolSpeed;
        if (nav.destination == enemySight.personalLastSighting || nav.remainingDistance<nav.stoppingDistance)
        {
            partrolTimer += Time.deltaTime;
            if (partrolTimer>partrolWaitTime)
            {
                if (wayPointIndex == patrolWayPoints.Length-1)
                {
                    wayPointIndex = 0;
                }
                else
                {
                    wayPointIndex++;
                }
                partrolTimer = 0;
            }
        }
        else
        {
            partrolTimer = 0f;
        }
        nav.destination = patrolWayPoints[wayPointIndex].position;
        nav.isStopped = false;
    }
}

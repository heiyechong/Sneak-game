using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySight : MonoBehaviour
{
    //�涨���˵��ӽǷ�Χ
    public float fieldOfViewAngle = 110;
    //���˿�����ҷ���true
    public bool playerInSight;
    //������������͸���ȫ�ֱ�������Ȼ��һ������֪���������λ��
    public Vector3 personalLastSighting;

    private NavMeshAgent nav;

    private SphereCollider sphereCollider;

    private LastPlayerSighting lastPlayerSighting;

    private Animator ani;

    private GameObject player;

    private Animator playerAnimator;

    private PlayerHealth playerHealth;

    private HashIDs hashIDs;

    //�洢��һ֡��ҵ�λ��
    private Vector3 previousSighting;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        sphereCollider = GetComponent<SphereCollider>();
        lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
        player = GameObject.FindGameObjectWithTag(Tags.player);
        playerAnimator = player.GetComponent<Animator>();
        playerHealth = player.GetComponent<PlayerHealth>();
        hashIDs = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();
        personalLastSighting = lastPlayerSighting.rePosition;
        previousSighting = lastPlayerSighting.rePosition;
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        if (lastPlayerSighting.position!=previousSighting)
        {
            personalLastSighting = lastPlayerSighting.position;
        }
        previousSighting = lastPlayerSighting.position;
        if (playerHealth.hp>0f)
        {
            ani.SetBool(hashIDs.playerInsightBool, playerInSight);
        }
        else
        {
            ani.SetBool(hashIDs.playerInsightBool, false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInSight = false;
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction,transform.forward);
            if (angle<fieldOfViewAngle*0.5f)
            {
                RaycastHit raycastHit;
                if (Physics.Raycast(transform.position + transform.up, direction, out raycastHit, sphereCollider.radius)) 
                {
                    if (raycastHit.collider.gameObject ==player)
                    {
                        playerInSight = true;
                        personalLastSighting = player.transform.position;
                    }
                }
            }
            //�������ֻ����������û������   
            //��ǰ����㶯�����ִ�ж���
            int playerLayerZeroStateHash = playerAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash;
            int playerLayerOneStateHash = playerAnimator.GetCurrentAnimatorStateInfo(1).fullPathHash;

            if (playerLayerZeroStateHash == hashIDs.locomotionState ||playerLayerOneStateHash == hashIDs.shoutState)
            {
                if (CalculatePathLength(player.transform.position) <= sphereCollider.radius)
                {
                    personalLastSighting = player.transform.position;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject ==player)
        {
            playerInSight = false;
            personalLastSighting = lastPlayerSighting.position;
        }
    }
    float CalculatePathLength(Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();
        nav.CalculatePath(targetPosition, path);
        Vector3[] vectors = new Vector3[path.corners.Length + 2];
        vectors[0] = transform.position;
        vectors[vectors.Length - 1] = targetPosition;
        for (int i = 1; i < vectors.Length - 1; i++)
        {
            vectors[i] = path.corners[i - 1];
        }
        float pathLength = 0f;
        for (int i = 0; i < vectors.Length - 1; i++)
        {
            pathLength += Vector3.Distance(vectors[i], vectors[i + 1]);
        }
        return pathLength;
    }
}

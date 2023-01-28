using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public float maxDamage = 110f;
    public float minDamage = 45f;
    public AudioClip shootClip;
    //射击特效灯光强度为三
    public float intensity = 3f;
    //渐变速度为10
    public float fadeSpeed = 10f;

    private Animator anim;
    private Transform player;
    private HashIDs hash;
    private PlayerHealth playerHealth;
    private Light selflight;
    private LineRenderer lineRenderer;
    //判断射击范围
    private SphereCollider sphereCollider;
    private bool shooting;
    //伤害的浮动范围
    private float scaleDamage;
   
    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        hash =GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();
        playerHealth = player.GetComponent<PlayerHealth>();
        selflight = GetComponentInChildren<Light>();
        lineRenderer = GetComponentInChildren<LineRenderer>();
        sphereCollider = GetComponent<SphereCollider>();
        lineRenderer.enabled = false;
        selflight.intensity = 0f;
        scaleDamage = maxDamage - minDamage;
    }
    private void Update()
    {
        float shot = anim.GetFloat(hash.shotFloat);
        if (shot>0.5&&!shooting)
        {
            Shoot();
        }
        if (shot<0.5)
        {
            shooting = false;
            lineRenderer.enabled = false;
        }
        selflight.intensity = Mathf.Lerp(selflight.intensity, 0f, fadeSpeed*Time.deltaTime);
    }
    private void OnAnimatorIK(int layerIndex)
    {
        float aimWeight = anim.GetFloat(hash.aimWeightFloat);
        anim.SetIKPosition(AvatarIKGoal.RightHand, player.transform.position + Vector3.up * 1.5f);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, aimWeight);
    }
    void Shoot()
    {
        shooting = true;
        float fractionalDistance = (sphereCollider.radius - Vector3.Distance(transform.position, player.position)) / sphereCollider.radius;
        float damage = minDamage + scaleDamage * fractionalDistance;
        playerHealth.HPDamage(damage);
        ShootEffect();
    }

    void ShootEffect()
    {
        lineRenderer.SetPosition(0,lineRenderer.transform.position);
        lineRenderer.SetPosition(1, player.transform.position + Vector3.up * 1.5f);
        lineRenderer.enabled = true;
        selflight.intensity = intensity;
        AudioSource.PlayClipAtPoint(shootClip,lineRenderer.transform.position);
    }
}

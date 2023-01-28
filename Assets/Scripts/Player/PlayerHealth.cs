using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float hp = 100;
    //死亡动画
    private Animator animator;
    //死亡后多久重开
    private float deathTime = 5f;
    private SceneFadeInOut sceneFadeInOut;
    private LastPlayerSighting lastPlayerSighting;
    public AudioClip deathClip;
    private PlayerMovement playerMovement;
    private HashIDs hashIDs;
    private float timer;
    private bool deathBool = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        sceneFadeInOut = GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<SceneFadeInOut>();
        playerMovement = GetComponent<PlayerMovement>();
        lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
        hashIDs = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();
    }
    private void Update()
    {
        if (hp<=0)
        {
            if (!deathBool)
            {
                PlayerDying();
            }
            else
            {
                Dead();
                LevelReset();
            }
        }
    }

    private void PlayerDying()
    {
        deathBool = true;
        animator.SetBool(hashIDs.Dead, deathBool);
        AudioSource.PlayClipAtPoint(deathClip, transform.position);
    }
    private void Dead()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash==hashIDs.DyingState)
        {
            animator.SetBool(hashIDs.Dead, false);
        }
        animator.SetFloat(hashIDs.speedFloat, 0f);
        playerMovement.enabled = false;
        lastPlayerSighting.position = lastPlayerSighting.rePosition;
        GetComponent<AudioSource>().Stop();
    }

    private void LevelReset()
    {
        timer += Time.deltaTime;
        if (timer>=deathTime)
        {
            sceneFadeInOut.EndScene();
        }
    }
    public void HPDamage(float attack)
    {
        hp -= attack;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public AudioClip shoutAudioClip;
    public float turnSmoothing = 15f;
    public float speedDampTime = 0.1f;
    private Animator animator;
    private HashIDs hashIDs;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        hashIDs = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();
        //设置第二个动画层的权重。第一个参数为哪个动画层，第二个参数为权重是多少。
        animator.SetLayerWeight(1, 1);
    }
    private void Update()
    {
      bool shout =  Input.GetButtonDown("Shout");
        animator.SetBool(hashIDs.shoutingBool, shout);
        AudioManager(shout);
    }
    //因为角色是物理对象
    private void FixedUpdate()
    {
        float hz = Input.GetAxis("Horizontal");
        float vt = Input.GetAxis("Vertical");
        bool sneak = Input.GetButton("Sneak");
        MovementManager(hz, vt, sneak);
    }

    void MovementManager(float hz, float vt, bool sneak)
    {
        animator.SetBool(hashIDs.sneakingBool, sneak);
        if (hz != 0 || vt != 0)
        {
            Rotating(hz, vt);
            animator.SetFloat("Speed", 5.5f, speedDampTime, Time.fixedDeltaTime);
        }
        else
        {
            animator.SetFloat(hashIDs.speedFloat, 0f);
        }
    }
    void Rotating(float hz, float vt)
    {
        Vector3 vector3 = new Vector3(hz, 0, vt);
        Quaternion targetRotation = Quaternion.LookRotation(vector3);
        Quaternion processRotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSmoothing * Time.deltaTime);
        transform.rotation = processRotation;
       
       
    }

    //当角色处于Locomotion动画时才调用这个方法
    void AudioManager(bool shout)
    {
        AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (animatorStateInfo.fullPathHash == hashIDs.locomotionState)
        {
            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            GetComponent<AudioSource>().Stop();
        }
        if (shout)
        {
            AudioSource.PlayClipAtPoint(shoutAudioClip, transform.position);
        }
    }

}

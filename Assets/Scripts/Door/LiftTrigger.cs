using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftTrigger : MonoBehaviour
{
    public float timeToDoorsClose = 2f;
    public float timeToLiftStart = 3f;
    public float timeToEndLevel = 3.5f;
    public float liftSpeed = 3f;
    private GameObject player;
    private Animator playerAnim;
    private HashIDs hash;
    private CameraMovement camMovement;
    private SceneFadeInOut sceneFadeIn0ut;
    private LiftDoorsTracking liftDoorsTracking;
    private bool playerInLift;
    private float timer = 0;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player);
        playerAnim = player.GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();
        camMovement = Camera.main.gameObject.GetComponent<CameraMovement>();
        sceneFadeIn0ut = GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<SceneFadeInOut>();
        liftDoorsTracking = GetComponent<LiftDoorsTracking>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInLift = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        playerInLift = false;
        timer = 0f;
    }
    void Update()
    {
        if (playerInLift)
            LiftActivation();
        if (timer<timeToDoorsClose)
        {
            liftDoorsTracking.DoorFollowing();
        }
        else
        {
            liftDoorsTracking.CloseDoors();
        }
    }
    void LiftActivation()
    {
        timer += Time.deltaTime;
        if (timer >= timeToLiftStart)
        {
            playerAnim.SetFloat(hash.speedFloat, 0f);
            camMovement.enabled = false;
            player.transform.parent = transform;
            transform.Translate(Vector3.up * liftSpeed * Time.deltaTime);
            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Play();
            if (timer >= timeToEndLevel)
                sceneFadeIn0ut.EndScene();
            Debug.Log(timer);
        }
    }
}

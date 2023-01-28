using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashIDs : MonoBehaviour
{
    //死亡动画
    public int DyingState;
    //过渡到死亡的参数的ID
    public int Dead;
    public int locomotionState;
    public int shoutState;
    public int speedFloat;
    public int sneakingBool;
    public int shoutingBool;
    public int playerInsightBool;
    public int shotFloat;
    public int aimWeightFloat;
    public int angularSpeedFloat;
    public int openBool;
    private void Awake()
    {
        DyingState = Animator.StringToHash("Base Layer.Dying");
        Dead = Animator.StringToHash("Dead");
        locomotionState = Animator.StringToHash("Base Layer.Locomotion"); 
        shoutState = Animator.StringToHash("Shouting.Shout");
        speedFloat = Animator.StringToHash("Speed");
        sneakingBool = Animator.StringToHash("Sneaking"); 
        shoutingBool = Animator.StringToHash("Shouting");
        playerInsightBool = Animator.StringToHash("PlayerInSight"); 
        shotFloat = Animator.StringToHash("Shot");
        aimWeightFloat = Animator.StringToHash("AimWeight");
        angularSpeedFloat = Animator.StringToHash("AngularSpeed");
        openBool = Animator.StringToHash("Open");

    }
}

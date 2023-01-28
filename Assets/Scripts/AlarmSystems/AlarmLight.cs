using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 警报灯的变化
/// </summary>
public class AlarmLight : MonoBehaviour
{
    //警报灯
    private Light alarmLight; 
    //警报灯的从大变到小或者从小变到大的衰减速度
    public float fadeSpeed = 2f;
    //警报灯的最高亮度
    public float hightIntensity = 2f;
    //警报灯的最低亮度
    public float lowIntensity = 0.5f;
    //警报灯的阈值，用于变化警报灯的亮度，大-小-大(如果当前亮度距离目标亮度小于阈值，就进行变化)
    public float changeMargin = 0.2f;
    //用于控制警报灯是否开启
    public bool alarmOn;
    //目标强度值
    private float targetIntensity;
   private void Awake()
    {
        alarmLight = GetComponent<Light>();
        alarmLight.intensity = 0f;
        targetIntensity = hightIntensity;
    }
    void Update()
    {
        if (alarmOn)
        {
            alarmLight.intensity = Mathf.Lerp(alarmLight.intensity, targetIntensity, fadeSpeed * Time.deltaTime);
            CheckTargetIntensity();
        }
        else
        {
           alarmLight.intensity = Mathf.Lerp(alarmLight.intensity, 0f, fadeSpeed * Time.deltaTime);
        }
    }
    private void CheckTargetIntensity()
    {
        if (Mathf.Abs(targetIntensity-alarmLight.intensity)<changeMargin)
        {
            if (targetIntensity == hightIntensity)
            {
                targetIntensity = lowIntensity;
            }
            else
            {
                targetIntensity = hightIntensity;
            }
        }
        
    }
}

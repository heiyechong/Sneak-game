using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����Ƶı仯
/// </summary>
public class AlarmLight : MonoBehaviour
{
    //������
    private Light alarmLight; 
    //�����ƵĴӴ�䵽С���ߴ�С�䵽���˥���ٶ�
    public float fadeSpeed = 2f;
    //�����Ƶ��������
    public float hightIntensity = 2f;
    //�����Ƶ��������
    public float lowIntensity = 0.5f;
    //�����Ƶ���ֵ�����ڱ仯�����Ƶ����ȣ���-С-��(�����ǰ���Ⱦ���Ŀ������С����ֵ���ͽ��б仯)
    public float changeMargin = 0.2f;
    //���ڿ��ƾ������Ƿ���
    public bool alarmOn;
    //Ŀ��ǿ��ֵ
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �洢�������Ŀ����ҵ�λ��
/// </summary>
public class LastPlayerSighting : MonoBehaviour
{
    //������˼�⵽��λ�������Ĭ��ֵ����ô�Ͳ���׷���
    public Vector3 position = new Vector3(1000f, 1000f, 1000f);
    //�����������λ�õ�����㣨��Ĭ��ֵ��ͬ��
    public Vector3 rePosition = new Vector3(1000f, 1000f, 1000f);
   
    //���ƹ������
    public float lightHightIntensity = 0.25f;
    public float lightLowIntensity = 0f;
    public float fadeSpeed = 7f;

    //���ֵ��л��ٶ�
    public float musicFadeSpeed = 1f;

    
    private AlarmLight alarmLight;
    private Light mainlight;
    private AudioSource panicAudio;
    private AudioSource[] sirens;
    private void Awake()
    {
        alarmLight = GameObject.FindGameObjectWithTag(Tags.alarm).GetComponent<AlarmLight>();
        //��ȡ���ƹ�
        mainlight = GameObject.FindGameObjectWithTag(Tags.mainLight).GetComponent<Light>();
        panicAudio = transform.Find("secondaryMusic").GetComponent<AudioSource>();
        //������������
        GameObject[] sirenGameObject = GameObject.FindGameObjectsWithTag(Tags.siren);
        sirens = new AudioSource[sirenGameObject.Length];
        for (int i = 0; i < sirens.Length; i++)
        {
            sirens[i] = sirenGameObject[i].GetComponent<AudioSource>();
        }
    }
    private void Update()
    {
        SwitchAlarms();
        MusicFading();
    }
    /// <summary>
    /// ����������ұ�¶��ſ����������λ�ò�����reposition
    /// �л��ƹ�   ������رվ�����
    /// </summary>
    void SwitchAlarms()
    {
        alarmLight.alarmOn = (position != rePosition);
        //���ƹ�Ҫ�ﵽ��ǿ��
        float newIntensity;

        if (position!= rePosition)
        {
            newIntensity = lightLowIntensity;
        }
        else
        {
            newIntensity = lightHightIntensity;
        }
        mainlight.intensity = Mathf.Lerp(mainlight.intensity, newIntensity, fadeSpeed * Time.deltaTime);
        for (int i = 0; i < sirens.Length; i++)
        {
            if (position!=rePosition && !sirens[i].isPlaying)
            {
                sirens[i].Play();
            }
            else if (position == rePosition)
            {
                sirens[i].Stop();
            }
        }
    }
    /// <summary>
    /// �л�����
    /// </summary>
    void MusicFading()
    {
        if (position != rePosition)
        {
            GetComponent<AudioSource>().volume = Mathf.Lerp(GetComponent<AudioSource>().volume, 0f, musicFadeSpeed * Time.deltaTime);
            panicAudio.volume = Mathf.Lerp(panicAudio.volume, 0.8f, musicFadeSpeed * Time.deltaTime);
        }
        else
        {
            GetComponent<AudioSource>().volume = Mathf.Lerp(GetComponent<AudioSource>().volume, 0.8f, musicFadeSpeed * Time.deltaTime);
            panicAudio.volume = Mathf.Lerp(panicAudio.volume, 0f, musicFadeSpeed * Time.deltaTime);
        }
    }
}

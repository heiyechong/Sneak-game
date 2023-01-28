using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 存储敌人最后目击玩家的位置
/// </summary>
public class LastPlayerSighting : MonoBehaviour
{
    //如果敌人检测到的位置是这个默认值，那么就不会追玩家
    public Vector3 position = new Vector3(1000f, 1000f, 1000f);
    //用来重置玩家位置的坐标点（与默认值相同）
    public Vector3 rePosition = new Vector3(1000f, 1000f, 1000f);
   
    //主灯光的亮度
    public float lightHightIntensity = 0.25f;
    public float lightLowIntensity = 0f;
    public float fadeSpeed = 7f;

    //音乐的切换速度
    public float musicFadeSpeed = 1f;

    
    private AlarmLight alarmLight;
    private Light mainlight;
    private AudioSource panicAudio;
    private AudioSource[] sirens;
    private void Awake()
    {
        alarmLight = GameObject.FindGameObjectWithTag(Tags.alarm).GetComponent<AlarmLight>();
        //获取主灯光
        mainlight = GameObject.FindGameObjectWithTag(Tags.mainLight).GetComponent<Light>();
        panicAudio = transform.Find("secondaryMusic").GetComponent<AudioSource>();
        //警报喇叭数组
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
    /// 警报器在玩家暴露后才开启，即玩家位置不等于reposition
    /// 切换灯光   开启或关闭警报器
    /// </summary>
    void SwitchAlarms()
    {
        alarmLight.alarmOn = (position != rePosition);
        //主灯光要达到的强度
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
    /// 切换音乐
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

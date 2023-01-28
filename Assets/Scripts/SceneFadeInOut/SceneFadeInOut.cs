using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 控制游戏开始时或游戏结束时界面透明和黑之间变化
/// </summary>
public class SceneFadeInOut : MonoBehaviour
{
    public float fadeSpeed = 1.5f;
    private bool sceneStarting = true;
    private Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
        image.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
    }

    private void Start()
    {
        if (sceneStarting)
        {
            StartScene();
        }
    }
    private void FadeToClear()
    {
        //渐变成透明
        image.color = Color.Lerp(image.color, Color.clear, fadeSpeed * Time.deltaTime);
    }
    private void FadeToBlack()
    {
        //渐变成黑色
        image.color = Color.Lerp(image.color, Color.black, fadeSpeed * Time.deltaTime);
    }

    private void StartScene()
    {
        FadeToClear();
        if (image.color.a <= 0.05f)
        {
            image.color = Color.clear; 
            image.enabled = false;
            sceneStarting = false;
        }

    }
    /// <summary>
    /// 因为这个函数不在这个脚本运行，所以要设置成public让其他脚本调用
    /// </summary>
    public void EndScene()
    {
        image.enabled = true;
        FadeToBlack();
        if (image.color.a>=0.95f)
        {
            image.color = Color.black;
            SceneManager.LoadScene(0);
           
        }
    }
}

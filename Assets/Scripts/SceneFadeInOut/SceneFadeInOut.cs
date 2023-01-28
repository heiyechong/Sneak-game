using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ������Ϸ��ʼʱ����Ϸ����ʱ����͸���ͺ�֮��仯
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
        //�����͸��
        image.color = Color.Lerp(image.color, Color.clear, fadeSpeed * Time.deltaTime);
    }
    private void FadeToBlack()
    {
        //����ɺ�ɫ
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
    /// ��Ϊ���������������ű����У�����Ҫ���ó�public�������ű�����
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

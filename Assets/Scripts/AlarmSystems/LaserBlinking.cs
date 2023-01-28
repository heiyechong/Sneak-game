using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//º§π‚√≈
public class LaserBlinking : MonoBehaviour
{
    public float onTime;
    public float offTime;

    private float timer;

    private Light light;
    private MeshRenderer renderer;
    private void Awake()
    {
        light = GetComponent<Light>();
        renderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (renderer.enabled && timer >= onTime)
        {
            SwitchBeam();
        }
        else if (!renderer.enabled && timer >= offTime)
        {
            SwitchBeam();
        }

    }
    void SwitchBeam()
    {
        timer = 0f;
        light.enabled = !renderer.enabled;
        renderer.enabled = !renderer.enabled;

    }
}

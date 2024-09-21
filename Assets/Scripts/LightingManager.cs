using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LightingManager : MonoBehaviour
{
    [ColorUsageAttribute(true,true)] [SerializeField] Color startColor, endColor, menuColor;
    [SerializeField] Vector3 lightStartRotation, lightEndRotation, lightMenuRotation;
    [SerializeField] Vector3 clockPointerStartRotation, clockPointerEndRotation, clockPointerMenuRotation;
    [SerializeField] Transform directionallight, clock;
    bool changing;
    float time;

    public void StartDay(){
        changing = true;
        RenderSettings.ambientLight = startColor;
        directionallight.localEulerAngles = clockPointerStartRotation;
        clock.localEulerAngles = lightStartRotation;
    }

    public void EndDay(){
        changing = false;
        RenderSettings.ambientLight = menuColor;
        directionallight.localEulerAngles = lightMenuRotation;
        clock.localEulerAngles = clockPointerMenuRotation;
    }

    void Update()
    {
        if(changing){
            time = GameManager.Instance.TimeOfDay();
            RenderSettings.ambientLight = Color.Lerp(startColor, endColor, time);
            directionallight.localEulerAngles = Vector3.Lerp(lightStartRotation, lightEndRotation, time);
            clock.localEulerAngles = Vector3.Lerp(clockPointerStartRotation, clockPointerEndRotation, time);
        }
    }
}

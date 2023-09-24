using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EdibleGlobalEffects : MonoBehaviour
{
    public enum EffectType { None, SlowPlayer, StunEnemies, LightsOff, LightsOn }
    public enum EffectStatus { Done, Executing, Reverting }

    [Header("References")]
    public Light2D globalLight;

    [Header("General settings")]
    public EffectType currentEffectType = EffectType.None;
    public EffectStatus currentEffectStatus = EffectStatus.Done;
    public float currentEffectTime = 0;
    public float currentEffectDurationTime = 0;

    [Header("Lights default settings")]
    [Range(0, 1)] public float lightsDefaultIntensity = 0.5f;

    [Header("Lights OFF settings")]
    [Range(1, 10)] public float lightsOffDuration = 3;
    [Range(0.5f, 0)] public float lightsOffMiniumIntensity = 0;
    [Range(0.5f, 1)] public float lightsOffIntensityMultiplier = 0.3f;

    [Header("Lights ON settings")]
    [Range(1, 10)]  public float lightsOnDuration = 3;
    [Range(0.5f, 1)] public float lightsOnMiniumIntensity = 1;
    [Range(0.1f, 1)] public float lightsOnIntensityMultiplier = 0.3f;

    public void ExecEffect(EffectType type)
    {
        this.currentEffectType = type;
        this.currentEffectStatus = EffectStatus.Executing;
    }

    private void Update()
    {
        if (this.currentEffectStatus != EffectStatus.Done)
        {
            float delta = Time.deltaTime;
            this.currentEffectTime += delta;

            switch (currentEffectType)
            {
                case EffectType.None:
                    this.currentEffectTime = 0;
                    break;
                case EffectType.SlowPlayer:
                    break;
                case EffectType.StunEnemies:
                    break;
                case EffectType.LightsOff:
                    LightsOnOff(delta);
                    break;
                case EffectType.LightsOn:
                    LightsOnOff(delta, true);
                    break;
            }

            // Finished executing & reverting
            if (this.currentEffectStatus == EffectStatus.Done)
            {
                this.currentEffectTime = 0;
                this.currentEffectType = EffectType.None;
            }
        }
    }

    private void LightsOnOff(float delta, bool isLightsOn = false)
    {
        float lightsMiniumIntensity = this.lightsOffMiniumIntensity;
        float currentEffectDurationTime = this.lightsOffDuration;
        float intensityMultiplier = this.lightsOffIntensityMultiplier;

        if (isLightsOn)
        {
            lightsMiniumIntensity = this.lightsOnMiniumIntensity;
            currentEffectDurationTime = this.lightsOnDuration;
            intensityMultiplier = this.lightsOnIntensityMultiplier;
        }

        // While executing
        if (this.currentEffectStatus == EffectStatus.Executing)
        {
            if (isLightsOn)
            {
                // Set data until intensity is reached
                if (this.globalLight.intensity < lightsMiniumIntensity)
                {
                    this.currentEffectDurationTime = currentEffectDurationTime;
                    this.currentEffectTime = 0;
                    this.globalLight.intensity += delta * intensityMultiplier;
                }

                // Effect timer
                else if (this.globalLight.intensity >= lightsMiniumIntensity && this.currentEffectTime < this.currentEffectDurationTime)
                {
                    this.globalLight.intensity = lightsMiniumIntensity;
                    this.currentEffectTime += delta * intensityMultiplier;
                }

                else
                    this.currentEffectStatus = EffectStatus.Reverting;
            }
            else
            {
                // Set data until intensity is reached
                if (this.globalLight.intensity > lightsMiniumIntensity)
                {
                    this.currentEffectDurationTime = currentEffectDurationTime;
                    this.currentEffectTime = 0;
                    this.globalLight.intensity -= delta * intensityMultiplier;
                }

                // Effect timer
                else if (this.globalLight.intensity <= lightsMiniumIntensity && this.currentEffectTime < this.currentEffectDurationTime)
                {
                    this.globalLight.intensity = lightsMiniumIntensity;
                    this.currentEffectTime += delta * intensityMultiplier;
                }

                else
                    this.currentEffectStatus = EffectStatus.Reverting;
            }
        }

        // While reversing
        else
        {
            if (isLightsOn)
            {
                if (this.globalLight.intensity > this.lightsDefaultIntensity)
                    this.globalLight.intensity -= delta * intensityMultiplier;

                else
                    this.currentEffectStatus = EffectStatus.Done;
            }
            else
            {
                if (this.globalLight.intensity < this.lightsDefaultIntensity)
                    this.globalLight.intensity += delta * intensityMultiplier;

                else
                    this.currentEffectStatus = EffectStatus.Done;
            }

        }
    }
}

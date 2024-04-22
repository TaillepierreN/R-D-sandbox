using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class postProcessBehavior : MonoBehaviour
{
    [SerializeField] private Volume postProcessVolume;
    private ColorAdjustments colorAdjustments;
    
    private void Start() {
        postProcessVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments);
    }
    public void GlowUp()
    {
        StartCoroutine(Glowroutine(100f, 3f));
    }
    private IEnumerator Glowroutine(float target, float duration)
    {
        float time = 0;
        float startSaturation = colorAdjustments.saturation.value;
        float endSaturation = startSaturation + target;

        while (time < duration)
        {
            colorAdjustments.saturation.value = Mathf.Lerp(startSaturation, endSaturation, time/duration);
            time += Time.deltaTime;
            yield return null;
        }
        colorAdjustments.saturation.value = endSaturation;
    }
}

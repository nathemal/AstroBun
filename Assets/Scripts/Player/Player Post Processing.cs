using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

public class PlayerPostProcessing : MonoBehaviour
{
    private PlayerHealthController player;
    [Header("Take Damage Post Processing Settings")]
    public Volume takeDamageVolume;
    private bool takeDamageFadeIn; //true for fade in, false for fade out
    public float takeDamageFadeInTime;
    public float takeDamageFadeOutTime;
    private float takeDamageTimer; // The temp timer value to do the fade timer
    private float startWeight;
    //public float durationOfFlashing;
    //public float maxIntensityOfFlashing;

    [Header("Low Health Post Processing Settings")]
    public Volume lowHealthVolume;
    [Range(0f, 1f)]

    [Header("To see low health effects on sceen:")]
    public float showLowHealthVolumePercentageThreshold;


    private void Start()
    {
        lowHealthVolume.weight = 0;
        takeDamageVolume.weight = 0;
        player = GetComponent<PlayerHealthController>();
    }


    private void Update()
    {
        if (!player.takeDamage && takeDamageVolume.weight > 0)
        {
            StartCoroutine(FadeOutVolume(takeDamageVolume, takeDamageFadeOutTime));
        }

        UpdatePostProcessingEffects();
    }

    //Based on the script from Game Design 2 unity 2nd workshop 
    public void UpdatePostProcessingEffects()
    {
        float healthProc = (1.0f - (player.currentHealth / player.maxHealth));

        if( healthProc >= showLowHealthVolumePercentageThreshold)
        {
            //Ensures smooth transition in post processing effects
            float healthDropBeyondThreshold = healthProc - showLowHealthVolumePercentageThreshold;
            float remainingHealthRange = (1 - showLowHealthVolumePercentageThreshold);

            float volumeWeight = healthDropBeyondThreshold / remainingHealthRange;

            //Control intensity of post processing effect
            //float maxValueOfFlashing = 0.5f * (1.2f - volumeWeight);
            //volumeWeight += 0.5f * Mathf.PingPong(Time.time, maxValueOfFlashing); //flashing
            lowHealthVolume.weight = Mathf.SmoothStep(0, 1, volumeWeight);

            StartCoroutine(FadeInVolume(lowHealthVolume, takeDamageFadeInTime));
        }
        else
        {
            //lowHealthVolume.weight = Mathf.Lerp(lowHealthVolume.weight, 0, Time.deltaTime * 3);
            StartCoroutine(FadeOutVolume(lowHealthVolume, takeDamageFadeOutTime)); //if the health proc is less than threshold in other words player isn't in critical condition, remove low health volume
        }


        if (player.takeDamage)
        {
            takeDamageTimer = 0; // Reset fade-out timer
            StopAllCoroutines();
            StartCoroutine(FadeInVolume(takeDamageVolume, takeDamageFadeInTime));
            //StartCoroutine(FllashingVolumeTakingDamage(durationOfFlashing, maxIntensityOfFlashing));
        }
        else
        {
            takeDamageTimer += Time.deltaTime;
            if (takeDamageTimer >= takeDamageFadeOutTime)
            {
                StopCoroutine(FadeInVolume(takeDamageVolume, takeDamageFadeInTime));
                StartCoroutine(FadeOutVolume(takeDamageVolume, takeDamageFadeOutTime));
                player.takeDamage = false;
            }
        }
    }

    public void ChangeVolumeWhenTakingDamage()
    {

        takeDamageFadeIn = true;
        takeDamageTimer = 0;
        StartCoroutine(FadeInVolume(takeDamageVolume, takeDamageFadeInTime));
    }


    public void ChangeVolumeBasedOnHeal()
    {
        takeDamageTimer = 0;
        takeDamageFadeIn = false;

        startWeight = takeDamageVolume.weight;
        player.takeDamage = false;

        StartCoroutine(FadeOutVolume(takeDamageVolume, takeDamageFadeOutTime));
        StartCoroutine(FadeOutVolume(lowHealthVolume, takeDamageFadeOutTime));
    }

    private IEnumerator FadeOutVolume(Volume volume, float duration)
    {
        float startWeight = volume.weight;
        float timeHasPassed = 0;

        while (timeHasPassed < duration)
        {
            timeHasPassed += Time.deltaTime;
            volume.weight = Mathf.Lerp(startWeight, 0, timeHasPassed / duration);

            yield return null;
        }

        volume.weight = 0;   
    }


    private IEnumerator FadeInVolume(Volume volume, float duration)
    {
        float startWeight = volume.weight;
        float timeHasPassed = 0;

        while (timeHasPassed < duration)
        {
            timeHasPassed += Time.deltaTime;
            volume.weight = Mathf.Lerp(startWeight, 1, timeHasPassed / duration);

            yield return null;
        }

        volume.weight = 1;
    }


    private IEnumerator FllashingVolumeTakingDamage(float duration, float maxIntensity)
    {
        float minValue = 0.3f;

        float timeHasPassed = 0;

        while (timeHasPassed < duration)
        {
            float flickerValue = Mathf.PingPong(Time.time * 3, maxIntensity);
            takeDamageVolume.weight = Mathf.Clamp(flickerValue, minValue, maxIntensity);

            timeHasPassed += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(FadeOutVolume(takeDamageVolume, takeDamageFadeOutTime));
    }
}

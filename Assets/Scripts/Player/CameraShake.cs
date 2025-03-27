using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class CameraShake : MonoBehaviour
{
    private float passedTime;

    public float durationOfShake;
    public float strenghtOfShake;

    private float CalculateCameraShakeTime()
    {
        //Calculate how much time has passed from beginning of last frame
        passedTime += Time.deltaTime;

        return passedTime;
    }

    public IEnumerator ShakeCamera(float duration, float strength)
    {
        //transform parent position into wanted possition

        Vector3 originalPosition = transform.parent.localPosition; // parent 

        passedTime = 0.0f;


        while (passedTime < duration)
        {
            float cameraOffsetX = Random.Range(-1.0f, 1.0f) * strength;
            float cameraOffsetY = Random.Range(-1.0f, 1.0f) * strength;

            transform.parent.localPosition = new Vector3(cameraOffsetX, cameraOffsetY, originalPosition.z);

            passedTime = CalculateCameraShakeTime();

            yield return null;
        }


        transform.localPosition = originalPosition;


    }
}

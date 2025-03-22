using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //private Camera sceneCamera;

    //float startShakeAmount;

    //bool shaking;
    //float _shakeAmount;
    //float _shakeTime;
    //bool _lerp;
    //float timer;

    //private void Start()
    //{
    //    sceneCamera = GetComponent<Camera>();
    //    if (sceneCamera.GetComponent<CinemachineBasicMultiChannelPerlin>() == null)
    //    {
    //        Debug.LogError("You are missing a CinemachineBasicMultiChannelPerlin component on your Cinemachine Camera!");
    //    }
    //    cameraNoise = sceneCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
    //    startShakeAmount = cameraNoise.AmplitudeGain; // Store the initially set noise amplitude, so we can always revert back to this
    //}

    //private void Update()
    //{
    //    if (shaking) // Check if camera is supposed to shake
    //    {
    //        timer += Time.deltaTime; // Start timer

    //        if (_lerp) // If lerp is true, camera shake tapers off smoothly
    //        {
    //            cameraNoise.AmplitudeGain = Mathf.SmoothStep(_shakeAmount, startShakeAmount, timer / _shakeTime); // Smooth change in camera shake
    //        }
    //        else
    //        {
    //            cameraNoise.AmplitudeGain = _shakeAmount; // If lerp is false, camera shake is set to the desired amplitude
    //        }

    //        if (timer >= _shakeTime) // Check if timer is over
    //        {
    //            cameraNoise.AmplitudeGain = startShakeAmount; // Set the camera shake amount to the original value
    //            shaking = false; // Reset variables
    //            timer = 0;
    //        }
    //    }
    //}

    //public void ShakeCamera(float shakeAmount, float shakeTime, bool lerp, bool overrideCurrentShake)
    //{
    //    if (overrideCurrentShake) // Check to see if the current shake should be overwritten
    //    {
    //        // Store values and start the shake timer in Update
    //        shaking = true;
    //        _shakeAmount = shakeAmount;
    //        _shakeTime = shakeTime;
    //        _lerp = lerp;
    //        timer = 0;
    //    }
    //}
}

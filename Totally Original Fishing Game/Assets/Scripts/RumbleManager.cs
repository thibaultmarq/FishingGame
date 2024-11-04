using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Rendering;

public class RumbleManager : MonoBehaviour
{

    private static RumbleManager instance = null;

    public static RumbleManager Instance => instance;


    private Gamepad pad;
    private Coroutine stopRumbleAfterDurationCoroutine;



    void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }


    }


    public void RumblePulse(float lowFreq, float highFreq, float duration)
    {
        pad = Gamepad.current;

        if (pad != null)
        {
            
            pad.SetMotorSpeeds(lowFreq, highFreq);

            stopRumbleAfterDurationCoroutine = StartCoroutine(StopRumble(duration, pad));

        }
        
    }


    private IEnumerator StopRumble(float duration, Gamepad pad)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            yield return null;

        }

        pad.SetMotorSpeeds(0, 0);


    }

}

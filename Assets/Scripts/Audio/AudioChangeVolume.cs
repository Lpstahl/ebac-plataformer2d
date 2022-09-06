using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioChangeVolume : MonoBehaviour
{
    public AudioMixer group;
    public string FloatParam = "MyExposedParam";

    public void ChangeValue(float f)
    {
        group.SetFloat(FloatParam, f);
    }
}

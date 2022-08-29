using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRandomPlay : MonoBehaviour
{
    public List<AudioClip> audioClipList;

    public List<AudioSource> audioSourceList;

    private int _index = 0;

    public void PlyaRandom()
    {
        if (_index >= audioSourceList.Count) _index = 0;
        var audioSource = audioSourceList[_index];


        audioSource.clip = audioClipList[Random.Range(0, audioClipList.Count)];
        audioSource.Play();

        _index++;
    }

 
}

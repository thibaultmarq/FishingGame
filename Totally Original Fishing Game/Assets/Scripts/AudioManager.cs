using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField]
    private List<AudioClip> audioList;

    [SerializeField]
    private AudioSource audioSource;


    private static AudioManager instance = null;

    public static AudioManager Instance => instance;


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



    public void PlayAudio(int index)
    {
        audioSource.clip = audioList[index];
        audioSource.Play();


    }


}

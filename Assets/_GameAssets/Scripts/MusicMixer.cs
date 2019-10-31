using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMixer : MonoBehaviour
{
    [SerializeField] AudioClip music;
    private GameObject musicManager;

    void Start()
    {
        musicManager = GameObject.Find("MusicManager");
        musicManager.GetComponent<AudioSource>().clip = music;
        musicManager.GetComponent<AudioSource>().Play();
    }
}

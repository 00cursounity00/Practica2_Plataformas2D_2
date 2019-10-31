using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    [SerializeField] Slider volumenSlider;
    [SerializeField] Text VolumenTexto;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.enabled = PlayerPrefs.GetInt("sound", 1) == 1 ? true : false;
        audioSource.volume = PlayerPrefs.GetFloat("volume", 1);
        DontDestroyOnLoad(this.gameObject);
    }

    public void CambiarVolumen()
    {
        audioSource.volume = volumenSlider.value;
        VolumenTexto.text = ("Volmen " + ((int)(volumenSlider.value * 100)) + "%");
    }
}

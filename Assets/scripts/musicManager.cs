using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class musicManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private Slider sliderVolumen;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat("volumen");
        if (sliderVolumen != null)
        {
            sliderVolumen.value = audioSource.volume;
        }
        

    }
    public void changeVolumen(float volumen)
    {
        PlayerPrefs.SetFloat("volumen",volumen);
        audioSource.volume=volumen;

    }

    


}

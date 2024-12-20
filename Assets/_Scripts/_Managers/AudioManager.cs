using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public float musicVolume;
    public float soundVolume;
    [SerializeField] private AudioSource _musica;
    [SerializeField] private AudioSource _sound;
    public void SaveMusica()
    {
        musicVolume = InicioManager.Instance._musicaSlider.value;
        _musica.volume = musicVolume;
        soundVolume = InicioManager.Instance._sonidoSlider.value;
        _sound.volume = soundVolume;
    }
    private void Update()
    {
        SaveMusica();
    }
}

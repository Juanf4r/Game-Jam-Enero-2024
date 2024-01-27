using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InicioManager : MonoBehaviour
{
    [SerializeField] private Slider _sonidoSlider;
    [SerializeField] private Slider _musicaSlider;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("SoundVolume"))
        {
            PlayerPrefs.SetFloat("SoundVolume", 1);
            Load();
        }
        else
        {
            Load();
        }

        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 1);
        }
        else
        {
            Load();
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void cambioDeEscena(int Escena)
    {
        SceneManager.LoadScene(Escena);
    }

    public void ChangeVolumeS()
    {
        AudioListener.volume = _sonidoSlider.value;
        SaveS();
    }
    public void ChangeVolumeM()
    {
        AudioListener.volume = _musicaSlider.value;
        SaveM();
    }
    private void Load()
    {
        _sonidoSlider.value = PlayerPrefs.GetFloat("SoundVolume");
        _musicaSlider.value = PlayerPrefs.GetFloat("MusicVolume");
    }
    private void SaveS()
    {
        PlayerPrefs.SetFloat("SoundVolume", _sonidoSlider.value);
    }
    private void SaveM()
    {
        PlayerPrefs.SetFloat("MusicVolume", _musicaSlider.value);
    }

}

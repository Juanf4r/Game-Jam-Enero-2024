using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InicioManager : MonoBehaviour
{
    [SerializeField] Slider sonidoSlider;
    [SerializeField] Slider musicaSlider;
    public void Exit()
    {
        Application.Quit();
    }

    public void cambioDeEscena(int Escena)
    {
        SceneManager.LoadScene(Escena);
    }

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

    public void ChangeVolumeS()
    {
        AudioListener.volume = sonidoSlider.value;
        SaveS();
    }
    public void ChangeVolumeM()
    {
        AudioListener.volume = musicaSlider.value;
        SaveM();
    }
    private void Load()
    {
        sonidoSlider.value = PlayerPrefs.GetFloat("SoundVolume");
        musicaSlider.value = PlayerPrefs.GetFloat("MusicVolume");
    }
    private void SaveS()
    {
        PlayerPrefs.SetFloat("SoundVolume", sonidoSlider.value);
    }
    private void SaveM()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicaSlider.value);
    }

}

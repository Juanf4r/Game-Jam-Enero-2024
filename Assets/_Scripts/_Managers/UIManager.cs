using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public StatsManager statsManager;

    public void OnSaveButtonClicked()
    {
        statsManager.SaveName(statsManager.username.text);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWindow : MonoBehaviour
{
    [SerializeField] private AudioSource closeWindow;

    public void CloseWindows()
    {
        closeWindow.Play();
    }
}

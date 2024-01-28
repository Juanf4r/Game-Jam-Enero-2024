using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SistemaDialogoAds : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject PanelDialogo;
    [SerializeField] private GameObject PanelMiniJuego;
    [SerializeField] private string[] lines;
    private float _textSpeed = 0.1f;
    private int _index = 0;

    private void Start()
    {
        dialogueText.text = string.Empty;
        PanelMiniJuego.SetActive(false);
        startDialogue();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (dialogueText.text == lines[_index])
            {
                NextLine();
            }

            else
            {
                StopAllCoroutines();
                dialogueText.text = lines[_index];
            }
        }
    }

    private void startDialogue()
    {
        _index = 0;
        StartCoroutine(WriteLine());
    }

    private IEnumerator WriteLine()
    {
        dialogueText.text = string.Empty;
        foreach (char letter in lines[_index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(_textSpeed);
        }
    }

    private void NextLine()
    {
        if (_index < lines.Length - 1)
        {
            _index++;
            dialogueText.text = string.Empty;
            StartCoroutine(WriteLine());
        }
        else
        {
            PanelDialogo.SetActive(false);
            PanelMiniJuego.SetActive(true);
            AdsManager.Instance.StartGame();
        }
    }
}

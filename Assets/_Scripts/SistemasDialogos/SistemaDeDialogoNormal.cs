using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SistemaDeDialogoNormal : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject PanelDialogo;
    public string[] lines;
    public float textSpeed = 0.1f;
    int index = 0;


    void Start()
    {
        dialogueText.text = string.Empty;
        startDialogue();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (dialogueText.text == lines[index])
            {
                NextLine();
            }

            else
            {
                StopAllCoroutines();
                dialogueText.text = lines[index];
            }
        }
    }

    public void startDialogue()
    {
        index = 0;
        StartCoroutine(WriteLine());
    }

    IEnumerator WriteLine()
    {
        dialogueText.text = string.Empty;
        foreach (char letter in lines[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(WriteLine());
        }
        else
        {
            PanelDialogo.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Prueba : MonoBehaviour
{
    public static Prueba Instancia;

    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI dialogueText1;
    public TextMeshProUGUI dialogueText2;
    public TextMeshProUGUI dialogueText3;
    public TextMeshProUGUI dialogueText4;
    public TextMeshProUGUI dialogueText5;
    public TextMeshProUGUI dialogueText6;
    [SerializeField] private GameObject PanelDialogo1;
    [SerializeField] private GameObject PanelDialogo2;
    [SerializeField] private GameObject PanelDialogo3;
    [SerializeField] private GameObject PanelDialogo4;
    [SerializeField] private GameObject PanelDialogo5;
    [SerializeField] private GameObject PanelDialogo6;
    public string[] lines;
    public string[] linesPanel1;
    public string[] linesPanel2;
    public string[] linesPanel3;
    public string[] linesPanel4;
    public string[] linesPanel5;
    public string[] linesPanel6;
    public float textSpeed = 0.1f;
    private int index = 0;
    public int contador = 0;


    private void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            Destroy(this);
        }
        else
        {
            Instancia = this;
        }
    }

    void Start()
    {   
        dialogueText.text = string.Empty;
        if (PlayerPrefs.HasKey("contador"))
        {
            contador = PlayerPrefs.GetInt("contador");
        }
        else
        {
            PlayerPrefs.SetInt("contador", 0);
            contador = PlayerPrefs.GetInt("contador");
        }

        contador++;
        Dialogos();
        Debug.Log(contador);
    }
    private void Dialogos()
    {
        switch (contador)
        {
            case 1:
                Debug.Log("Entre al case 1");
                PanelDialogo1.SetActive(true);
                lines = linesPanel1;
                dialogueText = dialogueText1;
                startDialogue();
                break;
            case 2:
                Debug.Log("Entre al case 2");
                PanelDialogo2.SetActive(true);
                lines = linesPanel2;
                dialogueText = dialogueText2;
                startDialogue();
                break;
            case 3:
                Debug.Log("Entre al case 3");
                PanelDialogo3.SetActive(true);
                lines = linesPanel3;
                dialogueText = dialogueText3;
                startDialogue();
                break;
            case 4:
                Debug.Log("Entre al case 4");
                PanelDialogo4.SetActive(true);
                lines = linesPanel4;
                dialogueText = dialogueText4;
                startDialogue();
                break;
            case 5:
                Debug.Log("Entre al case 5");
                PanelDialogo5.SetActive(true);
                lines = linesPanel5;
                dialogueText = dialogueText5;
                startDialogue();
                break;
            case 6:
                Debug.Log("Entre al case 6");
                PanelDialogo6.SetActive(true);
                lines = linesPanel6;
                dialogueText = dialogueText6;
                startDialogue();
                break;
        }
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
            switch (contador)
            {
                case 1:
                    PanelDialogo1.SetActive(false);
                    break;
                case 2:
                    PanelDialogo2.SetActive(false);
                    break;
                case 3:
                    PanelDialogo3.SetActive(false);
                    break;
                case 4:
                    PanelDialogo4.SetActive(false);
                    break;
                case 5:
                    PanelDialogo5.SetActive(false);
                    break;
                case 6:
                    PanelDialogo6.SetActive(false);
                    break;
            }
            //PanelDialogo.SetActive(false);
        }
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("contador", contador);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAndShow : MonoBehaviour
{
    [SerializeField] private GameObject showObject;

    private bool _switch = false;

    public void Show()
    {
        if(_switch == false)
        {
            showObject.SetActive(true);
            _switch = true;
        }
        else if(_switch)
        {
            showObject.SetActive(false);
            _switch = false;
        }
    }
}

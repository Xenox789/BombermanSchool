using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField saveInput;

    public void SetSaveFileName()
    {
        if (saveInput.text != "")
            GameManager.Instance.saveFileName = saveInput.text + ".bml";
    }
}

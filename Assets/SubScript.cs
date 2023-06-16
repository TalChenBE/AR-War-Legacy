using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubScript : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    void Start()
    {
        LangHelper.lang = "he";
        dropdown.onValueChanged.AddListener(delegate {
            updateLang(dropdown);
        });
    }

    // Update is called once per frame
    void updateLang(TMP_Dropdown change)
    {
        LangHelper.lang = change.captionImage.sprite.name;
    }
}

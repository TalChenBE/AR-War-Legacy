using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class exit : MonoBehaviour
{
    public Button exitBtn;

    void Start()
    {
        exitBtn.onClick.AddListener(BackToMenu);
    }

    void BackToMenu()
    {
        SceneManager.LoadScene("Main");
    }
}

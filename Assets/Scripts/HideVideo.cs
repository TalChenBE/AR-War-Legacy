using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;
using Keymaps;

public class HideVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject canvas;
    public TextMeshProUGUI ramthnia_name;
    public Button skipButton;
    public TextMeshProUGUI textButton;


    public bool isDestroy = false;
    public bool isPlayerStarted = false;


    private void Start()
    {
        skipButton.enabled = true;
        skipButton.onClick.AddListener(SkipVideo);
        SetTextName(textButton, "skip_btn");
    }


    void Update()
    {
        if (isPlayerStarted == false && videoPlayer.isPlaying == true)
        {
            // When the player is started, set this information
            isPlayerStarted = true;
        }
        if (!isDestroy && isPlayerStarted == true && videoPlayer.isPlaying == false)
        {
            // When the player stopped playing, remove it
            DestroyANDinit();

            SetTextName(ramthnia_name,"ramthnia_name");
        }
    }

    void SetTextName(TextMeshProUGUI textOBJ, string key)
    {
        if (Keymaps.Keymap_names.getLanguage().Equals("Hebrew"))
            textOBJ.isRightToLeftText = true;
        else
            textOBJ.isRightToLeftText = false;

        textOBJ.text = Keymaps.Keymap_names.getValue(key);
    }


    void SkipVideo()
    {
        DestroyANDinit();
        videoPlayer.Stop();

        SetTextName(ramthnia_name, "ramthnia_name");
    }

    void DestroyANDinit()
    {
        canvas.gameObject.SetActive(false);
        skipButton.gameObject.SetActive(false);

        isDestroy = true;

        isPlayerStarted = false;
    }
}

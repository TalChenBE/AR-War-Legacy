using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class HideVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject canvas;
    public GameObject canvas_names;
    public bool isPlayerStarted = false;
    public bool isDestroy = false;

    private void Start()
    {
        canvas_names.SetActive(false);

    }


    void Update()
    {
        if (isPlayerStarted == false && videoPlayer.isPlaying == true)
        {
            // When the player is started, set this information
            isPlayerStarted = true;
        }
        if (isPlayerStarted == true && videoPlayer.isPlaying == false)
        {
            // When the player stopped playing, remove it
            Destroy(canvas.gameObject);
            isDestroy = true;
            canvas_names.SetActive(true);
        }
    }
}

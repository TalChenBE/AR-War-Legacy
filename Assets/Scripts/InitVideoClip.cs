using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class InitVideoClip : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip[] videoClip;
    public GameObject centrious_GameObject;
    public GameObject canvas;

    private bool initVideo;
    private int indexVideo;

    // Start is called before the first frame update
    void Start()
    {
        // init the video clip
        videoPlayer.clip = videoClip[0];
        indexVideo = 1;

        initVideo = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (initVideo && centrious_GameObject.GetComponent<IsrealTankAttack>().sceneName == "play-background-video")
        {
            videoPlayer.clip = videoClip[1];
            indexVideo = 2;

            canvas.gameObject.SetActive(true);

            initVideo = false;

            videoPlayer.Play();
        }
        else if (indexVideo == 2 && centrious_GameObject.GetComponent<IsrealTankAttack>().sceneName == "syria-retreat")
            initVideo = true;

        if (initVideo && centrious_GameObject.GetComponent<IsrealTankAttack>().sceneName == "syria-retreat")
        {
            videoPlayer.clip = videoClip[2];
            indexVideo = 3;

            canvas.gameObject.SetActive(true);

            initVideo = false;

            videoPlayer.Play();
        }
    }
}

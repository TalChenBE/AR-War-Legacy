using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;
using Keymaps;
public class ActivateOverview : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip videoClip_Ramthnaia_1_battle;
    public VideoClip videoClip_Ramthnaia_2_battle;

    public Button overviewButton;
    public TextMeshProUGUI textButton;

    public GameObject canvas;

    public MoveAttackerIsraelTank moveAttackerIsraelTank;
    public MoveRetreatIsraelTank[] moveRetreatIsraelTank;
    public IsrealTankAttack isrealTankAttack;
    public HideVideo hideVideo;

    private bool initVideo;
    private bool isClicked;

    // Start is called before the first frame update
    void Start()
    {
        overviewButton.enabled = true;
        overviewButton.onClick.AddListener(Change_overview);
        isClicked = false;
        initVideo = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isClicked)
        {
            SetTextName(textButton, "AR_btn");
            if (initVideo && moveAttackerIsraelTank.isSceneActive)
            {
                moveAttackerIsraelTank.isOnPause = true;
                for (int i = 0; i < moveRetreatIsraelTank.Length; i++)
                    moveRetreatIsraelTank[i].isOnPause = true;

                videoPlayer.clip = videoClip_Ramthnaia_1_battle;
                canvas.gameObject.SetActive(true);

                videoPlayer.Play();

                initVideo = false;
                return;
            }

            if (initVideo && isrealTankAttack.isSceneActive)
            {
                isrealTankAttack.isOnPause = true;

                videoPlayer.clip = videoClip_Ramthnaia_2_battle;
                canvas.gameObject.SetActive(true);
                videoPlayer.Play();

                initVideo = false;
                return;
            }
          /*  if (!initVideo && videoPlayer.isPlaying == false)
            {
                //isClicked = false;
                moveAttackerIsraelTank.isOnPause = false;
                for (int i = 0; i < moveRetreatIsraelTank.Length; i++)
                    moveRetreatIsraelTank[i].isOnPause = false;
            }*/
        }
        else
        {
            initVideo = true;
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


    void Change_overview()
    {
        if (isClicked)
        {
            videoPlayer.Stop();

            canvas.gameObject.SetActive(false);

            moveAttackerIsraelTank.isOnPause = false;
            for (int i = 0; i < moveRetreatIsraelTank.Length; i++)
                moveRetreatIsraelTank[i].isOnPause = false;

            isrealTankAttack.isOnPause = false;

            SetTextName(textButton, "overview_btn");

            isClicked = false;
            initVideo = true;
        }
        else
            isClicked = true;

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;

public class SubtitlesScenes : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI subtitleText = default;
    public HideVideo hideVideo;
    public string scene;

    public static SubtitlesScenes instance;
    private string file_name;
    public bool isRTL { get; set; }
    
    public SubtitlesManager subtitleRowsScene2;
    public SubtitlesManager subtitleRowsScene1;
    public MoveAttackerIsraelTank moveAttackerIsraelTank;
    public IsrealTankAttack isrealTankAttack;

    public string language;

    private bool scene1Enter, scene2Enter;

    private void Awake()
    {
        instance = this;
        subtitleRowsScene1 = new SubtitlesManager();
        subtitleRowsScene2 = new SubtitlesManager();
        ClearSubtitles();
        language = LangHelper.lang;
        if (language.Equals("he") || language.Equals("ar"))
            isRTL = true;
        else isRTL = false;
        subtitleText.isRightToLeftText = isRTL;
        file_name = "subtitles.scene1." + language.ToString();
        TextAsset textAsset = Resources.Load<TextAsset>(file_name);
        subtitleRowsScene1 = JsonUtility.FromJson<SubtitlesManager>(textAsset.text);

        file_name = "subtitles.scene2." + language.ToString();
        textAsset = Resources.Load<TextAsset>(file_name);
        subtitleRowsScene2 = JsonUtility.FromJson<SubtitlesManager>(textAsset.text);

        scene1Enter = false;
        scene2Enter = false;
    }

    private void Update()
    {
        if (hideVideo.isDestroy == false)
            return;

        if (!scene1Enter && moveAttackerIsraelTank.isSceneActive)
        {
            System.Collections.IEnumerator enumerator = (subtitleRowsScene1.rows).GetEnumerator();
            if (enumerator.MoveNext())
                SetSubtitles(enumerator);
            else ClearSubtitles();
            scene1Enter = true;
        }

        if (!scene2Enter && isrealTankAttack.isSceneActive)
        {
            System.Collections.IEnumerator enumerator = (subtitleRowsScene2.rows).GetEnumerator();
            if (enumerator.MoveNext())
                SetSubtitles(enumerator);
            else ClearSubtitles();
            scene2Enter = true;
        }
    }

    public void SetSubtitles(System.Collections.IEnumerator enumerator)
    {
        subtitleText.text = ((SubtitleRow)enumerator.Current).line;
        StartCoroutine(ClearAfterSeconds(((SubtitleRow)enumerator.Current).durationSec, enumerator));
    }

    public void SetIsRightToLeft(bool isRightToLeft)
    {
        subtitleText.isRightToLeftText = isRightToLeft;
    }

    public void ClearSubtitles()
    {
        subtitleText.text = "";
    }

    private IEnumerator ClearAfterSeconds(float delay, System.Collections.IEnumerator enumerator)
    {
        yield return new WaitForSeconds(delay);
        ClearSubtitles();
        if (enumerator.MoveNext())
            SetSubtitles(enumerator);
    }
}

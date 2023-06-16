using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Keymaps;

public class MoveAttackerIsraelTank : MonoBehaviour
{
    public string sceneName;
    public float smooth;
    public float speed;
    public bool isBattleFinish;
    public bool isSceneActive;
    public bool isOnPause;

    public AudioSource StartRamthniaWar_audioSource;
    public AudioSource DescriptionBattle_audioSource;
    public AudioSource Battle_audioSource;
    public AudioSource DanonRetreat_audioSource;
    public AudioSource back2naphach_audioSource;

    public GameObject explosion;
    public GameObject flames;
    public GameObject smoke;
    public GameObject canvas;

    public Button overviewButton;
    public TextMeshProUGUI textButton;

    public TextMeshProUGUI text_date;
    public TextMeshProUGUI text_speaker_name;
    public TextMeshProUGUI Harel_name;

    public HideVideo hideVideo;


    // --- variables of the start-driving (=StartD) secene ---
    public Vector3[] StartD_pathPositions;
    public Vector3[] StartD_pathRotarions;

    private bool isfirstInitiated;
    private int currentWaypoint;
    private bool canPlay;
    private bool alreadySet;


    private GameObject priv_explosion;
    private GameObject priv_flames;
    private GameObject priv_smoke;


    // Start is called before the first frame update
    void Start()
    {
        // -- general definitions -- //
        overviewButton.gameObject.SetActive(false);
        if (LangHelper.lang.Equals("he"))
            Keymaps.Keymap_names.keymap_names_init("Hebrew");
        else
            Keymaps.Keymap_names.keymap_names_init("English");

        sceneName = "start-driving";
        smooth = 5.0f;
        speed = 1f;
        currentWaypoint = 0;
        canPlay = true;
        isBattleFinish = false;
        isfirstInitiated = true;
        isSceneActive = false;
        isOnPause = false;
        alreadySet = false;
        canvas.SetActive(false);
        SetTextName(text_date, "monday_afternoon");
        text_speaker_name.text = "";

        SetTextName(textButton, "overview_btn");


        // this is the starsing place of the tank.
        transform.position = new Vector3(6, -0.74f, 5.34f);

        // Rotate the cube by converting the angles into a quaternion.
        Quaternion target = Quaternion.Euler(1.4f, -78.83f, -1.9f);
        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
    }

    // Update is called once per frame
    void Update()
    {
        if (hideVideo.isDestroy == false)
            return;

        if (isOnPause)
        {
            if (StartRamthniaWar_audioSource.isPlaying) StartRamthniaWar_audioSource.Stop();
            else if (DescriptionBattle_audioSource.isPlaying) DescriptionBattle_audioSource.Stop();
            else if (Battle_audioSource.isPlaying) Battle_audioSource.Stop();
            else if (DanonRetreat_audioSource.isPlaying) DanonRetreat_audioSource.Stop();
            else if (back2naphach_audioSource.isPlaying) back2naphach_audioSource.Stop();

            canPlay = true;
            currentWaypoint = 0;
            sceneName = "start-driving";

            // this is the starsing place of the tank.
            transform.position = new Vector3(6, -0.74f, 5.34f);

            // Rotate the cube by converting the angles into a quaternion.
            Quaternion target = Quaternion.Euler(1.4f, -78.83f, -1.9f);
            // Dampen towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);

            return;
        }

        //sceneName = "end-monday"; // just for DEBAG!!

        switch (sceneName)
        {
            case "start-driving":
                isSceneActive = true;
                overviewButton.gameObject.SetActive(true);
                // If we have reached the end of the path, start over
                if (currentWaypoint < StartD_pathPositions.Length)
                {
                    // Move towards the current waypoint
                    transform.position = Vector3.MoveTowards(transform.position, StartD_pathPositions[currentWaypoint], speed * Time.deltaTime);
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(StartD_pathRotarions[currentWaypoint]), Time.deltaTime * smooth);

                    // Check if we have reached the current waypoint
                    if (transform.position == StartD_pathPositions[currentWaypoint])
                        // Move to the next waypoint
                        currentWaypoint++;

                    if (canPlay && StartRamthniaWar_audioSource != null)
                    {
                        StartRamthniaWar_audioSource.Play();
                        canPlay = false;
                    }
                }
                else
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(StartD_pathRotarions[currentWaypoint]), Time.deltaTime * smooth);
                    // Check if we have reached the current waypoint
                    if (!StartRamthniaWar_audioSource.isPlaying)
                    {
                        canPlay = true;
                        sceneName = "danon-description-battle";
                        currentWaypoint = 0;
                    }
                }
                break;

            case "danon-description-battle":
                // Danone start sooting, and then his tank 
                if (canPlay && DescriptionBattle_audioSource != null)
                {
                    DescriptionBattle_audioSource.Play();
                    SetTextName(text_speaker_name, "danon_name");

                    canPlay = false;
                }
                // if the audio is finish palying then move to the next scene.
                if (!DescriptionBattle_audioSource.isPlaying)
                {
                    canPlay = true;
                    sceneName = "battle";
                    
                }
                break;
            case "battle":
                // Danone start sooting, and then his tank 
                if (canPlay && Battle_audioSource != null)
                {
                    Battle_audioSource.Play();
                    canPlay = false;
                }

                // if the audio is finish palying then move to the next scene.
                if (isfirstInitiated && !Battle_audioSource.isPlaying)
                {
                    Quaternion upRotation = Quaternion.Euler(-90, 0, 0);
                    canPlay = true;
                    sceneName = "retreat-on-walk";

                    priv_explosion = Instantiate(explosion, transform.position, transform.rotation);
                    var particleConfig = priv_explosion.GetComponent<ParticleSystem>();
                    var particleConfigMain = particleConfig.main;
                    particleConfigMain.startSize = 1;
                    Destroy(priv_explosion, 1.5f);

                    priv_flames = Instantiate(flames, transform.position, upRotation);
                    particleConfig = priv_flames.GetComponent<ParticleSystem>();
                    particleConfigMain = particleConfig.main;
                    particleConfigMain.startSize = 0.5f;

                    priv_smoke = Instantiate(smoke, transform.position, upRotation);
                    particleConfig = priv_smoke.GetComponent<ParticleSystem>();
                    particleConfigMain = particleConfig.main;
                    particleConfigMain.startSize = 0.5f;
                    isfirstInitiated = false;
                    

                }
                break;

            case "retreat-on-walk":
                // Danone start sooting, and then his tank 
                if (canPlay && DanonRetreat_audioSource != null)
                {
                    DanonRetreat_audioSource.Play();
                    canPlay = false;

                    canvas.SetActive(true);
                }
                // if the audio is finish palying then move to the next scene.
                if (!DanonRetreat_audioSource.isPlaying)
                {
                    SetTextName(Harel_name, "harel_name");
                    canPlay = true;
                    sceneName = "drive-back-naphach";
                    Destroy(canvas, 0.5f);
                    text_speaker_name.text = "";
                    isBattleFinish = true;
                }
                break;

            case "drive-back-naphach":
                if (canPlay && back2naphach_audioSource != null)
                {
                    back2naphach_audioSource.Play();
                    canPlay = false;
                }
                break;

            case "end-monday":
                if (alreadySet == false)
                {
                    hideVideo.isDestroy = false;
                    alreadySet = true;
                }
                GetComponent<IsrealTankAttack>().sceneName = "play-background-video";

                Destroy(priv_flames, 2f);
                Destroy(priv_smoke, 2f);

                isSceneActive = false;

                break;

            default:
                isSceneActive = false;
                overviewButton.gameObject.SetActive(false);

                break;
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
}

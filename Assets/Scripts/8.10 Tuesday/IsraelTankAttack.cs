using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Keymaps;
using UnityEngine.SceneManagement;

public class IsrealTankAttack : MonoBehaviour
{
    public bool isBombed;
    public string sceneName;
    public int offsetX;
    public float smooth;
    public float speed;
    public bool isSceneActive;
    public bool isOnPause;

    public AudioSource Battle_audioSource;

    public TextMeshProUGUI text_date;
    public TextMeshProUGUI text_speaker_name;
    public TextMeshProUGUI text_Danon_Force;
    public TextMeshProUGUI text_Harel_Force;

    public GameObject explosion;
    public GameObject flames;
    public GameObject smoke;

    public HideVideo hideVideo;

    public Button overviewButton;
    public TextMeshProUGUI textButton;

    // --- variables of the start-driving (=StartD) secene ---
    public Vector3[] StartD_pathPositions;
    public Vector3[] StartD_pathRotarions;

    private bool isfirstInitiated;
    private int currentWaypoint;
    private bool canPlay;

    private GameObject priv_explosion;
    private GameObject priv_flames;
    private GameObject priv_smoke;

    // Start is called before the first frame update
    void Start()
    {
        if (offsetX != 0)
            //sceneName = "start-driving";
            sceneName = "play-background-video";
        smooth = 5.0f;
        speed = 1f;
        currentWaypoint = 0;
        canPlay = true;
        isSceneActive = false;
        isOnPause = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (isOnPause)
        {
            if (offsetX == 0)
            {
                if (Battle_audioSource.isPlaying) Battle_audioSource.Stop();

                canPlay = true;
            }


            // move the tank to the init location:
            // this is the starsing place of the tank.
            transform.position = new Vector3(6, -0.74f, 5.34f);

            // Rotate the cube by converting the angles into a quaternion.
            Quaternion target = Quaternion.Euler(1.4f, -78.83f, -1.9f);
            // Dampen towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
            sceneName = "start-driving";
            return;
        }


        switch (sceneName)
        {
            case "play-background-video":

                if (offsetX == 0)
                {
                    overviewButton.gameObject.SetActive(false);
                    SetTextName(text_date, "tuesday_dawn");
                    text_speaker_name.text = "";

                    // move the tank to the init location:
                    // this is the starsing place of the tank.
                    transform.position = new Vector3(6, -0.74f, 5.34f);

                    // Rotate the cube by converting the angles into a quaternion.
                    Quaternion target = Quaternion.Euler(1.4f, -78.83f, -1.9f);
                    // Dampen towards the target rotation
                    transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);

                }

                if (hideVideo.isDestroy == false)
                    return;
                sceneName = "start-driving";

                break;
        
            case "start-driving":

                isSceneActive = true;
                if (offsetX == 0)
                {
                    //overviewButton.gameObject.SetActive(true);
                    //hideVideo.isDestroy = false;
                }
                               
                // If we have reached the end of the path, start over
                if (currentWaypoint < 1)
                //if (currentWaypoint < StartD_pathPositions.Length)
                    {
                    // Move towards the current waypoint
                    transform.position = Vector3.MoveTowards(transform.position, StartD_pathPositions[currentWaypoint], speed * Time.deltaTime);
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(StartD_pathRotarions[currentWaypoint]), Time.deltaTime * smooth);

                    // Check if we have reached the current waypoint
                    if (transform.position == StartD_pathPositions[currentWaypoint])
                        // Move to the next waypoint
                        currentWaypoint++;
                }
                else
                {
                    if (offsetX == 0)
                    {
                        SetTextName(text_Danon_Force, "danon_force");
                        SetTextName(text_Harel_Force, "harel_force");
                    }
                    // play audio
                    if (canPlay && Battle_audioSource && Battle_audioSource != null)
                    {
                        Battle_audioSource.Play();
                        canPlay = false;
                    }

                    sceneName = "battle"; // this is just for now
                }
                break;

            case "battle":
               
                // if the var isBombed is true then this tank will exploed
                if (isBombed)
                {
                    float randomTime = Random.Range(5, 10);
                    Invoke("BombTank", randomTime);
                }

                if (Battle_audioSource && !Battle_audioSource.isPlaying)
                {
                    canPlay = true;
                    sceneName = "syria-retreat";
                    currentWaypoint = 0;
                }
                if (offsetX != 0)
                    sceneName = "syria-retreat";

                break;
           
            case "syria-retreat":
                if (offsetX == 0)
                    SetTextName(text_date, "tuesday_afternoon");

                if (offsetX == 0 && hideVideo.isDestroy == false)
                    return;

                sceneName = "win-the-war";
                break;

            case "win-the-war":
                //overviewButton.gameObject.SetActive(false);
                if (offsetX == 0) 
                    Invoke("exit", 36);
                break;
             
            default:
                isSceneActive = false;

                break;
        }
    }

    private void exit()
    {
        SceneManager.LoadScene("Main");
    }

    void SetTextName(TextMeshProUGUI textOBJ, string key)
    {
        if (Keymaps.Keymap_names.getLanguage().Equals("Hebrew"))
            textOBJ.isRightToLeftText = true;
        else
            textOBJ.isRightToLeftText = false;

        textOBJ.text = Keymaps.Keymap_names.getValue(key);
    }
    
    void BombTank()
    {
        Quaternion upRotation = Quaternion.Euler(-90, 0, 0);

        var particleConfig = explosion.GetComponent<ParticleSystem>();
        var particleConfigMain = particleConfig.main;
        particleConfigMain.startSize = 1f;
        priv_explosion = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(priv_explosion, 1.5f);

        particleConfig = flames.GetComponent<ParticleSystem>();
        particleConfigMain = particleConfig.main;
        particleConfigMain.startSize = 0.5f;
        priv_flames = Instantiate(flames, transform.position, upRotation);

        particleConfig = smoke.GetComponent<ParticleSystem>();
        particleConfigMain = particleConfig.main;
        particleConfigMain.startSize = 0.5f;
        priv_smoke = Instantiate(smoke, transform.position, upRotation);

        Remove_BombTank(7);

    }

    void Remove_BombTank( float duration)
    {
        Destroy(priv_flames, duration);
        Destroy(priv_smoke, duration);
    }
}

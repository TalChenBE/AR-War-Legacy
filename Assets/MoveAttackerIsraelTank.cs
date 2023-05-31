using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAttackerIsraelTank : MonoBehaviour
{
    public string sceneName;
    public float smooth;
    public float speed;
    public bool isBattleFinish;
    public AudioSource StartRamthniaWar_audioSource;
    public AudioSource DescriptionBattle_audioSource;
    public AudioSource Battle_audioSource;
    public AudioSource DanonRetreat_audioSource;
    public GameObject explosion;
    public GameObject flames;
    public GameObject smoke;
    public GameObject arrow;
    public HideVideo hideVideo;


    // --- variables of the start-driving (=StartD) secene ---
    public Vector3[] StartD_pathPositions;
    public Vector3[] StartD_pathRotarions;

    // --- variables of the retreat-on-walk (=RetreaOnW) secene ---
/*    public Vector3[] RetreaOnW_pathPositions;
    public Vector3[] RetreaOnW_pathRotarions;*/

    private bool isfirstInitiated;
    private int currentWaypoint;
    private bool canPlay;


    // Start is called before the first frame update
    void Start()
    {
        // -- general definitions -- //
        sceneName = "start-driving";
        smooth = 5.0f;
        speed = 1f;
        currentWaypoint = 0;
        canPlay = true;
        isBattleFinish = false;
        isfirstInitiated = true;
        arrow.SetActive(false);


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

        switch (sceneName)
        {
            case "start-driving":

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

                    explosion = Instantiate(explosion, transform.position, transform.rotation);
                    var particleConfig = explosion.GetComponent<ParticleSystem>();
                    var particleConfigMain = particleConfig.main;
                    particleConfigMain.startSize = 1;
                    Destroy(explosion, 1.5f);

                    flames = Instantiate(flames, transform.position, upRotation);
                    particleConfig = flames.GetComponent<ParticleSystem>();
                    particleConfigMain = particleConfig.main;
                    particleConfigMain.startSize = 0.5f;

                    smoke = Instantiate(smoke, transform.position, upRotation);
                    particleConfig = smoke.GetComponent<ParticleSystem>();
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
                    arrow.SetActive(true);
                }
                // if the audio is finish palying then move to the next scene.
                if (!DanonRetreat_audioSource.isPlaying)
                {
                    canPlay = true;
                    sceneName = "done";
                    isBattleFinish = true;
                }



                // If we have reached the end of the path, start over
                /*  if (currentWaypoint < RetreaOnW_pathPositions.Length)
                  {
                      // Move towards the current waypoint
                      transform.position = Vector3.MoveTowards(transform.position, RetreaOnW_pathPositions[currentWaypoint], speed * Time.deltaTime);
                      transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(RetreaOnW_pathRotarions[currentWaypoint]), Time.deltaTime * smooth);

                      // Check if we have reached the current waypoint
                      if (transform.position == RetreaOnW_pathPositions[currentWaypoint])
                          // Move to the next waypoint
                          currentWaypoint++;
                  }
                  else
                  {
                      // Check if we have reached the current waypoint
                      if (currentWaypoint == RetreaOnW_pathPositions.Length)
                      {
                          transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(RetreaOnW_pathRotarions[currentWaypoint]), Time.deltaTime * smooth);
                          //sceneName = "meet-danone";
                          sceneName = "end";
                          currentWaypoint = 0;
                      }
                  }*/
                break;
           
            default:
                break;
        }
    }
}

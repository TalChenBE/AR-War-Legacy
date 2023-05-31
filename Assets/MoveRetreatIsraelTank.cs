using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRetreatIsraelTank : MonoBehaviour
{
    public int offsetX;
    public float smooth;
    public float speed;
    public HideVideo hideVideo;
    public MoveAttackerIsraelTank moveAttackerIsraelTank;

    // --- variables of the start-driving (=StartD) secene ---
    public Vector3[] StartD_pathPositions;
    public Vector3[] StartD_pathRotarions;

    // --- variables of the retreat-tanks (=RetreaT) secene ---
    public Vector3[] RetreaT_pathPositions;
    public Vector3[] RetreaT_pathRotarions;

    // --- variables of the drive-back-naphach (=Back2Naphach) secene ---
    public Vector3[] Back2Naphach_pathPositions;
    public Vector3[] Back2Naphach_pathRotarions;

    private int currentWaypoint;
    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        // -- general definitions -- //
        sceneName = "start-driving";
        smooth = 5.0f;
        //speed = 1f;
        currentWaypoint = 0;

        //-- init the positon and rotaion --//
        // this is the starsing place of the tank.
        transform.position = new Vector3(6 + offsetX, -0.74f, 5.34f);

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

                  
                }
                else
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(StartD_pathRotarions[currentWaypoint]), Time.deltaTime * smooth);
                    // Check if we have reached the current waypoint
                    if (currentWaypoint == StartD_pathPositions.Length)
                    {
                        sceneName = "retreat-tanks";
                        currentWaypoint = 0;
                    }
                }
                break;
            case "retreat-tanks":
                // If we have reached the end of the path, start over
                if (currentWaypoint < RetreaT_pathPositions.Length)
                {
                    // Move towards the current waypoint
                    transform.position = Vector3.MoveTowards(transform.position, RetreaT_pathPositions[currentWaypoint],  Time.deltaTime* speed / 50);
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(RetreaT_pathRotarions[currentWaypoint]), Time.deltaTime * smooth);

                    // Check if we have reached the current waypoint
                    if (transform.position == RetreaT_pathPositions[currentWaypoint])
                        // Move to the next waypoint
                        currentWaypoint++;
                }
                else
                {
                    // Check if we have reached the current waypoint
                    if (currentWaypoint == RetreaT_pathPositions.Length)
                    {
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(RetreaT_pathRotarions[currentWaypoint]), Time.deltaTime * smooth);
                        //sceneName = "meet-danone";
                        sceneName = "meet-danone";
                        currentWaypoint = 0;
                    }
                }
                break;
            case "meet-danone":
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(Back2Naphach_pathRotarions[0]), Time.deltaTime * smooth / 2);
                // they sholde wait for danone and then pikup him from the vilige and than continue :)
                if (moveAttackerIsraelTank && moveAttackerIsraelTank.isBattleFinish)
                    sceneName = "drive-back-naphach";
                break;
            case "drive-back-naphach":
                if (currentWaypoint < Back2Naphach_pathPositions.Length)
                {
                    // Move towards the current waypoint
                    transform.position = Vector3.MoveTowards(transform.position, Back2Naphach_pathPositions[currentWaypoint], speed * Time.deltaTime);
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(Back2Naphach_pathRotarions[currentWaypoint]), Time.deltaTime * smooth);

                    // Check if we have reached the current waypoint
                    if (transform.position == Back2Naphach_pathPositions[currentWaypoint])
                        // Move to the next waypoint
                        currentWaypoint++;
                }
                else
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(Back2Naphach_pathRotarions[currentWaypoint]), Time.deltaTime * smooth);
                    // Check if we have reached the current waypoint
                    if (currentWaypoint == Back2Naphach_pathRotarions.Length)
                        sceneName = "default";
                }
                break;
            default:
                break;
        }
    }
}

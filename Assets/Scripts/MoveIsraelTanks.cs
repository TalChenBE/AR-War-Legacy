using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIsraelTanks : MonoBehaviour
{
    public int offsetX;
    public float smooth;
    public float speed;
    public Vector3 finalDestination;
    public Vector3 finalRotationDestination;
    public Vector3[] pathPositions;
    public Quaternion[] pathRotarions;
    private int currentWaypoint;

    // Start is called before the first frame update
    void Start()
    {
        smooth = 5.0f;
        speed = 1f;
        currentWaypoint = 0;
        pathPositions = new Vector3[3];
        pathRotarions = new Quaternion[3];

        pathPositions[0] = new Vector3(-0.4f, -0.67f, 5.34f);
        pathRotarions[0] = Quaternion.Euler(7, -58.69f, 1);

        pathPositions[1] = new Vector3(-7.53f, 0.36f, 5.232f);
        pathRotarions[1] = Quaternion.Euler(-3.3f, -77.33f, -7.26f);

        pathRotarions[2] = Quaternion.Euler(-12.04f, 26.57f, -10.3f);
        /* pathPositions[2] = new Vector3(-6.47f, 1.81f, 4.86f);*/

        /*pathRotarions[3] = Quaternion.Euler(-8f, 51.73f, -10.3f);*/
        /*finalDestination = new Vector3(-5.8f, 1.8f, 4.38f);*/
        /*finalRotationDestination = Quaternion.Euler(-12.04f, 44.55f, -10.3f);*/


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
        // pos1: -0.4f, -0.67f, 5.34f, rpt1: 7, -58.69f, 1
        // pos2: -7.53f, 0.36f, 5.232f, rpt2: -12.04f, 26.57f, -10.3f
        // pos3: -6.47f, 1.81f, 4.86f, rpt2: -8f, 51.73f, -10.3f

        // If we have reached the end of the path, start over
        if (currentWaypoint < pathPositions.Length - 1)
        {
            // Check if we have reached the current waypoint
            if (transform.position == pathPositions[currentWaypoint])
                // Move to the next waypoint
                currentWaypoint++;

            /*if (currentWaypoint == pathPositions.Length - 1 && offsetX != 0)
                transform.position = Vector3.MoveTowards(transform.position, finalDestination, speed * Time.deltaTime);
            else*/
            // Move towards the current waypoint
            transform.position = Vector3.MoveTowards(transform.position, pathPositions[currentWaypoint], speed * Time.deltaTime);


        }
        /*
                if (currentWaypoint == pathPositions.Length - 1 && offsetX == 1)
                    transform.rotation = Quaternion.Slerp(transform.rotation, finalRotationDestination, Time.deltaTime * smooth);
                else if (currentWaypoint == pathPositions.Length - 1 && offsetX == 2)
                else
                    transform.rotation = Quaternion.Slerp(transform.rotation, pathRotarions[currentWaypoint], Time.deltaTime * smooth);*/
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, finalDestination, speed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(finalRotationDestination), Time.deltaTime * smooth);
        }
        // Rotate to face the next waypoint

    }
}

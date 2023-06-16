using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateIsraelTank : MonoBehaviour
{

    public GameObject Centerious_OBJtank;

    public Vector3[] init_Positions;
    public Vector3[] init_Rotarions;

    public Vector3 init_Harel_Positions;
    public Vector3 init_Harel_Rotarions;

    public bool alredyGenerate;

    public ActivateOverview activateOverview;

    private MoveAttackerIsraelTank moveAttackerIsraelTank;
    private GameObject newCenterious_OBJtank;

    private Vector3[] Danon_Positions;
    private Vector3[] Danon_Rotarions;
    private bool[] Danon_Tank_bomb;

    private Vector3[] Harel_Positions;
    private Vector3[] Harel_Rotarions;
    private bool[] Harel_Tank_bomb;



    // Start is called before the first frame update
    void Start()
    {
        alredyGenerate = false;
        moveAttackerIsraelTank = GetComponent<MoveAttackerIsraelTank>();

        Danon_Tank_bomb = new bool[6];
        Danon_Tank_bomb[0] = false;
        Danon_Tank_bomb[1] = false;
        Danon_Tank_bomb[2] = true;
        Danon_Tank_bomb[3] = true;
        Danon_Tank_bomb[4] = false;
        Danon_Tank_bomb[5] = false;


        Danon_Positions = new Vector3[6];
        Danon_Positions[0] = new Vector3(-0.215f, 0.742f, 5.295f);
        Danon_Positions[1] = new Vector3(-0.105f, 1.253f, 5.137f);
        Danon_Positions[2] = new Vector3(-0.755f, 0.648f, 5.377f);
        Danon_Positions[3] = new Vector3(-0.396f, 1.553f, 5.326f);
        Danon_Positions[4] = new Vector3(0.191f, 1.59f, 5.082f);
        Danon_Positions[5] = new Vector3(-0.451f, 0.248f, 5.387f);

        Danon_Rotarions = new Vector3[6];
        Danon_Rotarions[0] = new Vector3(-11, -58.69f, 10);
        Danon_Rotarions[1] = new Vector3(-11, -58.69f, 10);
        Danon_Rotarions[2] = new Vector3(-11, -58.69f, 10);
        Danon_Rotarions[3] = new Vector3(-9.734f, -61.491f, 12.858f);
        Danon_Rotarions[4] = new Vector3(-5.641f, -57.251f, 14.702f);
        Danon_Rotarions[5] = new Vector3(-11, -58.69f, 10);

        // final position | rotation:
        Harel_Positions = new Vector3[5];
        Harel_Positions[0] = new Vector3(-8.25f, 0.716f, 5.123f);
        Harel_Positions[1] = new Vector3(-8.48f, 1.16f, 5.329f);
        Harel_Positions[2] = new Vector3(-7.51f, 0.74f, 5.329f);
        Harel_Positions[3] = new Vector3(-7.71f, 1.14f, 5.28f);
        Harel_Positions[4] = new Vector3(-7.94f, 1.53f, 5.35f);

        Harel_Rotarions = new Vector3[5];
        Harel_Rotarions[0] = new Vector3(350.93f, 52.57f, 346.2f);
        Harel_Rotarions[1] = new Vector3(351.57f, 51.05f, 346.25f);
        Harel_Rotarions[2] = new Vector3(348.51f, 49.83f, 345.5f);
        Harel_Rotarions[3] = new Vector3(353.16f, 47.12f, 346.81f);
        Harel_Rotarions[4] = new Vector3(351.79f, 52.37f, 343.18f);

        Harel_Tank_bomb = new bool[5];
        Harel_Tank_bomb[0] = false;
        Harel_Tank_bomb[1] = false;
        Harel_Tank_bomb[2] = false;
        Harel_Tank_bomb[3] = true;
        Harel_Tank_bomb[4] = true;


    }

    // Update is called once per frame
    void Update()
    {
        if (!alredyGenerate && moveAttackerIsraelTank.sceneName == "end-monday") 
        {
            alredyGenerate = true;

            // Danon force
            for (int i = 0; i < Danon_Positions.Length; i++)
            {
                Centerious_OBJtank.transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
                newCenterious_OBJtank = Instantiate(Centerious_OBJtank, init_Positions[i], Quaternion.Euler(init_Rotarions[i]));

                newCenterious_OBJtank.AddComponent<IsrealTankAttack>();
                newCenterious_OBJtank.GetComponent<IsrealTankAttack>().offsetX = i + 1;

                newCenterious_OBJtank.GetComponent<IsrealTankAttack>().hideVideo = moveAttackerIsraelTank.hideVideo;

                // init and add the positions and the rotation array
                // generate a new instance of the position and the rotation
                Vector3[] StartD_pathPositions = new Vector3[1];
                Vector3[] StartD_pathRotarions = new Vector3[1];

                activateOverview.isrealTankAttack = newCenterious_OBJtank.GetComponent<IsrealTankAttack>();

                StartD_pathPositions[0] = Danon_Positions[i];
                StartD_pathRotarions[0] = Danon_Rotarions[i];

                newCenterious_OBJtank.GetComponent<IsrealTankAttack>().StartD_pathPositions = StartD_pathPositions;
                newCenterious_OBJtank.GetComponent<IsrealTankAttack>().StartD_pathRotarions = StartD_pathRotarions;

                newCenterious_OBJtank.GetComponent<IsrealTankAttack>().isBombed = Danon_Tank_bomb[i];
                if(Danon_Tank_bomb[i])
                {
                    newCenterious_OBJtank.GetComponent<IsrealTankAttack>().explosion = moveAttackerIsraelTank.explosion;
                    newCenterious_OBJtank.GetComponent<IsrealTankAttack>().flames = moveAttackerIsraelTank.flames;
                    newCenterious_OBJtank.GetComponent<IsrealTankAttack>().smoke = moveAttackerIsraelTank.smoke;
                }
            }

            // Harel force:
            for (int i = 0; i < Harel_Positions.Length; i++)
            {
                // init position:
                // Vector3(-14.2980003,-0.143000007,4.83699989) | Vector3(1.31976759,84.6045914,359.654083)
                Vector3 init_pos = init_Harel_Positions;
                init_pos[0] += i;

                Centerious_OBJtank.transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
                newCenterious_OBJtank = Instantiate(Centerious_OBJtank, init_pos, Quaternion.Euler(init_Harel_Rotarions));

                newCenterious_OBJtank.AddComponent<IsrealTankAttack>();
                newCenterious_OBJtank.GetComponent<IsrealTankAttack>().offsetX = i + 1;

                newCenterious_OBJtank.GetComponent<IsrealTankAttack>().hideVideo = moveAttackerIsraelTank.hideVideo;

                activateOverview.isrealTankAttack = newCenterious_OBJtank.GetComponent<IsrealTankAttack>();


                // init and add the positions and the rotation array
                // generate a new instance of the position and the rotation
                Vector3[] StartD_pathPositions = new Vector3[1];
                Vector3[] StartD_pathRotarions = new Vector3[1];
        
                StartD_pathPositions[0] = Harel_Positions[i];
                StartD_pathRotarions[0] = Harel_Rotarions[i];

                newCenterious_OBJtank.GetComponent<IsrealTankAttack>().StartD_pathPositions = StartD_pathPositions;
                newCenterious_OBJtank.GetComponent<IsrealTankAttack>().StartD_pathRotarions = StartD_pathRotarions;

                newCenterious_OBJtank.GetComponent<IsrealTankAttack>().isBombed = Harel_Tank_bomb[i];
                if (Harel_Tank_bomb[i])
                {
                    newCenterious_OBJtank.GetComponent<IsrealTankAttack>().explosion = moveAttackerIsraelTank.explosion;
                    newCenterious_OBJtank.GetComponent<IsrealTankAttack>().flames = moveAttackerIsraelTank.flames;
                    newCenterious_OBJtank.GetComponent<IsrealTankAttack>().smoke = moveAttackerIsraelTank.smoke;
                }
            }

        }
        else if (alredyGenerate)
        {
            moveAttackerIsraelTank.sceneName = "default";
        }
    }
}

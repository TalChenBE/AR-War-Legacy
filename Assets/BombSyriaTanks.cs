using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyriaTank : MonoBehaviour
{
    public GameObject explosion;
    public GameObject flames;
    public GameObject smoke;

    public MoveAttackerIsraelTank moveAttackerIsraelTank;


    private bool isfirstInitiated;
    // Start is called before the first frame update
    void Start()
    {
        isfirstInitiated = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isfirstInitiated && moveAttackerIsraelTank.sceneName == "battle")
        {
            Quaternion upRotation = Quaternion.Euler(-90, 0, 0);
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

    }

}

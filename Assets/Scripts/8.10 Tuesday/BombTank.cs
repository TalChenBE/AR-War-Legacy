using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTank : MonoBehaviour
{
    public GameObject explosion;
    public GameObject flames;
    public GameObject smoke;

    public IsrealTankAttack isrealTankAttack;

    private bool isfirstInitiated;
    private bool canDestroy;

    // Start is called before the first frame update
    void Start()
    {
        isfirstInitiated = true;
        canDestroy = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isfirstInitiated && isrealTankAttack.sceneName == "battle")
        {
            Quaternion upRotation = Quaternion.Euler(-90, 0, 0);

            var particleConfig = explosion.GetComponent<ParticleSystem>();
            var particleConfigMain = particleConfig.main;
            particleConfigMain.startSize = 1f;
            explosion = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(explosion, 1.5f);

            particleConfig = flames.GetComponent<ParticleSystem>();
            particleConfigMain = particleConfig.main;
            particleConfigMain.startSize = 0.5f;
            flames = Instantiate(flames, transform.position, upRotation);
            particleConfigMain.startSize = 0.4f;

            particleConfig = smoke.GetComponent<ParticleSystem>();
            particleConfigMain = particleConfig.main;
            particleConfigMain.startSize = 0.5f;
            smoke = Instantiate(smoke, transform.position, upRotation);

            isfirstInitiated = false;
            canDestroy = true;
        }

        // destroy the flames and smoke for the next battle
        if (canDestroy && isrealTankAttack.sceneName == "end-battle")
        {
            Destroy(flames, 1.5f);
            Destroy(smoke, 1.5f);
            canDestroy = false;
        }

    }
}

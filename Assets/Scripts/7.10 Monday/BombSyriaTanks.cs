using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyriaTank : MonoBehaviour
{
    public GameObject explosion;
    public GameObject flames;
    public GameObject smoke;

    public MoveAttackerIsraelTank moveAttackerIsraelTank;
    public IsrealTankAttack isrealTankAttack;

    private bool isfirstInitiated;
    private bool canDestroy;


    private GameObject priv_explosion;
    private GameObject priv_flames;
    private GameObject priv_smoke;

    // Start is called before the first frame update
    void Start()
    {
        isfirstInitiated = true;
        canDestroy = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isfirstInitiated && (moveAttackerIsraelTank.sceneName == "battle"|| isrealTankAttack.sceneName == "battle"))
        {
            if (isrealTankAttack.sceneName == "battle")
            {
                float randomTime = Random.Range(5, 10);
                Invoke("BombTank", randomTime);
            }
            else
                BombTank();

            isfirstInitiated = false;
            canDestroy = true;
        }

        

        // destroy the flames and smoke for the next battle
        if (canDestroy && (moveAttackerIsraelTank.sceneName == "end-monday" || isrealTankAttack.sceneName == "syria-retreat"))
        {
            Remove_BombTank(0);

            isfirstInitiated = true;
            canDestroy = false;
        }

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
        particleConfigMain.startSize = 0.4f;
        priv_flames = Instantiate(flames, transform.position, upRotation);

        particleConfig = smoke.GetComponent<ParticleSystem>();
        particleConfigMain = particleConfig.main;
        particleConfigMain.startSize = 0.5f;
        priv_smoke = Instantiate(smoke, transform.position, upRotation);
    }

    void Remove_BombTank(float duration)
    {
        Destroy(priv_flames, duration);
        Destroy(priv_smoke, duration);
    }

}

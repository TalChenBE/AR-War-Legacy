using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startScene : MonoBehaviour
{
    public Button scene;
    public Button game;
    public AudioSource audio;

    void Start()
    {
        scene.onClick.AddListener(BattleScene);
        game.onClick.AddListener(GameScene);
    }

    void GameScene()
    {
        audio.Stop();
        SceneManager.LoadScene("BattleGame");
    }

    void BattleScene()
    {
        audio.Stop();
        SceneManager.LoadScene("SampleScene");
    }
}

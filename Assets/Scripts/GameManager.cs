using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] OrderManager orderManager;
    [SerializeField] LightingManager lightingManager;

    [SerializeField] GameObject[] gameObjectActiveOnPause;
    [SerializeField] GameObject[] gameObjectActiveOnGameplay;

    [SerializeField] float dayDuration = 150;
    bool play;
    float timer = 0f;

    HighScores highScores;
    float score;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_InputField scoresNameInput;
    [SerializeField] TMP_Text highScoresNameText, highScoresScoreText;
    [SerializeField] GameObject menuPanel, scorePanel;

    [SerializeField] String[] objectsTagsToDestoryAfterGame;
    [SerializeField] ResetTransform[] objectsResetTransform;


    void Awake()
    {
        if(Instance == null) 
        {
            DontDestroyOnLoad(gameObject); 
            Instance = this;
        } else if(Instance != this) 
        {
            Destroy(gameObject);
        }
    }

    void Start(){
        highScores = new HighScores();
        play = false;
        timer = 0;
        foreach(GameObject go in gameObjectActiveOnGameplay){
            go.SetActive(false);
        }
        objectsResetTransform = FindObjectsOfType<ResetTransform>();
    }

    void Update()
    {
        if(play){
            if(timer < dayDuration){
                timer += Time.deltaTime;
            }else{
                EndGame();
            }
        }
    }

    public void Play(){
        timer = 0;
        play = true;

        foreach(GameObject go in gameObjectActiveOnPause){
            go.SetActive(false);
        }
        foreach(GameObject go in gameObjectActiveOnGameplay){
            go.SetActive(true);
        }

        orderManager.ResetOrderAndShow();
        orderManager.ResetMoney();
        lightingManager.StartDay();
    }

    public void EndGame(){
        play = false;

        foreach(GameObject go in gameObjectActiveOnPause){
            go.SetActive(true);
        }
        foreach(GameObject go in gameObjectActiveOnGameplay){
            go.SetActive(false);
        }
        foreach(ResetTransform go in objectsResetTransform){
            go.SetTransformToDefault();
        }

        ShowScore();
        scoresNameInput.text = "";
        lightingManager.EndDay();
        DestoryGameObjects();
        StopParticles();
        StopAudioSources();
    }

    public float TimeOfDay(){
        return timer/dayDuration;
    }

    public void ShowScore(){
        score = orderManager.GetMoney();
        scoreText.text = score.ToString("0.00") + " $";
        menuPanel.SetActive(false);
        scorePanel.SetActive(true);
    }

    public void SaveScore(){
        highScores.Add(scoresNameInput.text, score);
        UpdateHighScores();
    }

    public void UpdateHighScores(){
        string text = "";
        foreach(var nameItem in highScores.Names){
            text += nameItem + "\n";
        }
        highScoresNameText.text = text;
        text = "";
        foreach(var scoreItem in highScores.Scores){
            text += scoreItem.ToString("0.00") + " $\n";
        }
        highScoresScoreText.text = text;
    }

    public void Exit(){
        Exit();
    }

    void DestoryGameObjects(){
        foreach(var tag in objectsTagsToDestoryAfterGame){
            foreach(var go in GameObject.FindGameObjectsWithTag(tag)){
                Destroy(go);
            }
        }
    }

    void StopAudioSources(){
        foreach(var audioSouce in FindObjectsOfType<AudioSource>()){
            if(audioSouce.tag != "MainCamera")
                audioSouce.Stop();
        }
    }

    void StopParticles(){
        foreach(var particleSystem in FindObjectsOfType<ParticleSystem>()){
            particleSystem.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
}

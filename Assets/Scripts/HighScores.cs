using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScores
{
    string[] names;
    float[] scores;

    public string[] Names{ 
        get{
            Load();
            return names;
        }}
    public float[] Scores{ 
        get{
            Load();
            return scores;
        }}

    public HighScores(){
        names = new string[10];
        scores = new float[10];
    }


    void Load(){
        string name;

        for (int i = 0; i < 10; i++) {
            name = PlayerPrefs.GetString("HighScoreName_" + i);
            if(name != ""){
                names[i] = name;
                scores[i] = PlayerPrefs.GetFloat("HighScore_" + i);
            }
            else{
                names[i] = "---";
                scores[i] = 0;
            }
        }
    }

    public void Add(string name, float score){
        int i = 9;
        while(i >= 0 && score > scores[i]){
            if(i != 0 && score > scores[i-1]){
                scores[i] = scores[i-1];
                names[i] = names[i-1];
            }else{
                scores[i] = score;
                names[i] = name;
            }
            i--;
        }

        Save();
    }

    void Save(){
        for (int i = 0; i < names.Length; i++) {
            PlayerPrefs.SetString("HighScoreName_" + i, names[i]);
            PlayerPrefs.SetFloat("HighScore_" + i, scores[i]);
        }
        PlayerPrefs.Save();
    }
}

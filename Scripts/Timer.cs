using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // time player and pause on death
    public Text timerText;
    private float startTime;
    public bool death = false;

    // fade to black and save time spent alive
    public GameObject BlackFade;
    public Text deathText;
    string currentDeathScoreString;
    float currentDeathScoreFloat;
    
    



    //death animations
    public Animator deathAnim;


	// Use this for initialization
	void Awake ()
    {
        Time.timeScale = 1;
        startTime = 0;

	}


    // Update is called once per frame
    void Update ()
    {
        float x = startTime + Time.timeSinceLevelLoad;
        

        if (death)
        {
            currentDeathScoreFloat = x;

            

            OnDeathTimer();

            BlackFade.SetActive(true);
            deathAnim.Play("BlackFadeOnDeath");

            // high score recorder
            if(PlayerPrefs.GetFloat("HighScore") < currentDeathScoreFloat)
            { 
                PlayerPrefs.SetFloat("HighScore", currentDeathScoreFloat);
            }

            return;
        }

        
        string Minutes = ((int)x / 60).ToString();
        string Seconds = (x % 60).ToString("f1");

        timerText.text = Minutes + ":" + Seconds;
	}





    public void OnDeathTimer()
    {        
        timerText.color = Color.yellow;
        Time.timeScale = 0;
        currentDeathScoreString = timerText.text;
        deathText.text = currentDeathScoreString;
    }

    void returnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}

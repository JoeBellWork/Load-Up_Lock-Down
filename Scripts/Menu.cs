using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    public Text HighscoreText;
    float a;
    double b;


    void Start()
    {
        a = (PlayerPrefs.GetFloat("HighScore"));

        string Minutes = ((int)a / 60).ToString();
        string Seconds = (a % 60).ToString("f0");
        HighscoreText.text = "HighScore  " + Minutes + "m:" + Seconds + "s";
    }

    public void storyMode()
    {
        Debug.Log("Story mode is not currently available while improvments and player-feel features are being added!");
    }


    public void arenaMode()
    {
        SceneManager.LoadScene("Arena");
    }

    public void exitGame()
    {
        Application.Quit();
    }






    //Controller detection and movement
    public bool useController;
    private int Xbox_One_Controller = 0;
    private int PS4_Controller = 0;
    // // // // //

    /// <MenuButtons>
    public Button story, Riot, Quit;
    public Text tStory, tRiot, tQuit;
    

    /// </MenuButtons>


    void Update()
    {
        //  detect if the user has moved the controler, touch a button or has moved the mouse
        //  set a bool to state which type of controls has been used.
        ////////////////////////////////////////////
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
            useController = false;
        }
        if (Input.GetAxisRaw("Mouse X") != 0.0f || Input.GetAxisRaw("Mouse Y") != 0.0f)
        {
            useController = false;
        }
        if (Input.GetAxisRaw("Xbox-Hor") != 0.0f || Input.GetAxisRaw("Xbox-Vert") != 0.0f || Input.GetAxisRaw("Play-Hor") != 0.0f || Input.GetAxisRaw("Play-Vert") != 0.0f)
        {
            useController = true;
        }
        if (Input.GetKey(KeyCode.JoystickButton0) ||
           Input.GetKey(KeyCode.JoystickButton1) ||
           Input.GetKey(KeyCode.JoystickButton2) ||
           Input.GetKey(KeyCode.JoystickButton3) ||
           Input.GetKey(KeyCode.JoystickButton4) ||
           Input.GetKey(KeyCode.JoystickButton5) ||
           Input.GetKey(KeyCode.JoystickButton6) ||
           Input.GetKey(KeyCode.JoystickButton7) ||
           Input.GetKey(KeyCode.JoystickButton8) ||
           Input.GetKey(KeyCode.JoystickButton9) ||
           Input.GetKey(KeyCode.JoystickButton10))
        {
            useController = true;
        }
        /////////////////////////////////////////////////


        if (useController)
        {
            tStory.text = "Story mode...(Press [])" + "              (coming Soon)";
            tRiot.text = "Riot mode..." + "(Press o)";
            tQuit.text = "Exit the Game!" + "(Press X)";


            string[] names = Input.GetJoystickNames();
            for (int x = 0; x < names.Length; x++)
            {
                if (names[x].Length == 19)
                {

                    PS4_Controller = 1;
                    Xbox_One_Controller = 0;
                }
                if (names[x].Length == 33)
                {

                    //set a controller bool to true
                    PS4_Controller = 0;
                    Xbox_One_Controller = 1;
                }
            }

            if (Xbox_One_Controller == 1)
            {

            }
            if (PS4_Controller == 1)
            {
                if(Input.GetKey(KeyCode.Joystick1Button0))
                {
                    story.onClick.Invoke();
                }
                if (Input.GetKey(KeyCode.Joystick1Button2))
                {
                    Riot.onClick.Invoke();
                }
                if (Input.GetKey(KeyCode.Joystick1Button1))
                {
                    Quit.onClick.Invoke();
                }
            }
        }
    }

}





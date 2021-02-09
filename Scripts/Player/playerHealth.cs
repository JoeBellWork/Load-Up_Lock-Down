using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class playerHealth : MonoBehaviour
{
    public float cur_Health, max_Health = 100f;
    public Image healthBar;
    public CameraShake cameraShake;

    //on hit flash
    public float flashTime;
    Color originalColour;
    public MeshRenderer rend;

    public bool alive = true;

    //camera shake script for camera shake on damage
    public float shakeDuration = 0.5f;

    void Start()
    {
        cur_Health = max_Health;
        SetHealthBar();

        //onHit flash
        originalColour = rend.material.color;
    }

    public void SetHealthBar()
    {
        float my_health = cur_Health / max_Health;
        healthBar.fillAmount = my_health;
    }

    public void TakeDamage(float amount)
    {
        FindObjectOfType<AudioManager>().play("PlayerOnHit");
        Flash();
        shake();
        cur_Health -= amount;
        SetHealthBar();



        if (cur_Health <= 0)
        {
            cur_Health = 0;
            alive = false;
        }

        if (alive == false)
        {
            GameObject gm = GameObject.Find("BlackFade");
            Timer timer = gm.GetComponent<Timer>();
            timer.death = true;


        }

    }

    void shake()
    {
        cameraShake.shake(shakeDuration);
    }

    //on Hit Flash
    void Flash()
    {
        rend.material.color = Color.red;
        Invoke("ResetColor",flashTime);
    }

   void ResetColor()
    {
        rend.material.color = originalColour;
    }
}

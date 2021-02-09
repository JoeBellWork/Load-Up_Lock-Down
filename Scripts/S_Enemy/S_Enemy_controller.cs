using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.AI;

public class S_Enemy_controller : MonoBehaviour
{
    public float lookRadius;

    Transform target;
    NavMeshAgent agent;

    public int health;
    public float damage;
    private int currentHealth;



    //colour changer
    public Material[] mats;
    Renderer rendH, rendA, rendF;
    public GameObject head, Arm, ForeArm;
    //colour changer end
   

    

    
    void Start()
    {
        StartCoroutine(healthTickDown());
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();

        currentHealth = health;


        //colour changer
        rendH = head.GetComponent<Renderer>();
        rendA = Arm.GetComponent<Renderer>();
        rendF = ForeArm.GetComponent<Renderer>();

        rendH.enabled = true;
        rendA.enabled = true;
        rendF.enabled = true;

        rendH.sharedMaterial = mats[0];
        rendA.sharedMaterial = mats[0];
        rendF.sharedMaterial = mats[0];
        // colour changer 
    }







    // Update is called once per frame
    void Update()
    {
        


        float distance = Vector3.Distance(target.position, transform.position);

        if (distance > lookRadius)
        {
            agent.SetDestination(target.position);
            FacePlayer();
        }
        if (distance <= lookRadius)
        {
            FacePlayer();
        }

        EnemyHealth();      

        
    }


    // sphere of sight
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

    }




    void FacePlayer()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 7f);
    }


    // enemy change colour and death of enemy
    void EnemyHealth()
    {
        if (currentHealth == 3)
        {
            rendH.sharedMaterial = mats[0];
            rendA.sharedMaterial = mats[0];
            rendF.sharedMaterial = mats[0];
        }

        if (currentHealth == 2)
        {
            rendH.sharedMaterial = mats[1];
            rendA.sharedMaterial = mats[1];
            rendF.sharedMaterial = mats[1];
        }

        if (currentHealth <= 1)
        {
            rendH.sharedMaterial = mats[2];
            rendA.sharedMaterial = mats[2];
            rendF.sharedMaterial = mats[2];
        }

        if (currentHealth <= 0)
        {
            FindObjectOfType<AudioManager>().play("EnemyDead");
            gameObject.SetActive(false);
        }
    }


    public void HurtEnemy(int damage)
    {
        currentHealth -= damage;
    }





    ///////////////////////
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<playerHealth>().TakeDamage(damage);

        }
    }  



    IEnumerator healthTickDown()
    {
        yield return new WaitForSeconds(30f);
        HurtEnemy(1);
        StartCoroutine(healthTickDown());
    }
   

}

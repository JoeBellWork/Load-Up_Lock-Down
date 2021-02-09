using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class M_EnemyController : MonoBehaviour
{
    public float lookRadius;

    Transform target;
    NavMeshAgent agent;

    public int health;
    public float damage;
    private int currentHealth;



    //colour changer
    public Material[] mats;
    Renderer rendH, rendL, rendR;
    public GameObject head, L_Arm, R_Arm;
    //colour changer end



    

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(healthTickDown());

        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();

        currentHealth = health;


        //colour changer
        rendH = head.GetComponent<Renderer>();
        rendH.enabled = true;
        rendH.sharedMaterial = mats[0];

        rendL = L_Arm.GetComponent<Renderer>();
        rendL.enabled = true;
        rendL.sharedMaterial = mats[0];

        rendR = R_Arm.GetComponent<Renderer>();
        rendR.enabled = true;
        rendR.sharedMaterial = mats[0];
        // colour changer end
    }
    // Update is called once per frame
    void Update ()
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
            rendL.sharedMaterial = mats[0];
            rendR.sharedMaterial = mats[0];
        }

        if (currentHealth == 2)
        {
            rendH.sharedMaterial = mats[1];
            rendL.sharedMaterial = mats[1];
            rendR.sharedMaterial = mats[1];
        }

         if (currentHealth <= 1)
         {
            rendH.sharedMaterial = mats[2];
            rendL.sharedMaterial = mats[2];
            rendR.sharedMaterial = mats[2];
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gran_Controller : MonoBehaviour
{
    public float Gran_Speed;
    private Rigidbody rb;
    public int damage;  



	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        this.rb.useGravity = false;
	}



	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.forward * Gran_Speed * Time.deltaTime);

    }


    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Die());
        if (other.gameObject.tag == "MEnemy" || other.gameObject.tag == "SEnemy")
        {
            if (other.gameObject.tag == "MEnemy")
            {
                Debug.Log("Hit " + other.tag);                 
                other.gameObject.GetComponent<M_EnemyController>().HurtEnemy(damage);
            }

             if (other.gameObject.tag == "SEnemy")
             {
                 Debug.Log("Hit " + other.tag);                    
                 other.gameObject.GetComponent<S_Enemy_controller>().HurtEnemy(damage);
             }           
            
        }  
        else if (other.gameObject.CompareTag("Wall"))
        {
            this.gameObject.SetActive(false);
        }
    }
    
    IEnumerator Die()
    {
        yield return new WaitForSeconds(3f);
        this.gameObject.SetActive(false);
    }
}

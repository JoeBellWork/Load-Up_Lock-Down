using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bulletSpeed;

    private Rigidbody rb;


    public int damagePerBullet;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        this.rb.useGravity = false;
	}



	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
	}



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Wall"))
        {
            this.gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "MEnemy")
        {
            this.gameObject.SetActive(false);
            other.gameObject.GetComponent<M_EnemyController>().HurtEnemy(damagePerBullet);
        }

        if (other.gameObject.tag == "SEnemy")
        {
            Debug.Log("HIT");
            this.gameObject.SetActive(false);
            other.gameObject.GetComponent<S_Enemy_controller>().HurtEnemy(damagePerBullet);
        }
    }

}

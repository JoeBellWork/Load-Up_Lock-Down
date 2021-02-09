using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlteredBullet : MonoBehaviour
{

    
    public int speed;
    public int damagePerBullet;
    float spawnTime;

    
    Transform myTransform;

    void Update()
    {

        myTransform.Translate(Vector3.forward * Time.deltaTime * speed);
        

        if (Time.time - spawnTime > 5)
        {
            AlteredSpawner.inst.missileList.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }


    


    public void Instantiate(Vector3 spawnLocation, Quaternion rot, int newSpeed, Transform mytransform, float spawntime)
    {
        myTransform = mytransform;
        this.transform.position = spawnLocation;
        myTransform.rotation = rot;
        newSpeed = speed;
        spawnTime = spawntime;
    }




    //onHit
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            AlteredSpawner.inst.missileList.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            AlteredSpawner.inst.missileList.Remove(this.gameObject);
            Destroy(this.gameObject);
            other.gameObject.GetComponent<playerHealth>().TakeDamage(damagePerBullet);
        }
    }

}

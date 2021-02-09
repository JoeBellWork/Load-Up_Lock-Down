using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlteredSpawner : MonoBehaviour
{
    public static AlteredSpawner inst;

    AlteredSpawner()
    {
        inst = this;
    }

    public GameObject fireLocation;
    public GameObject Missile;
    public List<GameObject> missileList = new List<GameObject>();
    public bool canFire = true;


    void Update()
    {
        if (canFire)
        {
            StartCoroutine(Shoot());
            Debug.Log("is true");
        }
    }

    void missleHold()
    {
        //Quaternion randRotation = Quaternion.Euler(fireLocation.transform.localRotation.x, fireLocation.transform.localRotation.y - (Random.Range(-10.0f, 10.0f)), fireLocation.transform.localRotation.z);


        Debug.Log("Has fired");
        GameObject missile = Instantiate(Missile);
        missileList.Add(missile);
        missile.GetComponent<AlteredBullet>().Instantiate(this.transform.position, this.transform.rotation, 10, missile.transform, Time.time);
        FindObjectOfType<AudioManager>().play("EnemyShoot");
    }

    IEnumerator Shoot()
    {
        Debug.Log("SHOOTING");
        canFire = false;
        missleHold();
        yield return new WaitForSeconds(1f);
        missleHold();
        yield return new WaitForSeconds(1f);
        missleHold();
        yield return new WaitForSeconds(3f);
        canFire = true;
    }
}

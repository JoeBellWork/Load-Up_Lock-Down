using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSingleton : MonoBehaviour
{
    public static WaveSingleton Inst;    
    public Transform Menemy, Senemy;
    public int mCount, sCount;
    public float rate;



    private void Awake()
    {
        Inst = this;
    }


    // Use this for initialization
    void Start ()
    {
		
	}



	
	// Update is called once per frame
	void Update ()
    {
		
	}



}

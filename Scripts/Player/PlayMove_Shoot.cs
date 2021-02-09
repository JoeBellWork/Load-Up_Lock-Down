using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayMove_Shoot : MonoBehaviour
{

    public float playerSpeed;
    private Rigidbody playerRB;

    private Vector3 playerMoveInput;
    private Vector3 playerMoveVelocity;



    private Camera mainCam;


    public Transform[] GunEndLocation;
    public bool canFire = true;
    public bool GrenFire = true;


    //grenade UI reload timer
    public Image specialReload;
    public Text specialReloadText;



    //Controller detection and movement
    public bool useController;
    private int Xbox_One_Controller = 0;
    private int PS4_Controller = 0;
    // // // // //


    // Use this for initialization
    void Start ()
    {
        playerRB = GetComponent<Rigidbody>();
        mainCam = FindObjectOfType<Camera>();
	}




	
	
	void Update ()
    {

        // recieve input to move player character
        playerMoveInput = new Vector3(Input.GetAxis("Horizontal"), 0f,Input.GetAxis("Vertical"));
        playerMoveVelocity = playerMoveInput * playerSpeed;


        //  detect if the user has moved the controler, touch a button or has moved the mouse
        //  set a bool to state which type of controls has been used.
        ////////////////////////////////////////////
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
            useController = false;
        }
        if(Input.GetAxisRaw("Mouse X") != 0.0f || Input.GetAxisRaw("Mouse Y") !=0.0f)
        {
            useController = false;
        }
        if(Input.GetAxisRaw("Xbox-Hor") != 0.0f || Input.GetAxisRaw("Xbox-Vert") != 0.0f || Input.GetAxisRaw("Play-Hor") != 0.0f || Input.GetAxisRaw("Play-Vert") != 0.0f)
        {
            useController = true;
        }
        if(Input.GetKey(KeyCode.JoystickButton0) ||
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










        //////////////////////////////////////////////////////////// use mouse and keyboard
        if (!useController)
        {
            //ray cast to rotate player
            Ray CamRay = mainCam.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;


            if (groundPlane.Raycast(CamRay, out rayLength))
            {
                Vector3 pointLook = CamRay.GetPoint(rayLength);
                Debug.DrawLine(CamRay.origin, pointLook, Color.cyan);

                transform.LookAt(new Vector3(pointLook.x, transform.position.y, pointLook.z));
            }


            //shooting function
            if (Input.GetKey(KeyCode.Mouse0) && canFire)
            {
                StartCoroutine(waitToFire());
            }

            if(Input.GetKey(KeyCode.Space) && GrenFire)
            {
                StartCoroutine(BladeGrenWait());
            }


            // state what controls the special attack uses
            specialReloadText.text = "(Space)";
        }///////////////////////////////////////////////////////////////////////////////////////














        //////////////////////////////////////////////////////////////////////////////////////////use controller
        if(useController)
        {
            // state what controls the special attack uses
            specialReloadText.text = "(Left Trigger)";


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
                Vector3 PlayerDirection = Vector3.right * Input.GetAxisRaw("Xbox-Hor") + Vector3.forward * -Input.GetAxisRaw("Xbox-Vert");
                if (PlayerDirection.sqrMagnitude > 0.0f)
                {
                    transform.rotation = Quaternion.LookRotation(PlayerDirection, Vector3.up);
                }

                if (Input.GetKey(KeyCode.Joystick1Button5) && canFire)
                {
                    StartCoroutine(waitToFire());
                }

                if(Input.GetKey(KeyCode.Joystick1Button4))
                {
                    StartCoroutine(BladeGrenWait());
                }
            }
            else if (PS4_Controller == 1)
            {
                Vector3 PlayerDirection = Vector3.right * Input.GetAxisRaw("Play-Hor") + Vector3.forward * -Input.GetAxisRaw("Play-Vert");
                if (PlayerDirection.sqrMagnitude > 0.0f)
                {
                    transform.rotation = Quaternion.LookRotation(PlayerDirection, Vector3.up);
                }

                if (Input.GetKey(KeyCode.Joystick1Button7) && canFire)
                {
                    StartCoroutine(waitToFire());
                }
                if (Input.GetKey(KeyCode.Joystick1Button6) && GrenFire)
                {
                    StartCoroutine(BladeGrenWait());
                }
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////

	}


    void FireBullet()
    {
        for (int i = 0; i < GunEndLocation.Length; i++)
        {
            GameObject Bullet = OBJpool.objectInstance.GetPooledOBJ();
            if(Bullet != null)
            {
                Bullet.transform.position = GunEndLocation[i].position;
                Bullet.transform.rotation = GunEndLocation[i].rotation;
                Bullet.SetActive(true);
            }
        }
    }

    IEnumerator waitToFire()
    {
        FindObjectOfType<AudioManager>().play("PlayerShoot");
        FireBullet();
        canFire = false;
        yield return new WaitForSeconds(0.5f);
        canFire = true;
    }




    void FireBladeGren()
    {
        for (int i = 0; i < GunEndLocation.Length; i++)
        {
            GameObject BladeGren = Gran_OBJ.objectInstance.GetPooledOBJ();
            if (BladeGren !=null)
            {
                BladeGren.transform.position = GunEndLocation[i].position;
                BladeGren.transform.rotation = GunEndLocation[i].rotation;
                BladeGren.SetActive(true);
            }
        }
    }

    IEnumerator BladeGrenWait()
    {
        FindObjectOfType<AudioManager>().play("Blade");
        StartCoroutine(reload());
        FireBladeGren();
        GrenFire = false;
        yield return new WaitForSeconds(30f);
        GrenFire = true;
    }


    void FixedUpdate()
    {
        playerRB.velocity = playerMoveVelocity;
    }
    



    //grenade special reload function
    IEnumerator reload()
    {
        specialReload.fillAmount = 0f;
        yield return new WaitForSeconds(3f);
        specialReload.fillAmount = 0.1f;
        yield return new WaitForSeconds(3f);
        specialReload.fillAmount = 0.2f;
        yield return new WaitForSeconds(3f);
        specialReload.fillAmount = 0.3f;
        yield return new WaitForSeconds(3f);
        specialReload.fillAmount = 0.4f;
        yield return new WaitForSeconds(3f);
        specialReload.fillAmount = 0.5f;
        yield return new WaitForSeconds(3f);
        specialReload.fillAmount = 0.6f;
        yield return new WaitForSeconds(3f);
        specialReload.fillAmount = 0.7f;
        yield return new WaitForSeconds(3f);
        specialReload.fillAmount = 0.8f;
        yield return new WaitForSeconds(3f);
        specialReload.fillAmount = 0.9f;
        yield return new WaitForSeconds(3f);
        specialReload.fillAmount = 1f;
    }

}

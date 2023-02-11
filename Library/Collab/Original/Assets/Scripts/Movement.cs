using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Movement : NetworkBehaviour
{

    //Photon
    public GameObject playerCamera;
    public GameObject UICanavs;
    public MeshRenderer mr;
    public Text PlayerNameText;


    public Material[] material;
    public GameObject g;

    public float moveSpeed = 20000f;
    public float jumpPower = 8.0f;
    public float jumpHeight = 1.0f;

    public float timer = 1f;

    public float gravity = -9.81f;

    private float delay;
    public float delayPeriod = 1f;
    private bool isJumping = false;

    public Joystick joystick;

    public CharacterController controller;

    public Rigidbody floor;

    private Vector3 velocity;


    private void Start()
    {

        if (isLocalPlayer)
        {

            //mr.material = material[NetworkServer.connections.Count];
            Debug.Log("Camera on");
            playerCamera.SetActive(true);
            UICanavs.SetActive(true);
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        if (isLocalPlayer)
        {
            checkInput();
        }
    }


    void OnControllerColliderHit(ControllerColliderHit col)
    {

        if (col.collider.tag == "floor")
        {
            //Material floorMaterial = new Material(col.gameObject.GetComponent<MeshRenderer>().material);
            //col.gameObject.GetComponent<MeshRenderer>().material


            changeFloorMaterial(col.gameObject);
            
            

            //StartCoroutine(floorFall(col, 0.7f));
            //Destroy(col.gameObject, 2f);
        }
    }

    //[Command]
    void changeFloorMaterial(GameObject floor)
    {
        floor.GetComponent<MeshRenderer>().material = material[0];
    }

    private IEnumerator floorFall(ControllerColliderHit col, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        try
        {
            if(col.gameObject.GetComponent<Rigidbody>() == null)
            col.gameObject.AddComponent<Rigidbody>();
        }
        catch
        {
            
         }
    }


    public void jump()
    {
        isJumping = true;
    }

    private void checkInput()
    {
        //keyboard inputs
        if (Input.GetKey("w"))
        {
            controller.Move(new Vector3(10 * Time.deltaTime, 0, 0));
        }



        //float horizontalSpeed = joystick.GetComponent<Joystick>().Horizontal;
        float verticalSpeed = joystick.Vertical;
        float horizontalSpeed = joystick.Horizontal;
        //float verticalSpeed = joystick.GetComponent<Joystick>().Vertical;


        Vector3 direction = new Vector3(horizontalSpeed, velocity.y, verticalSpeed).normalized;

        if (direction.magnitude >= 0.1f)
        {
            controller.Move(direction * moveSpeed * Time.deltaTime);
        }

        if (delay > 0)
            delay -= 1 * Time.deltaTime;

        if (isJumping && controller.isGrounded)
            velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        isJumping = false;

        if (controller.isGrounded && !isJumping)
            velocity.y = -0.2f;
    }

}

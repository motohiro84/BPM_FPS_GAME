using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
  public GameObject Player;
  public GameObject Camera;
  public float speed;
  private Vector3 playerPos;
  private Transform PlayerTransform;
  private Transform CameraTransform;
  private float ii;
  private Animator animator;
  private Rigidbody rb;
  private float jumpForce = 300.0f;
  private bool Ground = true;

  // Use this for initialization
  void Start()
  {
    rb = GameObject.Find("Player").GetComponent<Rigidbody>();
    PlayerTransform = GameObject.Find("Camera").transform.parent;
    playerPos = GameObject.Find("Player").transform.position;
    animator = GameObject.Find("Player").GetComponent<Animator>();
    CameraTransform = GameObject.Find("Camera").GetComponent<Transform>();
    CameraTransform.transform.Rotate(360f, 0, 0);
  }

  void Update()
  {
    float X_Rotation = Input.GetAxis("Mouse X");
    float Y_Rotation = Input.GetAxis("Mouse Y");
    PlayerTransform.transform.Rotate(0, X_Rotation, 0);

    ii = (Camera.transform.localEulerAngles.x - Y_Rotation) * Mathf.Deg2Rad;
    ii = Mathf.Sin(ii);

    if (ii > -0.6f && ii < 0.6f)
    {
      CameraTransform.transform.Rotate(-Y_Rotation, 0, 0);
    }


    Move();

  }

  void Move()
  {
    if (Ground == true)
    {
      if (Input.GetButtonDown("Jump"))
      {
        rb.AddForce(transform.up * jumpForce, ForceMode.Force);
        //rb.velocity += transform.up * jumpForce * time.deltatime / mass
        //rb.velocity += transform.up * jumpForce / mass
        Ground = false;
      }

    }


    float x = Input.GetAxisRaw("Horizontal");
    float z = Input.GetAxisRaw("Vertical");

    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
    {
      Vector3 dir = (transform.right * x) + (transform.forward * z);
      PlayerTransform.transform.position += dir * speed * Time.deltaTime;
    }


  }


  void OnTriggerEnter(Collider col)
  {
    if (col.gameObject.tag == "Ground")
    {
      if (Ground == false)
        Ground = true;
    }
  }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
  public GameObject Player;
  public GameObject hito;
  public float speed;
  private Vector3 playerPos;
  private Transform PlayerTransform;
  private Animator animator;
  private Rigidbody rb;
  private float jumpForce = 300.0f;
  private bool Ground = true;

  void Start()
  {
    rb = Player.GetComponent<Rigidbody>();
    PlayerTransform = hito.transform.parent;
    playerPos = Player.transform.position;
    animator = Player.GetComponent<Animator>();
  }


  void Update()
  {

    Move();

  }

  void Move()
  {
    if (Ground == true)
    {
      if (Input.GetButtonDown("Jump"))
      {
        rb.AddForce(transform.up * jumpForce, ForceMode.Acceleration);
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
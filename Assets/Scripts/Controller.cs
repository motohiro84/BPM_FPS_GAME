using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Controller : MonoBehaviour
{
  public static bool moveEnabled = true;
  public static bool attackEnabled = true;
  public static bool coolDown = false;

  public int maxHp = 20;
  int hp;

  public GameObject Boss;
  Animator animator;
  RuntimeAnimatorController runtimeAnimatorController;
  AnimationClip[] animationClips;
  Rigidbody rb;
  float[] values = { 0, 0.5f, 1f };

  public Transform target;
  Events Events;
  private float jumpForce = 800.0f;
  private Vector3 velocity;
  private Vector3 direction;

  public int Hp
  {
    set
    {
      hp = Mathf.Clamp(value, 0, maxHp);
      if (hp <= 0)
      {
        StartCoroutine(Death());
      }
    }
    get
    {
      return hp;
    }
  }

  void Start()
  {
    Events = GetComponent<Events>();
    animator = GetComponent<Animator>();
    runtimeAnimatorController = animator.runtimeAnimatorController;
    animationClips = runtimeAnimatorController.animationClips;

    Events.AddAnimationEvents();

    rb = GetComponent<Rigidbody>();
    // StartCoroutine(CheckState());

    InitCharacter();
    Boss.SetActive(false);
  }

  void Update()
  {
    transform.LookAt(target.position);
  }

  void InitCharacter()
  {
    Hp = maxHp;
  }

  float calcMagnitude()
  {
    Vector3 vec = target.position - transform.position;
    float dist = vec.magnitude;

    return dist;
  }

  public IEnumerator CheckState()
  {
    while (true)
    {
      var dist = calcMagnitude();
      var animStateInfo = animator.GetCurrentAnimatorStateInfo(0);

      if (dist <= 4f && attackEnabled &&
          animStateInfo.fullPathHash != Animator.StringToHash("Base Layer.Attack"))
      {
        StartCoroutine(Attack());
      }
      if (dist > 4f && coolDown &&
          animStateInfo.fullPathHash != Animator.StringToHash("Base Layer.Jump"))
      {
        coolDown = false;
        rb.AddForce(transform.up * jumpForce, ForceMode.Acceleration);
        StartCoroutine(Jump());
        Invoke("CoolDown", 20f);
      }
      else if (dist > 4f && moveEnabled &&
          animStateInfo.fullPathHash != Animator.StringToHash("Base Layer.Move"))
      {
        StartCoroutine(Move());
      }

      yield return new WaitForSeconds(1f);
    }
  }

  void CoolDown()
  {
    coolDown = true;
  }

  IEnumerator Death()
  {
    animator.SetTrigger("Death");
    StopCoroutine(CheckState());
    yield return new WaitForSeconds(3f);
    BossSpawner.bossNum--;
    this.gameObject.SetActive(false);
  }

  IEnumerator Attack()
  {
    animator.SetTrigger("Attack");
    animator.SetFloat("AttackBlend", values[UnityEngine.Random.Range(0, values.Length)]);

    yield return null;
  }

  IEnumerator Move()
  {
    animator.SetTrigger("Move");

    float a = 1;
    float strength = 6f;
    while (a > 0.3f)
    {
      var v = target.position - transform.position;
      var d = v.magnitude;

      a = Mathf.Clamp(d, 0f, 10f) * 0.1f;
      animator.SetFloat("MoveBlend", a);

      rb.velocity = v.normalized * a * strength;
      yield return null;
    }
    yield break;
  }
  IEnumerator Jump()
  {
    animator.SetTrigger("Jump");

    float a = 1;
    float strength = 6f;
    {
      var v = target.position - transform.position;
      var d = v.magnitude;

      a = Mathf.Clamp(d, 0f, 10f) * 0.1f;

      rb.velocity = v.normalized * a * strength;
      yield return null;
    }

    yield break;
  }



}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]

public class EnemyController : MonoBehaviour
{
  public bool moveEnabled = true;
  [SerializeField]
  int maxHp = 2;
  [SerializeField]
  int HpDamage = 25;
  [SerializeField]
  int attackInterval = 1;
  [SerializeField]
  string targetTag = "Player";
  [SerializeField]
  float deadTime = 3;
  bool attacking = false;
  int hp;
  float moveSpeed;
  Animator animator;
  BoxCollider boxCollider;
  Rigidbody rigidBody;
  NavMeshAgent agent;
  Transform target;
  GameManager gameManager;
  PlayerHP player;
  public int Hp
  {
    set
    {
      hp = Mathf.Clamp(value, 0, maxHp);
      if (hp <= 0)
      {
        StartCoroutine(Dead());
      }
    }
    get
    {
      return hp;
    }
  }
  void Start()
  {
    animator = GetComponent<Animator>();
    boxCollider = GetComponent<BoxCollider>();
    rigidBody = GetComponent<Rigidbody>();
    agent = GetComponent<NavMeshAgent>();
    target = GameObject.FindGameObjectWithTag(targetTag).transform;
    gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerHP>();
    InitCharacter();
  }
  void Update()
  {
    if (moveEnabled)
    {
      Move();
    }
    else
    {
      Stop();
    }
  }
  void InitCharacter()
  {
    Hp = maxHp;
    moveSpeed = agent.speed;
  }
  void Move()
  {
    agent.speed = moveSpeed;
    animator.SetFloat("Speed", agent.speed, 0.1f, Time.deltaTime);
    agent.SetDestination(target.position);
    rigidBody.velocity = agent.desiredVelocity;
  }
  void Stop()
  {
    agent.speed = 0;
    animator.SetFloat("Speed", agent.speed, 0.1f, Time.deltaTime);
  }
  IEnumerator Dead()
  {
    moveEnabled = false;
    Stop();
    animator.SetTrigger("Dead");
    yield return new WaitForSeconds(deadTime);
    EnemySpawner.enemyNum--;
    this.gameObject.SetActive(false);
  }
  private void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.tag == "Player")
    {
      StartCoroutine(AttackTimer());
    }
  }
  IEnumerator AttackTimer()
  {
    if (!attacking)
    {
      attacking = true;
      moveEnabled = false;
      animator.SetTrigger("Attack");
      player.HP -= HpDamage;
      yield return new WaitForSeconds(attackInterval);
      attacking = false;
      moveEnabled = true;
    }
    yield return null;
  }
}
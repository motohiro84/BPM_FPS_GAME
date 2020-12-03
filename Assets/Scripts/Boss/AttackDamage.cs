using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamage : MonoBehaviour
{
  [SerializeField]
  int HpDamage = 10;
  PlayerHP player;

  // Start is called before the first frame update
  void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerHP>();
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      player.HP -= HpDamage;
    }
  }

}

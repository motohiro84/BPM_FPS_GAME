using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
  public GameObject Boss;
  public GameObject BossGate;
  public GameObject BossPoint;
  private float x;
  private float z;
  int num = 1;
  public static int bossNum;

  void Start()
  {
    bossNum = 5;
  }

  void Update()
  {
    BossNumCount();
  }

  void BossNumCount()
  {

    if (bossNum == 1)
    {
      BossGate.SetActive(false);
    }
    if (bossNum == 0)
    {
      OneTimeGate(false);
      bossNum--;
    }
  }

  void OnTriggerEnter(Collider other)
  {
    if (num == 1)
    {
      if (other.CompareTag("Player"))
      {
        BossEnemy();
        OneTimeGate(true);
        num--;
      }
    }
  }

  void BossEnemy()
  {
    Boss.transform.position = BossPoint.transform.position;
    Boss.SetActive(true);
  }

  void OneTimeGate(bool i)
  {
    foreach (GameObject g in GameManager.oneTime)
    {
      g.SetActive(i);
    }
  }

}

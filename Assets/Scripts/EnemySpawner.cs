using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
  public GameObject[] zombie1;
  public GameObject[] zombie2;
  public GameObject spawnPoint1;
  public GameObject spawnPoint2;
  private float x;
  private float z;
  private Vector3 SpawnPostion;
  int num = 1;
  public static int enemyNum;

  void Start()
  {
    enemyNum = 4;
    OneTimeGate(false);
  }

  void Update()
  {
    if (enemyNum == 0)
    {
      OneTimeGate(false);
      enemyNum--;
      BossSpawner.bossNum--;
    }
  }

  void OnTriggerEnter(Collider other)
  {
    if (num == 1)
    {
      if (other.CompareTag("Player"))
      {
        SpawnEnemy();
        num--;
        OneTimeGate(true);
      }
    }
  }

  void SpawnEnemy()
  {
    enemyNum = 4;
    foreach (GameObject i in zombie1)
    {
      EnemyController enemy = i.GetComponent<EnemyController>();
      enemy.Hp = 3;
      enemy.moveEnabled = true;
      x = Random.Range(-6, 6);
      z = Random.Range(-6, 6);
      SpawnPostion = new Vector3(spawnPoint1.transform.position.x + x, spawnPoint1.transform.position.y, spawnPoint1.transform.position.z + z);
      i.transform.position = SpawnPostion;
      i.SetActive(true);
      GameObject BaseZombie = i.transform.Find("Base HumanPelvis").gameObject;
      BaseZombie.SetActive(true);
    }
    if (spawnPoint2 != null)
    {
      foreach (GameObject i in zombie2)
      {
        EnemyController enemy = i.GetComponent<EnemyController>();
        enemy.Hp = 3;
        enemy.moveEnabled = true;
        x = Random.Range(-6, 6);
        z = Random.Range(-6, 6);
        SpawnPostion = new Vector3(spawnPoint2.transform.position.x + x, spawnPoint2.transform.position.y, spawnPoint2.transform.position.z + z);
        i.transform.position = SpawnPostion;
        i.SetActive(true);
        GameObject BaseZombie = i.transform.Find("Base HumanPelvis").gameObject;
        BaseZombie.SetActive(true);
      }
      enemyNum = 8;
    }
  }

  void OneTimeGate(bool i)
  {
    foreach (GameObject g in GameManager.oneTime)
    {
      g.SetActive(i);
    }
  }

}

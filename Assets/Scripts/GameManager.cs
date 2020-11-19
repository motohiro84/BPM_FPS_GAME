﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
  public GameObject Player;
  private PlayerMove playerMove;
  private PlayerCamera playerCamera;

  public FirstPersonGunController gunCOntroller;
  public Text centerText;
  private float waitTime = 2;

  [System.NonSerialized]
  public bool gameOver = false;


  void Start()
  {
    playerMove = Player.GetComponent<PlayerMove>();
    playerCamera = Player.GetComponent<PlayerCamera>();
    InitGame();
    StartCoroutine(GameStart());
  }
  void InitGame()
  {
    playerMove.enabled = false;
    playerCamera.enabled = true;
    gunCOntroller.shootEnabled = false;
  }
  public IEnumerator GameStart()
  {
    yield return new WaitForSeconds(waitTime);
    centerText.enabled = true;
    centerText.text = "3";
    yield return new WaitForSeconds(1);
    centerText.text = "2";
    yield return new WaitForSeconds(1);
    centerText.text = "1";
    yield return new WaitForSeconds(1);
    centerText.text = "GO!!";
    playerMove.enabled = true;
    playerCamera.enabled = true;
    gunCOntroller.shootEnabled = true;
    yield return new WaitForSeconds(1);
    centerText.text = "";
    centerText.enabled = false;
  }

  public IEnumerator GameOver()
  {
    gameOver = true;
    playerMove.enabled = false;
    playerCamera.enabled = false;
    gunCOntroller.shootEnabled = false;
    centerText.enabled = true;
    centerText.text = "Game Over";
    yield return new WaitForSeconds(waitTime);
    DestroyEnemies();
    centerText.text = "";
    centerText.enabled = false;
    gameOver = false;
  }
  void DestroyEnemies()
  {
    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
    foreach (GameObject enemy in enemies)
    {
      Destroy(enemy);
    }
  }
}
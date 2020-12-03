﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeponChange : MonoBehaviour
{
  public GameObject[] Wepon;
  public static int Key = 1;
  Motion motion1;
  Motion motion2;

  void Start()
  {
    Wepon[0].SetActive(true);
    FirstPersonGunController.damage = 2;
    motion1 = Wepon[0].GetComponent<Motion>();
    motion2 = Wepon[1].GetComponent<Motion>();
  }

  void Update()
  {
    Change();
  }

  void Change()
  {
    if (Wepon[1].activeSelf && Input.GetKeyDown(KeyCode.Alpha1))
    {
      Wepon[0].SetActive(true);
      Wepon[1].SetActive(false);
      Key = 1;
      FirstPersonGunController.damage = 2;
      motion1.IdleMotion();
    }
    else if (Wepon[0].activeSelf && Input.GetKeyDown(KeyCode.Alpha2))
    {
      Wepon[0].SetActive(false);
      Wepon[1].SetActive(true);
      Key = 2;
      FirstPersonGunController.damage = 1;
      motion2.IdleMotion();
    }


  }

}

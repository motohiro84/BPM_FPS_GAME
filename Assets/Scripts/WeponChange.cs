﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeponChange : MonoBehaviour
{
  public GameObject[] Wepon;
  //   public GameObject Wepon1;
  //   public GameObject Wepon2;
  void Start()
  {
    Wepon[0].SetActive(true);
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
    }
    else if (Wepon[0].activeSelf && Input.GetKeyDown(KeyCode.Alpha2))
    {
      Wepon[0].SetActive(false);
      Wepon[1].SetActive(true);
    }
  }

}

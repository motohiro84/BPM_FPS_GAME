﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackChara : MonoBehaviour
{
  public static bool Key;
  void Start()
  {
    Key = false;
  }

  void OnTriggerStay(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      Key = true;
    }
  }

}
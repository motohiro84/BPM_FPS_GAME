﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMaker : MonoBehaviour
{

  public GameObject wall; //壁にするプレハブ取得用の変数

  // Use this for initialization
  void Start()
  {
    int[,] map = {
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,1,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,1,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,1,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,1,1},
            {1,0,0,0,0,0,1,1,0,0,0,0,1,1,1,0,0,0,1,1},
            {1,0,0,0,0,0,0,0,0,0,2,0,1,1,1,0,0,0,1,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,1,1},
            {1,0,0,0,0,0,1,1,0,0,0,0,1,1,1,0,0,0,1,1},
            {1,1,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,1,1},
            {1,1,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
        };
    for (int i = 0; i < 20; i++)
    {
      for (int j = 0; j < 20; j++)
      {
        if (map[i, j] == 1)
        {
          Instantiate(wall, new Vector3((j * 2) - 19, 10, (i * 2) - 19), Quaternion.identity);
        }
      }
    } //このループで二次元配列に基づいて壁を作成


  }

}
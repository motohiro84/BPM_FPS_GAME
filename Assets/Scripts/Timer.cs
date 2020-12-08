using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
  public static float time;
  private float starttime;
  public Text TimeText;


  void Start()
  {
    time = 0.0f;
    starttime = 0.0f;
    TimeText.GetComponent<Text>().text = time.ToString("F2");
  }
  void Update()
  {
    TimeCount();
  }
  void TimeCount()
  {
    if (starttime < 5)
    {
      starttime += Time.deltaTime;
    }
    else if (BossSpawner.bossNum > 0)
    {
      time += Time.deltaTime;
      TimeText.GetComponent<Text>().text = time.ToString("F2");
    }
  }
}

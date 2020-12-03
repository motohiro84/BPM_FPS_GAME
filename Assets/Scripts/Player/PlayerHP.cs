using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
  public int maxHP = 100;
  public int Hp = 0;
  public Image HpGauge;

  public int HP
  {
    set
    {
      Hp = Mathf.Clamp(value, 0, maxHP);

      float scaleX = (float)Hp / maxHP;
      HpGauge.rectTransform.localScale = new Vector3(scaleX, 1, 1);
    }
    get
    {
      return Hp;
    }
  }
  void Start()
  {
    HP = maxHP;
  }

  // Update is called once per frame
  void Update()
  {

  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
  public int maxHP = 100;
  public int Hp = 0;
  public Image HpGauge;
  GameManager gameManager;
  EnemyController enemy;

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
    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    HP = maxHP;
  }

  // Update is called once per frame
  void Update()
  {
    if (Hp == 0)
    {
      StartCoroutine(gameManager.GameOver());
    }
  }

  void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.tag == "Enemy")
    {
      EnemyController enemy = collision.gameObject.transform.root.gameObject.GetComponent<EnemyController>();
      StartCoroutine(enemy.AttackTimer());
    }
  }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeponChange : MonoBehaviour
{
  public GameObject Wepon1;
  public GameObject Wepon2;
  void Start()
  {
    Wepon1.SetActive(true);
  }

  // Update is called once per frame
  void Update()
  {
    Change();
  }

  void Change()
  {
    if (Wepon2.activeSelf && Input.GetKeyDown(KeyCode.Alpha1))
    {
      Wepon1.SetActive(true);
      Wepon2.SetActive(false);
    }
    else if (Wepon1.activeSelf && Input.GetKeyDown(KeyCode.Alpha2))
    {
      Wepon1.SetActive(false);
      Wepon2.SetActive(true);
    }
  }

}

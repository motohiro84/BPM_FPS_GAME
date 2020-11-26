using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{
  private Animator animator;
  public static string state;
  string prevState;
  string wepon = "Revolver";
  int weponNum = 1;
  string weponState;

  void Start()
  {
    animator = this.gameObject.GetComponent<Animator>();
  }

  void Update()
  {
    if (WeponChange.Key != weponNum)
    {
      WeponMotionChange();
    }

    if (state == "Relord")
    {
      if (GunRelord())
      {
        RelordEndMotion();
      }
    }
    // ChangeState();
    // ChangeAnimation();
  }

  void ChangeState()
  {
    if (state != "Relord" && state != "RelordEnd")
    {
      state = "Idle";
    }
    if (GunRelord() && state == "Relord")
    {
      state = "RelordEnd";
    }
  }

  void ChangeAnimation()
  {
    if (prevState != state)
    {
      switch (state)
      {
        case "Fire":
          animator.SetTrigger("Fire");
          animator.SetBool("Idle", false);
          animator.SetBool("Relord", false);
          animator.SetBool("RelordEnd", false);
          break;
        case "DryFire":
          animator.SetTrigger("DryFire");
          animator.SetBool("Idle", false);
          animator.SetBool("Relord", false);
          animator.SetBool("RelordEnd", false);
          break;
        case "Relord":
          animator.SetBool("Relord", true);
          animator.SetBool("Idle", false);
          animator.SetBool("RelordEnd", false);
          break;
        case "RelordEnd":
          animator.SetBool("RelordEnd", true);
          animator.SetBool("Relord", false);
          animator.SetBool("Idle", false);
          break;

        default:
          animator.SetBool("Idle", true);
          animator.SetBool("Relord", false);
          animator.SetBool("RelordEnd", false);
          break;
      }
      prevState = state;
    }
  }

  bool GunRelord()
  {
    return Input.GetKeyDown(KeyCode.R);
  }

  public void FireShootMotion()
  {
    state = "Fire";
    WeponName();
    // ChangeAnimation();
    animator.CrossFadeInFixedTime(weponState, 0);
  }

  public void DryShootMotion()
  {
    state = "DryFire";
    WeponName();
    // ChangeAnimation();
    animator.CrossFadeInFixedTime(weponState, 0);
  }

  public void RelordMotion()
  {
    state = "Relord";
    WeponName();
    // ChangeAnimation();
    animator.CrossFadeInFixedTime(weponState, 0);
  }
  public void RelordEndMotion()
  {
    state = "RelordEnd";
    WeponName();
    // ChangeAnimation();
    animator.CrossFadeInFixedTime(weponState, 0);
  }
  void IdleMotion()
  {
    state = "Idle";
    ChangeAnimation();
  }
  public void Test()
  {
    state = "Idle";
    ChangeAnimation();
  }

  void WeponMotionChange()
  {
    if (weponNum == 1)
    {
      weponNum = 2;
      wepon = "Shotgun";
    }
    else
    {
      weponNum = 1;
      wepon = "Revolver";
    }
    WeponName();
  }

  void WeponName()
  {
    weponState = wepon + "_" + state;
  }

}

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
  AnimatorStateInfo animStateInfo;

  void Start()
  {
    animator = this.gameObject.GetComponent<Animator>();
  }

  void Update()
  {
    animStateInfo = animator.GetCurrentAnimatorStateInfo(0);

    if (WeponChange.Key != weponNum)
    {
      WeponMotionChange();
    }

    if (animStateInfo.fullPathHash == Animator.StringToHash("Base Layer." + wepon + "_Relord"))
    {
      if (animStateInfo.normalizedTime >= 1.0f)
      {
        state = "_Relord";
      }
    }

    if (animStateInfo.fullPathHash == Animator.StringToHash("Base Layer." + wepon + "_RelordEnd"))
    {
      if (animStateInfo.normalizedTime >= 1.0f)
      {
        IdleMotion();
      }
    }
  }

  void IdleMotion()
  {
    state = "Idle";
    WeponName();
    animator.CrossFadeInFixedTime(weponState, 0);
  }

  public void FireShootMotion()
  {
    state = "Fire";
    WeponName();
    animator.CrossFadeInFixedTime(weponState, 0);
  }

  public void DryShootMotion()
  {
    state = "DryFire";
    WeponName();
    animator.CrossFadeInFixedTime(weponState, 0);
  }

  public void RelordMotion()
  {
    state = "Relord";
    WeponName();
    animator.CrossFadeInFixedTime(weponState, 0);
  }
  public void RelordEndMotion()
  {
    state = "RelordEnd";
    WeponName();
    animator.CrossFadeInFixedTime(weponState, 0);
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

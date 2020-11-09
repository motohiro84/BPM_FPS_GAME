using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{
  private Animator animator;
  public static string state;
  string prevState;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      ChangeState();
      ChangeAnimation();
    }

  void ChangeState()
  {

    // if ((stateInfo.fullPathHash !=  Animator.StringToHash("Base Layer.Relord")) && Input.GetKeyDown(KeyCode.R))
    // {
    //     state = "Relord";
    // }
    if (state != "Relord")
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
          break;
        case "DryFire":
          animator.SetTrigger("DryFire");
          animator.SetBool("Idle", false);
          animator.SetBool("Relord", false);
          break;
        case "Relord":
          animator.SetBool("Relord", true);
          animator.SetBool("Idle", false);
          break;
        case "RelordEnd":
          animator.SetTrigger("RelordEnd");
          animator.SetBool("Relord", false);
          animator.SetBool("Idle", false);
          break;

        default:
          animator.SetBool("Idle", true);
          animator.SetBool("Relord", false);
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
    ChangeAnimation();
  }
  
  public void DryShootMotion()
  {
    state = "DryFire";
    ChangeAnimation();
  }

  public void RelordMotion()
  {
    state = "Relord";
    ChangeAnimation();
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{
  private Animator animator;
  string state;
  string prevState;
  public static AnimatorStateInfo stateInfo;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
      stateInfo = animator.GetCurrentAnimatorStateInfo(0);
      ChangeState();
      ChangeAnimation();
    }

  void ChangeState()
  {

    // if ((stateInfo.fullPathHash !=  Animator.StringToHash("Base Layer.Relord")) && Input.GetKeyDown(KeyCode.R))
    // {
    //     state = "Relord";
    // }
    if ((stateInfo.fullPathHash ==  Animator.StringToHash("Base Layer.Relord")) && Input.GetKeyDown(KeyCode.R))
    {
        state = "RelordEnd";
    }
    else if ((stateInfo.fullPathHash !=  Animator.StringToHash("Base Layer.Relord")))
    {
        state = "Idle";
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

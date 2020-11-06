using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{
  private Animator animator;
  private int maxAmmo;
  private int ammo;
  string state;
  string prevState;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        maxAmmo = FirstPersonGunController.fullAmmo;
        ammo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeState();
        ChangeAnimation();
    }

  void ChangeState()
  {


    if (Input.GetMouseButtonDown(0))
    {
        if (ammo != 0)
        {
            state = "Fire";
            ammo--;
        }
        else
        {
            state = "DryFire";
        }
    }
    else if ((ammo < maxAmmo) & Input.GetKeyDown(KeyCode.R))
    {
        state = "Relord1";
    }
    else if ((state == "Relord1") && Input.GetKeyDown(KeyCode.R))
    {
        ammo = maxAmmo;
        state = "Relord2";
    }
    else if (state != "Relord1")
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
          break;
        case "DryFire":
          animator.SetTrigger("DryFire");
          animator.SetBool("Idle", false);
          break;
        case "Relord1":
          animator.SetTrigger("Relord1");
          animator.SetBool("Idle", false);
          break;
        case "Relord2":
          animator.SetTrigger("Relord2");
          animator.SetBool("Idle", false);
          break;

        default:
          animator.SetBool("Idle", true);
          break;
      }
      prevState = state;
    }
  }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Events : MonoBehaviour
{
  Animator animator;
  RuntimeAnimatorController runtimeAnimatorController;
  AnimationClip[] animationClips;
  public GameObject JumpAttack;
  public GameObject[] AttackSphere;


  // Start is called before the first frame update
  void Start()
  {
    animator = GetComponent<Animator>();
    runtimeAnimatorController = animator.runtimeAnimatorController;
    animationClips = runtimeAnimatorController.animationClips;
    AttackMode(false);
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void AddAnimationEvents()
  {
    foreach (var clip in animationClips)
    {
      if (clip.name != "Idle")
      {

        if (clip.name != "Run" && clip.name != "Walk")
        {
          var NoMoveEvent = new AnimationEvent();
          NoMoveEvent.functionName = "NoMoveAnimation";
          NoMoveEvent.stringParameter = clip.name;
          NoMoveEvent.time = 0;
          clip.AddEvent(NoMoveEvent);

          if (clip.name != "Jump")
          {
            var MoveEvent = new AnimationEvent();
            MoveEvent.functionName = "MoveAnimation";
            MoveEvent.stringParameter = clip.name;
            MoveEvent.time = clip.length;
            clip.AddEvent(MoveEvent);
          }
        }

        if (clip.name == "Jump" || clip.name == "JumpEnd")
        {
          var NoAttackEvent = new AnimationEvent();
          NoAttackEvent.functionName = "NoAttackAnimation";
          NoAttackEvent.stringParameter = clip.name;
          NoAttackEvent.time = 0;
          clip.AddEvent(NoAttackEvent);

          if (clip.name == "JumpEnd")
          {
            var JumpEndEvent = new AnimationEvent();
            JumpEndEvent.functionName = "JumpEndAnimation";
            JumpEndEvent.stringParameter = clip.name;
            JumpEndEvent.time = clip.length;
            clip.AddEvent(JumpEndEvent);
          }

          if (clip.name == "Jump")
          {
            var JumpEvent = new AnimationEvent();
            JumpEvent.functionName = "JumpAnimation";
            JumpEvent.stringParameter = clip.name;
            JumpEvent.time = clip.length;
            clip.AddEvent(JumpEvent);
          }

        }
      }

      if (clip.name == "Attack1" || clip.name == "Attack2" || clip.name == "Attack3")
      {
        var StartAttackEvent = new AnimationEvent();
        StartAttackEvent.functionName = "StartAttackAnimation";
        StartAttackEvent.stringParameter = clip.name;
        StartAttackEvent.time = 0;
        clip.AddEvent(StartAttackEvent);

        var ExitAttackEvent = new AnimationEvent();
        ExitAttackEvent.functionName = "ExitAttackAnimation";
        ExitAttackEvent.stringParameter = clip.name;
        ExitAttackEvent.time = clip.length;
        clip.AddEvent(ExitAttackEvent);
      }

      if (clip.name == "Death")
      {
        var DeathEvent = new AnimationEvent();
        DeathEvent.functionName = "DeathAnimation";
        DeathEvent.stringParameter = clip.name;
        DeathEvent.time = 0;
        clip.AddEvent(DeathEvent);
      }
    }


  }

  void MoveAnimation()
  {
    Controller.moveEnabled = true;
  }
  void NoMoveAnimation()
  {
    Controller.moveEnabled = false;
  }
  void NoAttackAnimation()
  {
    Controller.attackEnabled = false;
  }
  void JumpEndAnimation()
  {
    Controller.attackEnabled = true;
    JumpAttack.SetActive(false);
  }

  void JumpAnimation()
  {
    JumpAttack.SetActive(true);
  }
  void DeathAnimation()
  {
    Controller.moveEnabled = false;
    Controller.attackEnabled = false;
    Controller.coolDown = false;
  }

  void StartAttackAnimation()
  {
    AttackMode(true);
  }
  void ExitAttackAnimation()
  {
    AttackMode(false);
  }



  void AttackMode(bool i)
  {
    foreach (GameObject g in AttackSphere)
    {
      g.SetActive(i);
    }
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : ByTheTale.StateMachine.State {
  public override void Initialize () {

  }

  public override void Enter () {
    Time.timeScale = 0;
  }

  public override void Execute () {

  }
  public override void PhysicsExecute () {

  }
  public override void PostExecute () {

  }

  public override void Exit () {
    Time.timeScale = 1;
  }
}
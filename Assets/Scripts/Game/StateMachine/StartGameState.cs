using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameState : ByTheTale.StateMachine.State {
  public override void Initialize () {

  }

  public override void Enter () {
    GameEvent.instance.InGameState ();
  }

  public override void Execute () {

  }
  public override void PhysicsExecute () {

  }
  public override void PostExecute () {

  }

  public override void Exit () {

  }
}
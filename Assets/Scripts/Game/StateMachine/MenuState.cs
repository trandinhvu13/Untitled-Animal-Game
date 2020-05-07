using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : ByTheTale.StateMachine.State {
  #region Mono
  public override void Initialize () {

  }
  public override void Enter () {
    GameEvent.instance.OnPlayButtonPress += HandlePlayButtonPressed;
  }
  public override void Exit () {
    GameEvent.instance.OnPlayButtonPress -= HandlePlayButtonPressed;
  }
  public override void Execute () {

  }
  public override void PhysicsExecute () {

  }
  public override void PostExecute () {

  }

  #endregion

  #region Methods

  void HandlePlayButtonPressed () {
    GameEvent.instance.ChangeScene(1);
  }

  #endregion
}
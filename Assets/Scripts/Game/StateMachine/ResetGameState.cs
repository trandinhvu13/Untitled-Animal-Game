using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGameState : ByTheTale.StateMachine.State
{
    public override void Enter(){
      GameEvent.instance.ChangeScene (1);
    }
}

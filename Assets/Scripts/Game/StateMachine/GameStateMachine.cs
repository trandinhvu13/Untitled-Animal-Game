using System.Collections;
using System.Collections.Generic;
using ByTheTale.StateMachine;
using UnityEngine;

public class GameStateMachine : MachineBehaviour {
    public static GameStateMachine instance = null;

    private void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }

    }
    public override void AddStates () {
        AddState<MenuState> ();
        AddState<LoadingState> ();
        AddState<StartGameState> ();
        AddState<InGameState> ();
        AddState<PauseState> ();
        AddState<LoseState> ();
        AddState<PostGameState> ();


        SetInitialState<MenuState> ();
    }
}
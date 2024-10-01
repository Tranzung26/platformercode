using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBooleanBehavior : StateMachineBehaviour
{
    //The Animator boolean parameter name.
    public string BoolName;
   // When set to true, causes 'ValueOnEnter' to be set when any state is entered, and 'ValueOnExit' to be set when any state is exited.
    public bool UpdateOnState;
   // When set to true, causes 'ValueOnEnter' to be set when the state machine itself is entered, and 'ValueOnExit' to be set when the state machine itself is exited.
    public bool UpdateOnStateMachine;
   //The value to be set when a state or statemachine was entered.
    public bool ValueOnEnter;
    //The value to be set when a state or statemachine was exited.
    public bool ValueOnExit;

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(UpdateOnState)
        {
            animator.SetBool(BoolName, ValueOnEnter);
        }
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(UpdateOnState) 
        {
            animator.SetBool(BoolName, ValueOnExit);
        }
    }

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if(UpdateOnStateMachine)
        {
            animator.SetBool(BoolName, ValueOnEnter);
        }
    }

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if(UpdateOnStateMachine)
        {
            animator.SetBool(BoolName, ValueOnExit);
        }
    }
}

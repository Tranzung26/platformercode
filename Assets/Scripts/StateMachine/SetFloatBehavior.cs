using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: There is quite some duplicated code between this class and SetBooleanBehavior.
// Extract a baseclass (with generic T?) for the kind of parameter being set.
public class SetFloatBehavior : StateMachineBehaviour
{
    //The Animator float parameter name.
    public string FloatName;

    //When set to true, causes 'ValueOnEnter' to be set when any state is entered.
    public bool UpdateOnStateEnter;
    //When set to true, causes 'ValueOnExit' to be set when any state is exited.
    public bool UpdateOnStateExit;
    //When set to true, causes 'ValueOnEnter' to be set when the state machine itself is entered.
    public bool UpdateOnStateMachineEnter;
    //When set to true, causes 'ValueOnExit' to be set when the state machine itself is exited.
    public bool UpdateOnStateMachineExit;
    //The value to be set when a state or statemachine was entered.
    public float ValueOnEnter;
   //"The value to be set when a state or statemachine was exited.
    public float ValueOnExist;
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (UpdateOnStateEnter)
        {
            animator.SetFloat(FloatName, ValueOnEnter);
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
        if(UpdateOnStateExit)
        {
            animator.SetFloat(FloatName, ValueOnExist);
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
        if(UpdateOnStateMachineEnter)
        {
            animator.SetFloat(FloatName, ValueOnEnter);
        }
    }

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (UpdateOnStateMachineExit)
        {
            animator.SetFloat(FloatName, ValueOnExist);
        }
    }
}

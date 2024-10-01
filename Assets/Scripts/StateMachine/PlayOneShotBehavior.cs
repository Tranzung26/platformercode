using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOneShotBehavior : StateMachineBehaviour
{
    //Determines the AudioClip to be played.
    public AudioClip SoundToPlay;

    //Determines the volume of 'SoundToPlay' when played.
    public float Volume = 1f;

    //Determines if 'SoundToPlay' is played when the state is entered.
    public bool PlayOnEnter = true;

    //Determines if 'SoundToPlay' is played when the state is exited.
    public bool PlayOnExit = false;

    //Determines if 'SoundToPlay' is played after a delay since the state was entered.
    public bool PlayOnDelay = false;
    //Determines the length, in seconds, of the delay.
    public float PlayDelay = 0.25f;
    float _timeSinceEntered = 0f;
    bool _hasPlayedDelayedSound = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (PlayOnEnter)
        {
            // Assumption: It's always the animator owner that should be playing the sound
            AudioSource.PlayClipAtPoint(SoundToPlay, animator.gameObject.transform.position, Volume);
        }

        _timeSinceEntered = 0f;
        _hasPlayedDelayedSound = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(PlayOnDelay && !_hasPlayedDelayedSound) 
        {
            _timeSinceEntered += Time.deltaTime;
            if(_timeSinceEntered > PlayDelay)
            {
                _hasPlayedDelayedSound = true;

                // Assumption: It's always the animator owner that should be playing the sound
                AudioSource.PlayClipAtPoint(SoundToPlay, animator.gameObject.transform.position, Volume);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (PlayOnExit)
        {
            // Assumption: It's always the animator owner that should be playing the sound
            AudioSource.PlayClipAtPoint(SoundToPlay, animator.gameObject.transform.position, Volume);
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

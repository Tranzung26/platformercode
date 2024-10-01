using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeRemoveBehavior : StateMachineBehaviour
{
    float _timeElapsed = 0f;
    float _fadeDelayElapsed = 0f;

    //Determines how long, in seconds, it takes before the object fades off the screen and then gets destroyed.
    public float FadeTime = 0.5f; // In seconds

    //Determines how long, in seconds, before the fading kicks in.
    public float FadeDelay = 0f;

    SpriteRenderer _spriteRenderer;
    GameObject _gameObjectToRemove;

    Color _startColor;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timeElapsed = 0f;
        _spriteRenderer = animator.GetComponent<SpriteRenderer>();
        _startColor = _spriteRenderer.color;
        _gameObjectToRemove = animator.gameObject;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(FadeDelay > _fadeDelayElapsed)
        {
            _fadeDelayElapsed += Time.deltaTime;
        }
        else
        {
            _timeElapsed += Time.deltaTime;

            var newAlpha = _startColor.a * (1 - (_timeElapsed / FadeTime));
            _spriteRenderer.color = new Color(_startColor.r, _startColor.g, _startColor.b, newAlpha);

            if (_timeElapsed > FadeTime)
            {
                Destroy(_gameObjectToRemove);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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

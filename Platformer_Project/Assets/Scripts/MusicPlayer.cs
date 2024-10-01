using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [Tooltip("Determines the music to be played as intro into the game.")]
    public AudioSource IntroSource;
    [Tooltip("Determines the loopable music of the game.")]
    public AudioSource LoopSource;

    private void Awake()
    {
        Debug.Assert(IntroSource != null, "'IntroSource' must be set!");
        Debug.Assert(LoopSource != null, "'LoopSource' must be set!");
    }

    // Start is called before the first frame update
    void Start()
    {
        IntroSource.Play();
        LoopSource.PlayScheduled(AudioSettings.dspTime + IntroSource.clip.length);
    }
}

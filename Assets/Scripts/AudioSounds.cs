using UnityEngine;

public class AudioSounds : MonoBehaviour
{
    [SerializeField] private AudioSource countdownBeat;
    [SerializeField] private AudioSource gameFinishSound;
    [SerializeField] private AudioSource rightSquareSound;
    [SerializeField] private AudioSource wrongSquareSound;
    [SerializeField] private AudioSource lowTimeSound;
    [SerializeField] private AudioSource buttonClickSound;

    public void PlayCountdownBeat()
    {
        countdownBeat.Play();
    }

    public void PlayGameFinishSound()
    {
        gameFinishSound.Play();
    }

    public void PlayRightSquareSound()
    {
        rightSquareSound.Play();
    }

    public void PlayWrongSquareSound()
    {
        wrongSquareSound.Play();
    }

    public void PlayLowTimeSound()
    {
        lowTimeSound.Play();
    }

    public void PlayButtonClickSound()
    {
        buttonClickSound.Play();
    }
}
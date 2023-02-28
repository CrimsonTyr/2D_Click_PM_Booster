using UnityEngine;

public class ComboTextScript : MonoBehaviour
{
    private Animator comboAnimator;
    private Vector3 pos;
    private AnimationEvent evt;
    private AnimationClip clip;

    // Sets up the AnimationEvent to add the function TriggerAtAnimationEnd()
    // as an event that will be called 0.5s after the animation starts
    private void Awake()
    {
        comboAnimator = GetComponent<Animator>();
        pos = Vector3.zero;
        evt = new AnimationEvent();
        evt.time = 0.5f;
        evt.functionName = "TriggerAtAnimationEnd";
        clip = comboAnimator.runtimeAnimatorController.animationClips[0];
        clip.AddEvent(evt);
        gameObject.SetActive(false);
    }

    // Set the combo text close to the user's mouse cursor
    // and then starts the animation before being set to inactive again
    private void OnEnable()
    {
        SetTextPosition();
        comboAnimator.Play("ClickInfosAnimation");
    }

    // Set the text close to the user's mouse cursor
    private void SetTextPosition()
    {
        pos.x = Input.mousePosition.x / 10 - 96 + 10;
        pos.y = Input.mousePosition.y / 10 - 54 + 5;
        transform.localPosition = pos;
    }

    // This function is called 0.5s after the animation is called
    public void TriggerAtAnimationEnd()
    {
        gameObject.SetActive(false);
    }
}
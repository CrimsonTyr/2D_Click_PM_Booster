using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ExitApp : MonoBehaviour
{
    [SerializeField] AudioSounds sounds;
    [SerializeField] Animator exitAnimator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ConfirmExit();
    }

    public void ConfirmExit()
    {
        if (ClickPmBoosterGame.instance.IsPlaying())
            ClickPmBoosterGame.instance.PauseMode(true);
        sounds.PlayButtonClickSound();
        exitAnimator.Play("ExitPanelAnimation");
    }

    public void ExitApplication()
    {
        sounds.PlayButtonClickSound();
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    public void CancelExit()
    {
        sounds.PlayButtonClickSound();
        exitAnimator.Play("CancelExitAnimation");
        if (ClickPmBoosterGame.instance.IsPlaying())
            StartCoroutine(ExitPauseMode());
    }

    private IEnumerator ExitPauseMode()
    {
        yield return new WaitForSeconds(0.5f);
        ClickPmBoosterGame.instance.PauseMode(false);
    }
}
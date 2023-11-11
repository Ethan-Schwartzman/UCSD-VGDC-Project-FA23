using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class GameOver : MonoBehaviour
{
    public SpriteRenderer TransitionScreen;
    public TextMeshPro GameOverMessage;
    public MainMenuManager MMM;
    public JumpscareManager Jumpscare;
    private float fadeSpeed = 1f;

    public void Win() {
        gameObject.SetActive(true);
        StartCoroutine(FadeIn(true));
    }

    public void Lose() {
        gameObject.SetActive(true);
        StartCoroutine(FadeIn(false));
    }

    // Fade in a black screen
    private IEnumerator FadeIn(bool won) {
        float opactiy = 0f;
        GameOverMessage.color = new Color(0, 0, 0, opactiy);
        yield return TransitionManager.Fade(TransitionScreen, 0f, 1f, fadeSpeed);
        yield return new WaitForSeconds(1f);
        if(won) {
            StartCoroutine(FadeTextIn());
        }
        else {
            Jumpscare.TriggerJumpscare();
        }
    }

    // Fade in the gave over message
    private IEnumerator FadeTextIn() {
        float opactiy = 0f;
        GameOverMessage.color = new Color(1f, 1f, 1f, 0f);
        yield return TransitionManager.Fade(GameOverMessage, 0, 1, fadeSpeed);
        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeOut(true));


        Application.Quit();

        // Return to the main menu
        MMM.ShowMainMenuNoFade();
    }

    // Fade out the game over message, then right after,
    // fade out the black screen
    public IEnumerator FadeOut(bool fadeOutText) {
        if(fadeOutText) {
            yield return TransitionManager.Fade(GameOverMessage, 1, 0, fadeSpeed);
        }
        else {
            GameOverMessage.color = new Color(1, 1, 1, 0);
        }

        yield return TransitionManager.Fade(TransitionScreen, 1, 0, fadeSpeed);
    }
}

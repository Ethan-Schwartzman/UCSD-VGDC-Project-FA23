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
    private float fadeSpeed = 0.001f;

    public void Win() {
        StartCoroutine(FadeIn());
    }

    // Fade in a black screen
    private IEnumerator FadeIn() {
        float opactiy = 0f;
        GameOverMessage.color = new Color(0, 0, 0, opactiy);
        while(opactiy < 1f) {
            opactiy += fadeSpeed;
            if(opactiy > 1f) opactiy = 1f; 
            TransitionScreen.color = new Color(0, 0, 0, opactiy);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeTextIn());
    }

    // Fade in the gave over message
    private IEnumerator FadeTextIn() {
        float opactiy = 0f;
        while(opactiy < 1f) {
            opactiy += fadeSpeed;
            if(opactiy > 1f) opactiy = 1f; 
            GameOverMessage.color = new Color(1, 1, 1, opactiy);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeOut());
        
        // Return to the main menu
        MMM.ShowMainMenuNoFade();
    }

    // Fade out the game over message, then right after,
    // fade out the black screen
    private IEnumerator FadeOut() {
        float opactiy = 1f;
        while(opactiy > 0f) {
            opactiy -= fadeSpeed;
            if(opactiy < 0f) opactiy = 0f; 
            GameOverMessage.color = new Color(1, 1, 1, opactiy);
            yield return null;
        }

        opactiy = 1f;
        while(opactiy > 0f) {
            opactiy -= fadeSpeed;
            if(opactiy < 0f) opactiy = 0f; 
            TransitionScreen.color = new Color(0, 0, 0, opactiy);
            yield return null;
        }
    }
}

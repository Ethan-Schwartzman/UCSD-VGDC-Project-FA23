using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpscareManager : MonoBehaviour
{
    public GameObject TVStatic;
    public GameObject[] Jumpscares;
    public MainMenuManager MMM;
    public GameOver GameOverScreen;

    public void TriggerJumpscare() {
        gameObject.SetActive(true);
        TVStatic.SetActive(true);
        Jumpscares[Random.Range(0, Jumpscares.Length)].SetActive(true);
        StartCoroutine(WaitForJumpscareToFinish());
    }

    public void StopJumpscare() {
        TVStatic.SetActive(false);
        foreach(GameObject g in Jumpscares) {
            g.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    private IEnumerator WaitForJumpscareToFinish() {
        yield return new WaitForSeconds(1.6f);
        StopJumpscare();
        GameOverScreen.StartCoroutine(GameOverScreen.FadeOut(false));
        MMM.ShowMainMenuNoFade();
    }
}

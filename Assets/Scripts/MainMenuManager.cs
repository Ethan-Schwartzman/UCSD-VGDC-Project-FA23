using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MainMenuManager : MonoBehaviour {
    public bool EnableMainMenu;
    public GameObject Game;
    public Timer GameTimer;
    //[Range(0.001f, 0.01f)]
    public float FadeSpeed;
    public SpriteRenderer TransitionScreen;
    public float timeLimit;
    public MonsterSelection MonsterSelectionScript;
    public GameObject Credits;

    public AudioSource m_MyAudioSource;

    public struct Difficulty {
        public float time;
    }
    Difficulty CurrentDifficulty;
    private bool inCredits = false;
    private bool transitioning;

    // Start is called before the first frame update
    void Start() {
        transitioning = false;
        inCredits = false;
        SetDifficulty(timeLimit);
        if (EnableMainMenu) {
            foreach (Transform child in transform) {
                child.gameObject.SetActive(true);
            }
            Game.gameObject.SetActive(false);
            this.gameObject.SetActive(true);
        }
        else {
            foreach (Transform child in transform) {
                child.gameObject.SetActive(false);
            }
            Game.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
        Credits.SetActive(false);
        m_MyAudioSource = GetComponent<AudioSource>();
    }

    public void StartNewGame() {
        HideMainMenu();
        GameTimer.SetTimer(CurrentDifficulty.time);
        MonsterSelectionScript.ResetSelected();
        MonsterSelectionScript.DeselectAllIcons();
        MonsterSelectionScript.DeselectAllText();
    }

    public void ShowMainMenu() {
        //gameObject.SetActive(true);
        StartCoroutine(FadeToBlack(true)); // true means fade in
        
        inCredits = false;
        foreach (Transform child in transform) {
            child.gameObject.SetActive(true);
        }
        Game.gameObject.SetActive(false);
        this.gameObject.SetActive(true);
        Credits.SetActive(false);
    }

    public void HideMainMenu() {
        StartCoroutine(FadeToBlack(false)); // false means fade out
        if(Input.anyKey){
            m_MyAudioSource.Stop();
        }
    }

    public void SetDifficulty(float time) {
        CurrentDifficulty.time = time;
    }

    public IEnumerator ShowCredits() {
        transitioning = true;
        inCredits = true;
        yield return TransitionManager.Fade(TransitionScreen, 0f, 1f, FadeSpeed);
        Credits.SetActive(true);
        yield return TransitionManager.Fade(TransitionScreen, 1f, 0f, FadeSpeed);
        transitioning = false;
    }

    public IEnumerator HideCredits() {
        transitioning = true;
        inCredits = false;
        yield return TransitionManager.Fade(TransitionScreen, 0f, 1f, FadeSpeed);
        Credits.SetActive(false);
        yield return TransitionManager.Fade(TransitionScreen, 1f, 0f, FadeSpeed);
        transitioning = false;
    }

    void Update() {
        if (Input.GetKeyDown("escape")) {
            Application.Quit();
        }
        if (Input.anyKeyDown) {
            StartCoroutine(TryHideMainMenu());
        }
    }

    private IEnumerator TryHideMainMenu() {
        yield return new WaitForSeconds(0.1f);
        if(!transitioning) {
            if(!inCredits) {
                HideMainMenu();
            }
            else {
                StartCoroutine(HideCredits());
            }
        }
    }

    private IEnumerator FadeToBlack(bool fadeDirection) {
        yield return TransitionManager.Fade(TransitionScreen, 0f, 1f, FadeSpeed);

        foreach (Transform child in transform) {
            child.gameObject.SetActive(fadeDirection);
        }

        Game.gameObject.SetActive(!fadeDirection);

        yield return TransitionManager.Fade(TransitionScreen, 1f, 0f, FadeSpeed);
        this.gameObject.SetActive(fadeDirection);
    }

    public void ShowMainMenuNoFade() {
        inCredits = false;
        foreach (Transform child in transform) {
            child.gameObject.SetActive(true);
        }
        Game.gameObject.SetActive(false);
        this.gameObject.SetActive(true);
        Credits.SetActive(false);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MainMenuManager : MonoBehaviour {
    public bool EnableMainMenu;
    public GameObject Game;
    public Timer GameTimer;
    [Range(0.001f, 0.01f)]
    public float FadeSpeed;
    public SpriteRenderer TransitionScreen;
    public float timeLimit;
    public MonsterSelection MonsterSelectionScript;

    public struct Difficulty {
        public float time;
    }
    Difficulty CurrentDifficulty;

    // Start is called before the first frame update
    void Start() {
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
    }

    public void StartNewGame() {
        HideMainMenu();
        GameTimer.SetTimer(CurrentDifficulty.time);
        MonsterSelectionScript.DeselectAllIcons();
        MonsterSelectionScript.DeselectAllText();
    }

    public void ShowMainMenu() {
        gameObject.SetActive(true);
        StartCoroutine(FadeToBlack(true)); // true means fade in
    }

    public void HideMainMenu() {
        StartCoroutine(FadeToBlack(false)); // false means fade out
    }

    public void SetDifficulty(float time) {
        CurrentDifficulty.time = time;
    }

    void Update() {
        if (Input.GetKeyDown("escape")) {
            Application.Quit();
        }
        if (Input.anyKeyDown) {
            HideMainMenu();
        }
    }

    private IEnumerator FadeToBlack(bool fadeDirection) {
        float opactiy = 0f;
        while (opactiy < 1f) {
            opactiy += FadeSpeed;
            if (opactiy > 1f) opactiy = 1f;
            TransitionScreen.color = new Color(0, 0, 0, opactiy);
            yield return null;
        }

        foreach (Transform child in transform) {
            child.gameObject.SetActive(fadeDirection);
        }

        Game.gameObject.SetActive(!fadeDirection);

        while (opactiy > 0f) {
            opactiy -= FadeSpeed;
            if (opactiy < 0f) opactiy = 0f;
            TransitionScreen.color = new Color(0, 0, 0, opactiy);
            yield return null;
        }
        this.gameObject.SetActive(fadeDirection);
    }

    public void ShowMainMenuNoFade() {
        foreach (Transform child in transform) {
            child.gameObject.SetActive(true);
        }
        Game.gameObject.SetActive(false);
        this.gameObject.SetActive(true);
    }
}

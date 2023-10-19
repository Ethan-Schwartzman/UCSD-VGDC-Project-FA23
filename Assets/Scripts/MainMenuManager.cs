using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MainMenuManager : MonoBehaviour
{
    public bool EnableMainMenu;
    public GameObject Game;
    public Timer GameTimer;

    public struct Difficulty{
        public float time;
    }
    Difficulty CurrentDifficulty;

    // Start is called before the first frame update
    void Start()
    {
        SetDifficulty(300f);
        if(EnableMainMenu) {
            ShowMainMenu();
        }   
        else {
            HideMainMenu();
        }
    }

    public void StartNewGame() {
        HideMainMenu();
        GameTimer.SetTimer(CurrentDifficulty.time);
    }

    private void ShowMainMenu() {
        this.gameObject.SetActive(true);
        Game.gameObject.SetActive(false);
    }

    private void HideMainMenu() {
        this.gameObject.SetActive(false);
        Game.gameObject.SetActive(true);
    }

    public void SetDifficulty(float time) {
        CurrentDifficulty.time = time;
    }
}

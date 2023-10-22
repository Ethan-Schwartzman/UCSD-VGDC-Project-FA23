using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class MonsterSelection : MonoBehaviour
{
    public GameObject[] Options;
    public GameObject[] SelectedIcons;
    public GameObject Submit;
    public PageTurner PageTurnerScript;
    public GameOver GameOverScript;

    private Color notSelected = new Color(0.5f, 0.5f, 0.5f, 1f);
    private Color selected = new Color(0f, 0f, 0f, 1f);
    private int currentOption;
    private int selectedOption;
    private AudioSource[] allAudioSources;

    // Start is called before the first frame update
    void Start()
    {
        // Set all options to not selected
        currentOption = -1;
        selectedOption = -1;
        DeselectAllText();
        DeselectAllIcons();
    }

    private void DeselectAllText() {
        for(int i = 0; i < Options.Length; i++) {
            Options[i].GetComponentInChildren<TextMeshPro>().color = notSelected;
        }

        Submit.GetComponent<TextMeshPro>().color = notSelected;
    }

    private void DeselectAllIcons() {
        for(int i = 0; i < Options.Length; i++) {
            SelectedIcons[i].SetActive(false);
        }
    }

    public void SelectNext() {
        DeselectAllText();
        if(!(currentOption >= Options.Length)) currentOption++;
        if(currentOption < 0) return;
        else if(currentOption <= Options.Length-1) {
            Options[currentOption].GetComponentInChildren<TextMeshPro>().color = selected;
        }
        else {
            Submit.GetComponent<TextMeshPro>().color = selected;
        }
    }

    public void SelectPrevious() {
        DeselectAllText();
        if(!(currentOption < -1)) currentOption--;
        if(currentOption < 0) PageTurnerScript.SetNotSelectingMonster();
        else if(currentOption <= Options.Length-1) {
            Options[currentOption].GetComponentInChildren<TextMeshPro>().color = selected;
        }
        else {
            Submit.GetComponent<TextMeshPro>().color = selected;
        }
    }
    void StopAllAudio() {
	    allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
	    foreach( AudioSource audioS in allAudioSources) {
		audioS.Stop();
	}
}

    void Update() {
        if(Input.GetButtonDown("Submit")){
            if(currentOption >= 0 && currentOption <= Options.Length-1) {
                DeselectAllIcons();
                SelectedIcons[currentOption].SetActive(true);
                selectedOption = currentOption;
            }
            if(currentOption == Options.Length && selectedOption >= 0 && selectedOption <= Options.Length-1) {
                string monsterName = Options[selectedOption].GetComponentInChildren<TextMeshPro>().text.ToLower();
                if(monsterName.Equals(MonsterTypeManager.currentMonster.name.ToLower())) {
                    GameOverScript.Win();
                    StopAllAudio();
                }
                else {
                    GameOverScript.Lose();
                    StopAllAudio();
                }
            }
        } 
    }
}

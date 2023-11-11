using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButton : MonoBehaviour
{
    public MainMenuManager mmm;
    void OnMouseDown() {
        StartCoroutine(mmm.ShowCredits());
    }
}

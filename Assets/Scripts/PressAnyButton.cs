using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyButton : MonoBehaviour
{
    public float FlickerSpeed = 1f;
    private SpriteRenderer sr;

    // Update is called once per frame
    void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn() {
        yield return TransitionManager.Fade(sr, 0f, 1f, FlickerSpeed);
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut() {
        yield return TransitionManager.Fade(sr, 1f, 0f, FlickerSpeed);
        StartCoroutine(FadeIn());
    }
}

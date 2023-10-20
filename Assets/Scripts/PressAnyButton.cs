using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyButton : MonoBehaviour
{
    public float FlickerSpeed = 0.01f;
    private SpriteRenderer sr;

    // Update is called once per frame
    void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn() {
        float opactiy = 0f;
        while (opactiy < 1f) {
            opactiy += FlickerSpeed;
            if(opactiy > 1f) opactiy = 1f;
            sr.color = new Color(1, 1, 1, opactiy);
            yield return null;
        }
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut() {
        float opactiy = 1f;
        while (opactiy > 0f) {
            opactiy -= FlickerSpeed;
            if(opactiy < 0f) opactiy = 0f;
            sr.color = new Color(1, 1, 1, opactiy);
            yield return null;
        }
        StartCoroutine(FadeIn());
    }
}

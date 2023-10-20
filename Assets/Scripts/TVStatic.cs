using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TVStatic : MonoBehaviour
{
    public Sprite[] tvStatic;
    private SpriteRenderer sr;
    private int spriteIndex;

    void OnEnable() {
        if(sr == null) sr = GetComponent<SpriteRenderer>();
        spriteIndex = 0;
        StopAllCoroutines();
        StartCoroutine(LoopThroughSprites());
    }

    private IEnumerator LoopThroughSprites() {
        while(true) {
            sr.sprite = tvStatic[spriteIndex];
            if(spriteIndex++ >= tvStatic.Length-1) spriteIndex = 0;
            yield return new WaitForSeconds(0.03f);
        }
    }

    void OnDisable() {
        StopAllCoroutines();
    }
}

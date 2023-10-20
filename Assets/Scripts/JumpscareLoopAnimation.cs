using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpscareLoopAnimation : MonoBehaviour
{
    public Sprite[] sprites;
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
            sr.sprite = sprites[spriteIndex];
            if(spriteIndex++ >= sprites.Length-1) spriteIndex = 0;
            yield return null;
        }
    }

    void OnDisable() {
        StopAllCoroutines();
    }
}

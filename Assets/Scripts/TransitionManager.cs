using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

// For asynchronously smoothly interpolating between two values, (color, scale, position) 
public class TransitionManager
{
    public static IEnumerator Fade(SpriteRenderer s, float startValue, float endValue, float durationInSeconds) {
        float startTime = Time.time;
        while(Time.time - startTime < durationInSeconds) {
            float t = (Time.time - startTime) / durationInSeconds;
            s.color = new Color(0, 0, 0, Mathf.SmoothStep(startValue, endValue, t));
            yield return null;
        }
        s.color = new Color(0, 0, 0, endValue);
    }

    public static IEnumerator Scale(Transform transform, bool xTrueYFalse, float startValue, float endValue, float durationInSeconds) {
        float startTime = Time.time;
        if(xTrueYFalse) {
            while(Time.time - startTime < durationInSeconds) {
                float t = (Time.time - startTime) / durationInSeconds;
                transform.localScale = new Vector3(Mathf.SmoothStep(startValue, endValue, t), transform.localScale.y, transform.localScale.z);
                yield return null;
            }
            transform.localScale = new Vector3(transform.localScale.x, endValue, transform.localScale.z);
        } 
        else {
            while(Time.time - startTime < durationInSeconds) {
                float t = (Time.time - startTime) / durationInSeconds;
                transform.localScale = new Vector3(transform.localScale.y, Mathf.SmoothStep(startValue, endValue, t), transform.localScale.z);
                yield return null;
            }
            transform.localScale = new Vector3(endValue, transform.localScale.y, transform.localScale.z);
        }
    }

    public static IEnumerator ScaleLinear(Transform transform, bool xTrueYFalse, float startValue, float endValue, float durationInSeconds) {
        float startTime = Time.time;
        if(xTrueYFalse) {
            while(Time.time - startTime < durationInSeconds) {
                float t = (Time.time - startTime) / durationInSeconds;
                float val = startValue + t*(endValue-startValue);
                transform.localScale = new Vector3(val, transform.localScale.y, transform.localScale.z);
                yield return null;
            }
            transform.localScale = new Vector3(transform.localScale.x, endValue, transform.localScale.z);
        } 
        else {
            while(Time.time - startTime < durationInSeconds) {
                float t = (Time.time - startTime) / durationInSeconds;
                float val = startValue + t*(endValue-startValue);
                transform.localScale = new Vector3(transform.localScale.y, val, transform.localScale.z);
                yield return null;
            }
            transform.localScale = new Vector3(endValue, transform.localScale.y, transform.localScale.z);
        }
    }

    public static IEnumerator Translate(Transform transform, bool xTrueYFalse, float startValue, float endValue, float durationInSeconds) {
        float startTime = Time.time;
        if(xTrueYFalse) {
            while(Time.time - startTime < durationInSeconds) {
                float t = (Time.time - startTime) / durationInSeconds;
                transform.localPosition = new Vector3(Mathf.SmoothStep(startValue, endValue, t), transform.localPosition.y, transform.localPosition.z);
                yield return null;
            }
            transform.localPosition = new Vector3(endValue, transform.localPosition.y, transform.localPosition.z);
        } 
        else {
            while(Time.time - startTime < durationInSeconds) {
                float t = (Time.time - startTime) / durationInSeconds;
                transform.localPosition = new Vector3(transform.localPosition.y, Mathf.SmoothStep(startValue, endValue, t), transform.localPosition.z);
                yield return null;
            }
            transform.localPosition = new Vector3(transform.localPosition.x, endValue, transform.localPosition.z);
        }   
    }
}

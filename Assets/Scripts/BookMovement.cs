using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class BookMovement : MonoBehaviour
{
    public Transform startMarker;
    public Transform endMarker;

    private bool bookPos; // true = up, false = down
    private float speed = 0.1f;

    void Start() {
        transform.position = endMarker.position;
        bookPos = false;
    }

    public IEnumerator MoveUp() {
        bookPos = true;
        while(transform.position.y < startMarker.position.y) {
            transform.position += new Vector3(0, speed, 0);
            yield return null;
        }
    }

    public IEnumerator MoveDown() {
        bookPos = false;
        while(transform.position.y > endMarker.position.y) {
            transform.position -= new Vector3(0, speed, 0);
            yield return null;
        }
    }

    void Update() {
        if(Input.GetButtonDown("Jump")) {
            StopAllCoroutines();
            if(bookPos == true) {
                bookPos = false;
                StartCoroutine(MoveDown());
            }
            else {
                bookPos = true;
                StartCoroutine(MoveUp());
            }
        }
    }
}
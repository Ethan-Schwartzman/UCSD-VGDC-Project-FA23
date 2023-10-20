using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageTurner : MonoBehaviour
{
    public GameObject[] Pages;
    
    private float xOffset = 1.85f;
    private float pageTurnTime = 0.015f; // smaller = slower
    private int currentPage;
    private bool bookInFocus;
    private int axisReset;
    private BookMovement bookMovement;

    void Start() {
        currentPage = 0;
        bookMovement = transform.parent.GetComponent<BookMovement>();
        axisReset = 0;
        int numPages = Pages.Length;
        
        // Since the page turns in sets of 2, an empty gameobject
        // is added as the last page when the number of pages is even
        if(Pages.Length % 2 == 0) {
            // Copy page gameobjects to an array that's one bigger
            GameObject[] temp = new GameObject[Pages.Length+1];
            for(int i = 0; i < Pages.Length; i++) {
                temp[i] = Pages[i];
            }

            // Create the dummy page and add it to the array
            GameObject dummyPage = new GameObject("Dummy Page");
            dummyPage.transform.SetParent(this.transform);
            temp[Pages.Length] = dummyPage;
            Pages = temp;
        }

        // Set odd numbered pages on the left
        // and even numbered pages on the right
        for(int i = 0; i < numPages; i++) {
            // Get the SpriteRenderer and Transform of the gameobject 
            // that holds the content of the page
            SpriteRenderer sr = Pages[i].GetComponentInChildren<SpriteRenderer>();
            Transform p = sr.transform;

            // If the page is even...
            if(i % 2 == 0) {
                // Correctly scale and position the right pages
                p.localPosition = new Vector3(xOffset, 0, 0);
                p.parent.transform.localScale = new Vector3(1, 1, 1);
                // Earlier pages should be a higher layer
                sr.sortingOrder = 10 + Pages.Length-i;
            }
            // If the page is odd...
            else {
                // rotate so it's not like reading in a mirror
                p.localPosition = new Vector3(xOffset, 0, 0);
                p.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
                p.parent.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));

                // Hide all the left pages (scale to 0)
                p.parent.transform.localScale = new Vector3(0, 1, 1);
                // Earlier pages should be a lower layer
                sr.sortingOrder = 10 + i;
            }
        }
    }

    public void NextPage() {
        // + 2 because there's a page on the left and right
        if(currentPage + 2 < Pages.Length) {
            StartCoroutine(TurnPageR2L());
            currentPage += 2;
        }
    }

    public void PreviousPage() {
        // - 2 because there's a page on the left and right
        if(currentPage - 2 >= 0) {
            StartCoroutine(TurnPageL2R());
            currentPage -= 2;
        }
    }

    private IEnumerator TurnPageR2L() {
        float s = 1f;
        GameObject p1 = Pages[currentPage];
        GameObject p2 = Pages[currentPage+1];
        
        // Turn the right page halfway
        while (s > 0) {
            s -= pageTurnTime;
            p1.transform.localScale = new Vector3(s, 1, 1);
            yield return null;
        }
        p1.transform.localScale = new Vector3(0, 1, 1);

        // Finish turning the page (other side of the paper)
        while (s < 1) {
            s += pageTurnTime;
            p2.transform.localScale = new Vector3(s, 1, 1);
            yield return null;
        }
        p2.transform.localScale = new Vector3(1, 1, 1);
    }

    private IEnumerator TurnPageL2R() {
        float s = 1f;
        GameObject p1 = Pages[currentPage-1];
        GameObject p2 = Pages[currentPage-2];
        
        // Turn the Left page halfway
        while (s > 0) {
            s -= pageTurnTime;
            p1.transform.localScale = new Vector3(s, 1, 1);
            yield return null;
        }
        p1.transform.localScale = new Vector3(0, 1, 1);

        // Finish turning the page (other side of the paper)
        while (s < 1) {
            s += pageTurnTime;
            p2.transform.localScale = new Vector3(s, 1, 1);
            yield return null;
        }
        p1.transform.localScale = new Vector3(0, 1, 1);
    }

    void Update() {
        // axisReset is used to prevent spam turning the pages
        // by holding down a direction
        float horizontalValue = Input.GetAxisRaw("Horizontal"); 
        if(axisReset != 1 && horizontalValue > 0.02) {
            axisReset = 1;
            bookMovement.StopAllCoroutines();
            bookMovement.StartCoroutine(bookMovement.MoveUp());
            NextPage();
        }
        else if(axisReset != -1 && horizontalValue < -0.02){
            axisReset = -1;
            bookMovement.StopAllCoroutines();
            bookMovement.StartCoroutine(bookMovement.MoveUp());
            PreviousPage();
        }
        else if(horizontalValue <= 0.02 && horizontalValue >= -0.02){
            axisReset = 0;
        }
    }
}

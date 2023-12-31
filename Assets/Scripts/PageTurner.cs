using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PageTurner : MonoBehaviour
{
    public GameObject[] Pages;
    public GameObject MonsterSelectionPage;
    public MonsterSelection MonsterSelectionScript;
    
    private float xOffset = 1.85f;
    //private float pageTurnTime = 0.015f; // smaller = slower
    private float pageTurnSeconds = 0.15f;
    private int currentPage;
    private bool bookInFocus;
    private int axisReset;
    private BookMovement bookMovement;
    //private bool pageDoneTurning;
    private bool selectingMonster;

    void Start() {
        currentPage = 0;
        bookMovement = transform.parent.GetComponent<BookMovement>();
        axisReset = 0;
        //pageDoneTurning = true;
        int numPages = Pages.Length+1;
        MonsterSelectionScript = MonsterSelectionPage.GetComponent<MonsterSelection>(); 
        selectingMonster = false;
        
        // Since the page turns in sets of 2, an empty gameobject
        // is added as the last page when the number of pages is even
        if(Pages.Length % 2 == 0) {
            // Copy page gameobjects to an array that's two bigger
            GameObject[] temp = new GameObject[Pages.Length+2];
            for(int i = 0; i < Pages.Length; i++) {
                temp[i] = Pages[i];
            }

            // Add the monster selection page
            temp[Pages.Length] = MonsterSelectionPage;

            // Create the dummy page and add it to the array
            GameObject dummyPage = new GameObject("Dummy Page");
            dummyPage.transform.SetParent(this.transform);
            temp[Pages.Length+1] = dummyPage;
            Pages = temp;
        }
        else {
            // Copy page gameobjects to an array that's one bigger
            GameObject[] temp = new GameObject[Pages.Length+1];
            for(int i = 0; i < Pages.Length; i++) {
                temp[i] = Pages[i];
            }

            // Add the monster selection page
            temp[Pages.Length] = MonsterSelectionPage;
            Pages = temp;
        }

        SetupPages(numPages);
    }

    public void SetupPages(int numPages) {
        // Set odd numbered pages on the left
        // and even numbered pages on the right
        for(int i = 0; i < numPages; i++) {
            bool selectionPage = i == numPages-1; // TODO typo

            // Set page active
            Pages[i].SetActive(true);

            // Get the SpriteRenderer and Transform of the gameobject 
            // that holds the content of the page
            SpriteRenderer sr = Pages[i].GetComponentInChildren<SpriteRenderer>();
            Transform p = sr.transform;
            p.localPosition = new Vector3(xOffset, 0, 0);

            // If the page is even...
            if(i % 2 == 0) {
                // Correctly scale and position the right pages
                p.parent.transform.localScale = new Vector3(1, 1, 1);
                // Earlier pages should be a higher layer
                if(!selectionPage) sr.sortingOrder = 10 + Pages.Length-i;
            }
            // If the page is odd...
            else {
                // rotate so it's not like reading in a mirror
                
                p.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
                p.parent.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));

                // Hide all the left pages (scale to 0)
                p.parent.transform.localScale = new Vector3(0, 1, 1);
                
                // Earlier pages should be a lower layer
                if(!selectionPage) sr.sortingOrder = 10 + i;
            }
        }
    }

    public void NextPage() {
        // + 2 because there's a page on the left and right
        if(/*pageDoneTurning && */currentPage + 2 < Pages.Length) {
            StartCoroutine(TurnPageR2L());
            //TurnPageR2L();
            currentPage += 2;
        }
        else {
            MonsterSelectionScript.SelectNext();
            selectingMonster = true;
        }
    }

    public void PreviousPage() {
        // - 2 because there's a page on the left and right
        if(/*pageDoneTurning && */ !selectingMonster && currentPage - 2 >= 0) {
            StartCoroutine(TurnPageL2R());
            //TurnPageL2R();
            currentPage -= 2;
        }
        else if(selectingMonster) {
            MonsterSelectionScript.SelectPrevious();
        }
    }

    private IEnumerator TurnPageR2L() {
        GameObject p1 = Pages[currentPage];
        GameObject p2 = Pages[currentPage+1];
        //pageDoneTurning = false;
        
        // Turn the right page halfway
        p1.transform.localScale = new Vector3(1, 1, 1);
        yield return TransitionManager.ScaleLinear(p1.transform, true, 1f, 0f, pageTurnSeconds);

        // Finish turning the page (other side of the paper)
        p2.transform.localScale = new Vector3(0, 1, 1);
        yield return TransitionManager.ScaleLinear(p2.transform, true, 0f, 1f, pageTurnSeconds);
    }

    private IEnumerator TurnPageL2R() {
        GameObject p1 = Pages[currentPage-1];
        GameObject p2 = Pages[currentPage-2];
        //pageDoneTurning = false;
        
        p1.transform.localScale = new Vector3(1, 1, 1);
        yield return TransitionManager.ScaleLinear(p1.transform, true, 1f, 0f, pageTurnSeconds);

        // Finish turning the page (other side of the paper)
        p2.transform.localScale = new Vector3(0, 1, 1);
        yield return TransitionManager.ScaleLinear(p2.transform, true, 0f, 1f, pageTurnSeconds);
        //pageDoneTurning = true;
    }

    public void SetNotSelectingMonster() {
        selectingMonster = false;
    }

    public float GetXOffset() {
        return xOffset;
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

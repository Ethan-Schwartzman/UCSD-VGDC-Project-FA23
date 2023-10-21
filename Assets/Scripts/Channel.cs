using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Channel : MonoBehaviour
{
    public GameObject[] MonsterPositions;
    public GameObject[] WildlifePositions;
    public GameObject MonsterSpots;
    public GameObject WildlifeSpots;

    public void GameBegin() {
        //MonsterPositions = new GameObject[MonsterSpots.transform.childCount];
        //for (int i = 0; i < MonsterSpots.transform.childCount; i++) {
        //    MonsterPositions[i] = MonsterSpots.transform.GetChild(i).gameObject;
       // }
        if(WildlifeSpots != null) {
            WildlifePositions = new GameObject[WildlifeSpots.transform.childCount];
                for (int i = 0; i < WildlifeSpots.transform.childCount; i++) {
                    WildlifePositions[i] = WildlifeSpots.transform.GetChild(i).gameObject;
                }
        }
        //Debug.Log("monster pos length" + MonsterPositions.Length);
        //Debug.Log("Wildlife pos length" + WildlifePositions.Length);
    }

    public GameObject GetRandomMonsterPosition() {
        if(MonsterPositions.Length == 0) Debug.Log("Monster positions have not yet been set up on this channel");
        GameObject m = MonsterPositions[Random.Range(0, MonsterPositions.Length)];
        if(m == null) Debug.Log("Null value for this monster position");

        // Set the monster's sprite here
        // Eg.
        // Sprite monsterSprite = MonsterTypeManager.currentMonster.Sprites[0];
        // m.GetComponent<SpriteRenderer>().sprite = monsterSprite;

        return m;
    }

    public GameObject GetRandomWildlifePosition() {
        if(WildlifePositions.Length == 0) Debug.Log("Wildlife positions have not yet been set up on this channel");
        GameObject w = WildlifePositions[Random.Range(0, WildlifePositions.Length)];
        if(w == null) Debug.Log("Null value for this wildlife position");

        // Set the creature's sprite here

        return w;
    }
}

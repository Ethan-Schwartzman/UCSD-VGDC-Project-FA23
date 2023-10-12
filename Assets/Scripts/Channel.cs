using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Channel : MonoBehaviour
{
    public GameObject[] MonsterPositions;
    public GameObject[] WildlifePositions;

    public GameObject GetRandomMonsterPosition() {
        if(MonsterPositions.Length == 0) Debug.Log("Monster positions have not yet been set up on this channel");
        GameObject m = MonsterPositions[Random.Range(0, MonsterPositions.Length)];
        if(m == null) Debug.Log("Null value for this monster position");

        // Set the monster's sprite here

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTypeManager : MonoBehaviour {
    public MonsterType[] MonsterTypes;

    public static MonsterType currentMonster;

    void Start() {
        currentMonster = ChooseRandomMonsterType();
        Debug.Log(currentMonster);
    }
    private MonsterType ChooseRandomMonsterType() {
        return MonsterTypes[Random.Range(0, MonsterTypes.Length)];
    }
    public MonsterType getCurrentMonster() {
        return currentMonster;
    }
}

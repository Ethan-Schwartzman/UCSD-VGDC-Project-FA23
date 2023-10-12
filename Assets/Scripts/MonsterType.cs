using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "MonsterType", order = 1)]
public class MonsterType : ScriptableObject
{
    public String Name;
    public Sprite[] Sprites;
}

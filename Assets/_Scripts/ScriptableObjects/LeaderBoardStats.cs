using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LeaderBoard", menuName = "ScriptableObjects/LeaderBoard")]
public class LeaderBoardStats : ScriptableObject
{
    public string playerName;

    public float time;
}

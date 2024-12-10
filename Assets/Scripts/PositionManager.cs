using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
    public List<Transform> playerPositions; // Set positions in the Unity Editor
    public List<Transform> monsterPositions; // Set positions in the Unity Editor

    public Transform GetPlayerPosition(int index)
    {
        return playerPositions[index];
    }

    public Transform GetMonsterPosition(int index)
    {
        return monsterPositions[index];
    }

    public void ArrangeFighters(List<GameObject> players, List<GameObject> monsters)
    {
        // Arrange players
        for (int i = 0; i < players.Count; i++)
        {
            players[i].transform.position = GetPlayerPosition(i).position;
        }

        // Arrange monsters
        for (int i = 0; i < monsters.Count; i++)
        {
            monsters[i].transform.position = GetMonsterPosition(i).position;
        }
    }
}

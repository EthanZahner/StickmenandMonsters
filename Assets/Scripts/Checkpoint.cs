using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] DiceRoll diceRoll = DiceRoll.D6;
    readonly DiceRollSystem system = new();

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            var result = system.Roll(diceRoll);
            Debug.Log(result);
        }
    }
}

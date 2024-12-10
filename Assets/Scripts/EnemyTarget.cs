using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    private GameController gameController;

    void Start()
    {
        gameController = GameObject.Find("GameControllerObject").GetComponent<GameController>();
    }

    void OnMouseDown()
    {
        if (tag == "enemy") // Only allow selection of untargeted enemies
        {
            gameController.SelectTarget(this.gameObject);
        }
    }

}

using UnityEngine;

public class PowerPelletController : PelletController
{
    [SerializeField] private float powerUpDuration = 1f;
    
    /*
     * Eat: Add points (power up PacMan if PowerPellet)
     */
    protected override void Eat()
    {
        // TODO(lemmtopia): Power up PacMan
        GameController.Instance.AddScore(points);

    }
}

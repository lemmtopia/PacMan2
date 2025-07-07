using UnityEngine;

public class PelletController : MonoBehaviour
{
    [SerializeField] protected int points = 10;

    /*
     * Eat: Add points (power up PacMan if PowerPellet)
     */
    protected virtual void Eat()
    {
        GameController.Instance.AddScore(points);
    }

    /*
     * OnTriggerEnter2D(Collider2D collision): Get collisions with triggers (collisions that do not affect our physics state, only trigger events)
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PacMan"))
        {
            // Pellet collision
            Eat();
            gameObject.SetActive(false);
        }
    }
}

using UnityEngine;

public class PelletController : MonoBehaviour
{
    [SerializeField] private int points = 10;

    /*
     * OnTriggerEnter2D(Collider2D collision): Get collisions with triggers (collisions that do not affect our physics state, only trigger events)
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PacMan"))
        {
            // Pellet collision
            Destroy(gameObject);
        }
    }
}

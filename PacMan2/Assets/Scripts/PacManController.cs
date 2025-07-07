using UnityEngine;

public class PacManController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float wallCheckDistance = 0.25f;
    [SerializeField] private float wallCheckBoxSideLength;

    private Vector2 desiredDirection;
    private Vector2 direction;
    private Rigidbody2D rb;

    /*
    * Start: Runs at the first frame this object is active
    */
    private void Start()
    {
        // Get components
        rb = GetComponent<Rigidbody2D>();

        // Setup direction
        direction = Vector2.right;
        desiredDirection = Vector2.right;
    }

    /*
    * Update: Runs every frame
    */
    private void Update()
    {
        // Get and maybe apply the desired direction
        GetDesiredDirection();
        if (!CheckWallAt(desiredDirection, wallCheckDistance))
        {
            Debug.Log("Changes direction!");
            direction = desiredDirection;
        }

        // Apply movement vector
        Vector2 movementVector = direction * moveSpeed;
        rb.linearVelocity = movementVector;
    }

    /*
     * GetDesiredDirection: Gets the input direction (Will not apply it yet!)
     */
    private void GetDesiredDirection()
    {
        // Get key inputs
        bool keyRight = Input.GetKeyDown(KeyCode.RightArrow);
        bool keyLeft = Input.GetKeyDown(KeyCode.LeftArrow);
        bool keyUp = Input.GetKeyDown(KeyCode.UpArrow);
        bool keyDown = Input.GetKeyDown(KeyCode.DownArrow);

        // Ugly if statement, but necessary if statement (I mean, I could probably use arrays too)
        if (keyRight)
        {
            desiredDirection = Vector2.right;
            Debug.Log("Desired direction: Right!");
        }
        else if (keyLeft)
        {
            desiredDirection = Vector2.left;
            Debug.Log("Desired direction: Left!");
        }
        else if (keyUp)
        {
            desiredDirection = Vector2.up;
            Debug.Log("Desired direction: Up!");
        }
        else if (keyDown)
        {
            desiredDirection = Vector2.down;
            Debug.Log("Desired direction: Down");
        }
    }

    /*
     * CheckWallAt(Vector2 direction, float distance): Checks if there's a wall between PacMan and a point given by P = (direction, distance)
     */
    public bool CheckWallAt(Vector2 direction, float distance)
    {
        Vector2 boxSize = new Vector2(wallCheckBoxSideLength, wallCheckBoxSideLength);
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxSize, 0, direction, distance, LayerMask.GetMask("Walls"));
        if (hit)
        { 
            Debug.Log(hit.point);
            return true;
        }

        return false;
    }
}

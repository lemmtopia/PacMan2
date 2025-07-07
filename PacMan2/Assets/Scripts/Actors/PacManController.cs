using UnityEngine;
using UnityEngine.Tilemaps;

public class PacManController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float wallCheckDistance = 0.25f;
    [SerializeField] private float wallCheckCircleRadius = 0.25f;

    [SerializeField] private GameObject spriteContainer;
    [SerializeField] private Animator animator;
    [SerializeField] private Grid grid;
    
    Vector2 startingDirection;

    private Vector2 desiredDirection;
    private Vector2 direction;
    private Rigidbody2D rb;
    private float defaultAnimatorSpeed;

    /*
    * Start: Runs at the first frame this object is active
    */
    private void Start()
    {
        // Get components
        rb = GetComponent<Rigidbody2D>();
        animator = spriteContainer.GetComponentInChildren<Animator>();

        // Setup direction
        startingDirection = Vector2.right;

        direction = startingDirection;
        desiredDirection = Vector2.right;

        // Misc
        defaultAnimatorSpeed = animator.speed;
    }

    /*
    * Update: Runs every frame
    */
    private void Update()
    {
        // Get and maybe apply the desired direction
        GetDesiredDirection();
        if (CheckWallAt(desiredDirection, wallCheckDistance))
        {
            // Pause animation if we do not want to go in a different direction
            if (direction == desiredDirection)
            {
                animator.speed = 0;
            }
        }
        else
        {
            direction = desiredDirection;
            animator.speed = defaultAnimatorSpeed;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            spriteContainer.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
    private void FixedUpdate()
    {
        // Apply movement vector
        Vector2 movementVector = direction * moveSpeed;
        rb.linearVelocity = movementVector;
    }

    /*
     * ResetPositionAndDirection: Resets position and direction
     */
    public void ResetPositionAndDirection()
    {
        transform.position = new Vector3(0, -0.5f, 0); 
        direction = startingDirection;
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
            //Debug.Log("Desired direction: Right!");
        }
        else if (keyLeft)
        {
            desiredDirection = Vector2.left;
            //Debug.Log("Desired direction: Left!");
        }
        else if (keyUp)
        {
            desiredDirection = Vector2.up;
            //Debug.Log("Desired direction: Up!");
        }
        else if (keyDown)
        {
            desiredDirection = Vector2.down;
            //Debug.Log("Desired direction: Down");
        }
    }

    /*
     * CheckWallAt(Vector2 direction, float distance): Checks if there's a wall between PacMan and a point given by P = (direction, distance)
     */
    public bool CheckWallAt(Vector2 direction, float distance)
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, wallCheckCircleRadius, direction, distance, LayerMask.GetMask("Walls"));
        if (hit)
        {
            return true;
        }

        return false;
    }
}

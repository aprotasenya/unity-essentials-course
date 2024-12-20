using UnityEngine;

public class PlayerController2D : PlayerController
{
    // Public variables
    public float speed = 5f; // The speed at which the player moves
    public bool canMoveDiagonally = true; // Controls whether the player can move diagonally
    [SerializeField] private AudioClip pickUpSound;
    private AudioSource audioSource;


    // Private variables 
    private Rigidbody2D rb2d; // Reference to the Rigidbody2D component attached to the player
    private Vector2 movement; // Stores the direction of player movement
    private bool isMovingHorizontally = true; // Flag to track if the player is moving horizontally

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Initialize the Rigidbody2D component
        rb2d = GetComponent<Rigidbody2D>();
        // Prevent the player from rotating
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        // Get player input from keyboard or controller
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Check if diagonal movement is allowed
        if (canMoveDiagonally)
        {
            // Set movement direction based on input
            movement = new Vector2(horizontalInput, verticalInput).normalized;
            // Optionally rotate the player based on movement direction
            RotatePlayer(movement.x, movement.y);
        }
        else
        {
            // Determine the priority of movement based on input
            if (horizontalInput != 0)
            {
                isMovingHorizontally = true;
            }
            else if (verticalInput != 0)
            {
                isMovingHorizontally = false;
            }

            // Set movement direction and optionally rotate the player
            if (isMovingHorizontally)
            {
                movement = new Vector2(horizontalInput, 0);
                RotatePlayer(movement.x, 0);
            }
            else
            {
                movement = new Vector2(0, verticalInput);
                RotatePlayer(0, movement.y);
            }
        }
    }

    void FixedUpdate()
    {
        // Apply movement to the player in FixedUpdate for physics consistency
        rb2d.linearVelocity = movement * speed;
    }

    void RotatePlayer(float x, float y)
    {
        if (x == 0 && y == 0) return;

        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collectible2D>() != null) {
            audioSource.PlayOneShot(pickUpSound);
        }
    }
}

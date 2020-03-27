using UnityEngine;
using UnityEngine.EventSystems;

// Main player controller. Handles player movement/color/collisions
public class PlayerController : MonoBehaviour
{
    private new Rigidbody rigidbody;

    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private float jumpSpeed = 5.0f;

    // Start animation values
    [SerializeField]
    private float animationDuration = 3.0f;
    private float startTime;

    private Color playerColor;

    // Player movement values
    private Vector3 moveVector;
    private int side = 0; // -1 from right-to-left, 1 - from left-to-right, 0 - default value
    private float curDestinationPos = 0.0f; // For smooth position changing
    private float movementEdge = 2.6f; // Edge of left/right player movement limit
    private float positionChangeSpeed = 5.0f;

    // Mobile input
    private PlayerMoveDirection playerMoveDirection = PlayerMoveDirection.None;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        startTime = Time.time;

        // Changing player's color according to main color in GameController
        playerColor = (FindObjectOfType<GameController>() as GameController).mainColor;
        GetComponent<Renderer>().material.color = playerColor;

        //Handling mobile swipe callback
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
    }

    private void Update()
    {
        moveVector = Vector3.zero; // Clearing moving vector... 

        // Wait until start animation ends
        if (Time.time - startTime <= animationDuration)
        {
            rigidbody.velocity = Vector3.forward * speed;
            return;
        }

#if UNITY_ANDROID && !UNITY_EDITOR
        Debug.Log("unity adndoird");
        HandleMobileHorizontalInput();
        CalculateHorizontalMovement();
        HandleMobileVerticalInput();
#else
        HandlePCHorizontalInput();
        CalculateHorizontalMovement();
        HandlePCVerticalInput(); // Including jump implementation
#endif


        // Z axis
        moveVector.z = speed;

        rigidbody.velocity = moveVector;
    }

    // --------- COLLISION DETECTION ------
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Obstacle")
        {
            if (collision.collider.gameObject.GetComponent<Renderer>().material.color != playerColor)
            {
                Debug.Log("Game Over");
                ExecuteEvents.Execute<IEventHandler>(gameObject, null,
                (a, b) => a.OnGameOver());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            Destroy(other.gameObject);

            // Send Event to score controller to add a coin
            ExecuteEvents.Execute<IEventHandler>(gameObject, null,
                (a, b) => a.OnCoinPickedUp());
        }
    }

    // -------- MOBILE INPUT HANDLERS -------

    private void HandleMobileHorizontalInput()
    {
        if (playerMoveDirection == PlayerMoveDirection.Left)
        {
            curDestinationPos = curDestinationPos <= -movementEdge ? -movementEdge : curDestinationPos - movementEdge;
            side = -1;
            playerMoveDirection = PlayerMoveDirection.None;
        }
        if (playerMoveDirection == PlayerMoveDirection.Right)
        {
            curDestinationPos = curDestinationPos >= movementEdge ? movementEdge : curDestinationPos + movementEdge;
            side = 1;
            playerMoveDirection = PlayerMoveDirection.None;
        }
    }

    private void HandleMobileVerticalInput()
    {
        if (playerMoveDirection == PlayerMoveDirection.Up && Mathf.Abs(rigidbody.velocity.y) == 0.0f)
        {
            moveVector.y = jumpSpeed * 2f;
            playerMoveDirection = PlayerMoveDirection.None;
        }
        else
            moveVector.y = rigidbody.velocity.y + (Physics.gravity.y * Time.deltaTime * 2);
    }

    private void SwipeDetector_OnSwipe(SwipeData data)
    {
        Debug.Log("Swipe: " + data.Direction);
        switch (data.Direction)
        {
            case SwipeDirection.Up:

                playerMoveDirection = PlayerMoveDirection.Up;
                break;
            case SwipeDirection.Left:
                playerMoveDirection = PlayerMoveDirection.Left;
                break;
            case SwipeDirection.Right:
                playerMoveDirection = PlayerMoveDirection.Right;
                break;
        }
    }

    // -------- PC INPUT HANDLERS -------

    private void HandlePCHorizontalInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            curDestinationPos = curDestinationPos <= -movementEdge ? -movementEdge : curDestinationPos - movementEdge;
            side = -1;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            curDestinationPos = curDestinationPos >= movementEdge ? movementEdge : curDestinationPos + movementEdge;
            side = 1;
        }
    }

    private void HandlePCVerticalInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rigidbody.velocity.y) <= 0.5f)
            moveVector.y = jumpSpeed * 2f;
        else
            moveVector.y = rigidbody.velocity.y + (Physics.gravity.y * Time.deltaTime * 2);
    }

    //Horizontal input calculations
    private void CalculateHorizontalMovement()
    {
        if (ComparePositions(transform.position.x, curDestinationPos))
            transform.position = new Vector3(curDestinationPos, transform.position.y, transform.position.z);
        else
            moveVector.x = side * positionChangeSpeed;
    }

    private bool ComparePositions(float currentPos, float destinationPos) // For smooth player movement through 3 pos
    {
        if (destinationPos == movementEdge)
            return currentPos > destinationPos;
        else if (destinationPos == -movementEdge)
            return currentPos < destinationPos;
        else if (destinationPos == 0)
            return side == -1 ? currentPos < destinationPos : destinationPos < currentPos;
        else
            return false;
    }
    
    // Speed increasing at level up
    public void SetSpeed(float modifier)
    {
        speed += modifier;
        positionChangeSpeed += modifier / 2;
    }
}

public enum PlayerMoveDirection
{
    None,
    Up,
    Left,
    Right
}

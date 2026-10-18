using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float walkSpeed = 5f;
    public float leftRight;
    public float frontBack;

    public bool isRunning { get; private set; }

    public LayerMask groundLayer;
    public Transform groundCheck;   
    public float groundDistance = 0.2f;

    public float minJumpForce = 5f;
    public float maxJumpForce = 10f;
    public float maxJumpChargeTime = 3f;

    private float jumpChargeTime = 0f;
    private bool isChargingJump = false;
    
    public bool isGrounded { get; private set; }

    public bool doubleJumpActive = false;

    private bool doubleJumpUsed = false;

    public float doubleJumpForce = 8f;


    private CharacterAnimator characterAnimator;

    

    private float horizontalInput;
    private float verticalInput;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        characterAnimator = GetComponent<CharacterAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if(Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            leftRight = walkSpeed * 2f;
            frontBack = walkSpeed * 2f;
        }
        else
        {
            isRunning = false;
            leftRight = walkSpeed;
            frontBack = walkSpeed;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        if (isGrounded)
        {
            doubleJumpUsed = false;
        }

        if(Input.GetKeyDown(KeyCode.Space) && !isGrounded && doubleJumpActive && !doubleJumpUsed)
        {
            DoubleJump();
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isChargingJump = true;
            jumpChargeTime = 0f;
        }

        if (Input.GetKey(KeyCode.Space) && isChargingJump)
        {
            jumpChargeTime += Time.deltaTime;
            jumpChargeTime = Mathf.Clamp(jumpChargeTime, 0f, maxJumpChargeTime);

            if(jumpChargeTime >= maxJumpChargeTime)
            {
                Jump();
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && isChargingJump)
        {
            Jump();
        }

    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(horizontalInput * leftRight, 0, verticalInput * frontBack) * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }

    void Jump()
    {
        float jumpPercentage = jumpChargeTime / maxJumpChargeTime;
        float jumpForce = Mathf.Lerp(minJumpForce, maxJumpForce, jumpPercentage);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isChargingJump = false;
    }

    void DoubleJump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        rb.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
         doubleJumpUsed = true;

        characterAnimator.PlayDoubleJumpAnimation();
    }
}

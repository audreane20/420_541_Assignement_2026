using UnityEngine;
using System.Collections;

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

    public Transform cameraTransform;


    private CharacterAnimator characterAnimator;

    private Coroutine doubleJumpTimer;


    private float speedMultiplier = 1f;

    private Coroutine speedBoostTimer;

    

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

        isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = walkSpeed * speedMultiplier;

        if (isRunning)
        {
            currentSpeed *= 2f;
        }

        leftRight = currentSpeed;
        frontBack = currentSpeed; 

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

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 direction = forward * verticalInput + right * horizontalInput;

        direction = Vector3.ClampMagnitude(direction, 1f);
            
        rb.MovePosition(rb.position + direction * leftRight * Time.fixedDeltaTime);
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

    public void ActivateDoubleJump()
    {
        if (doubleJumpTimer != null)
        {
            StopCoroutine(doubleJumpTimer);
        }
        doubleJumpTimer = StartCoroutine(DoubleJumpCountdown());
    }

    private IEnumerator DoubleJumpCountdown()
    {
        doubleJumpActive = true;
        yield return new WaitForSeconds(30f);
        doubleJumpActive = false;
        doubleJumpTimer = null;
    }

    public void ActivateSpeedBoost()
    {
        if(speedBoostTimer != null)
        {
            StopCoroutine(speedBoostTimer);

        }

        speedBoostTimer = StartCoroutine(SpeedBoostCountdown());
    }

    private IEnumerator SpeedBoostCountdown()
    {
        speedMultiplier = 2f;
        yield return new WaitForSeconds(5f);
        speedMultiplier = 1f;
        speedBoostTimer = null;
    }
}

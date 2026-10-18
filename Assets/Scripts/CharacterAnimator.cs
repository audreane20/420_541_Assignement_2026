using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private Animator animator;
    private CharacterMovement movement;

    public ParticleSystem doubleJumpParticles;
    public AudioSource doubleJumpSound;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        float movementAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        float speedValue = 0f;

        if (movementAmount > 0.1f)
        {
            if (movement.isRunning)
            {
                speedValue = 1f; // Running

            }
            else
            {
                speedValue = 0.5f; // Walking

            }
        }
        animator.SetFloat("Speed", speedValue);

        animator.SetBool("isGrounded", movement.isGrounded);

    }

    public void PlayDoubleJumpAnimation()
    {
        animator.SetTrigger("Flip");
        if (doubleJumpParticles != null)
        {
            doubleJumpParticles.Play();
        }
        if (doubleJumpSound != null)
        {
            doubleJumpSound.Play();
        }
    }





}

using UnityEngine;
using System.Collections;

public class JumpBoostPickup : MonoBehaviour
{

    [SerializeField] private GameObject visual;
    [SerializeField] private AudioSource pickupSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Collider pickupCollider;

    private void Awake()
    {
        pickupCollider = GetComponent<Collider>();
    
    }

    private void OnTriggerEnter(Collider other)
    { 
        CharacterMovement player = other.GetComponentInParent<CharacterMovement>();
        if (player == null || !pickupCollider.enabled)
        {
            return;
        }

        player.ActivateDoubleJump();
        StartCoroutine(Respawn());
        
    }

    private IEnumerator Respawn()
    {
        pickupCollider.enabled = false;
        visual.SetActive(false);
        if (pickupSound != null)
        {
            pickupSound.Play();
        }
        yield return new WaitForSeconds(30f);

        visual.SetActive(true);
        pickupCollider.enabled = true;
    }

}

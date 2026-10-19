using System;
using UnityEngine;

public class ScorePickup : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSound;

    private bool collected = false;

    private void OnTriggerEnter(Collider other)
    {


        if (collected || other.GetComponentInParent<CharacterMovement>() == null)
        {
            return;
        }

        GameManager.Instance.IncreaseScore(50 * GameManager.Instance.ScoreMultiplier);

        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);

        }
        Destroy(gameObject);
    }
    
}

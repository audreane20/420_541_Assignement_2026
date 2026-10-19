using UnityEngine;
using System.Collections;

public class SpeedBoostPickup : MonoBehaviour
{

    [SerializeField] private GameObject visual;

    [SerializeField] private AudioSource pickupSound;

    private Collider pickupCollider;

    private void Awake()
    {
        pickupCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterMovement player = other.GetComponentInParent<CharacterMovement>();

        if(player == null || !pickupCollider.enabled)
        {
            return;
        }

        player.ActivateSpeedBoost();
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        pickupCollider.enabled = false;
        visual.SetActive(false);

        if(pickupSound != null)
        {
            pickupSound.Play();
        }

        yield return new WaitForSeconds(30f);

        visual.SetActive(true);
        pickupCollider.enabled = true;
    }

}

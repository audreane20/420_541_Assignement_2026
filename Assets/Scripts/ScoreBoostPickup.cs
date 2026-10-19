using System.Collections;
using UnityEngine;

public class ScoreBoostPickup : MonoBehaviour
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

        if (!pickupCollider.enabled || other.GetComponentInParent<CharacterMovement>() == null)
        {
            return;
        }
        GameManager.Instance.ActivateScoreBoost();
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

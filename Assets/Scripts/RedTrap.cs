using UnityEngine;

public class RedTrap : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponentInParent<CharacterMovement>() == null)
        {
            return;
        }
        GameManager.Instance.DecreaseHealth(1);
    }
    
}

using UnityEngine;

public class EndGoal : MonoBehaviour
{

    private bool activated;

    private void OnTriggerEnter(Collider other)
    {
        if(activated || other.GetComponentInParent<CharacterMovement>() == null)
        {
            return;
        }
        activated = true;
        GameManager.Instance.LoadNextLevel();

    }

}

using UnityEngine;

public class PickupMotion : MonoBehaviour
{

    public float roatationSpeed = 90f;
    public float hoverHeight = 0.15f;
    public float hoverSpeed = 2f;

    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, roatationSpeed * Time.deltaTime);

        transform.localPosition = startPosition + Vector3.up * Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
    }

}

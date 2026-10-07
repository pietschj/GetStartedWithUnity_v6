using UnityEngine;

public class CoinRotator : MonoBehaviour
{   
    [Header("Rotation")]
    //these tick boxes appear in the Inspector so each coin can switch motions on or off
    [SerializeField] bool rotate = true;
    //this allows us to set this variable in the Inspector
    [SerializeField] float degreesPerSecond = 90f;

    [Header("Bobbing")]
    [SerializeField] bool bob = true;
    [Tooltip("How far the coin moves up and down from its starting height.")]
    [SerializeField] float bobHeight = 0.25f;
    [Tooltip("Full up-and-down cycles per second.")]
    [SerializeField] float bobSpeed = 1f;
    [Tooltip("Start each coin at a different point in the cycle so they don't all move in step.")]
    [SerializeField] bool randomiseStart = true;

    Vector3 startPosition;
    float timeOffset;

    void Start()
    {
        //remember where the coin was placed so it bobs around that point
        startPosition = transform.localPosition;

        if (randomiseStart)
        {
            timeOffset = Random.Range(0f, 10f);
        }
    }

    void Update()
    {
        if (rotate)
        {
            transform.Rotate(0f, degreesPerSecond * Time.deltaTime, 0f, Space.World);
        }

        if (bob)
        {
            //Mathf.Sin gives a smooth value between -1 and 1, which we scale by bobHeight
            float offset = Mathf.Sin((Time.time + timeOffset) * bobSpeed * 2f * Mathf.PI) * bobHeight;
            transform.localPosition = startPosition + new Vector3(0f, offset, 0f);
        }
    }
}

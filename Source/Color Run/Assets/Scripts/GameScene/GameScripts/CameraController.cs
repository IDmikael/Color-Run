using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform lookAt;
    private Vector3 startOffset;
    private Vector3 moveVector;

    // Start animation properties
    [SerializeField]
    private float animationDuration = 3.0f;

    private float transition = 0.0f;
    private Vector3 animationOffset = new Vector3(0, 5, 5);

    // Start is called before the first frame update
    void Start()
    {
        lookAt = GameObject.FindGameObjectWithTag("Player").transform;
        startOffset = transform.position - lookAt.position;
    }

    private void LateUpdate()
    {
        moveVector = Vector3.zero;
        moveVector = lookAt.position + startOffset;

        moveVector.x = Mathf.Clamp(moveVector.x, -1f, 1f);

        moveVector.y = Mathf.Clamp(moveVector.y, 2.5f, 6.5f);

        if (transition > 1.0f)
        {
            transform.position = moveVector;
        }
        else
        {
            transform.position = Vector3.Lerp(moveVector + animationOffset, moveVector, transition);
            transition += Time.deltaTime / animationDuration;
            transform.LookAt(lookAt.position + Vector3.up);
        }
    }
}

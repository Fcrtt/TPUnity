using UnityEngine;

public class RotateCamera : MonoBehaviour
{

    public float speed = 100;
    private Vector3 axis = new(1, 0, 0);
    public Transform target;

    private void Start()
    {
        transform.LookAt(target.position);

    }
    // Update is called once per frame
    void Update()
    {

        float arrowsInput = Input.GetAxis("Vertical");
        float resultingSpeed = speed * Time.deltaTime * arrowsInput;
        Vector3 targetPosition = target.transform.position;

        transform.RotateAround(target.position, axis, resultingSpeed);
        float currentRotationX = transform.localEulerAngles.x;
        if (currentRotationX > 180.0f)
        {
            currentRotationX -= 360.0f;
        }

        float clampedRotationX = Mathf.Clamp(currentRotationX, -45f, 45f);

        transform.localEulerAngles = new Vector3(clampedRotationX, transform.localEulerAngles.y, transform.localEulerAngles.z);
        transform.LookAt(targetPosition);

    }

}

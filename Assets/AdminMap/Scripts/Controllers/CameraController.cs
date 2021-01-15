using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraTransform;

    public float fastSpeed;
    public float normalSpeed;
    public float movementSpeed;

    public float movementTime;

    public Vector3 position;

    public Quaternion rotation;
    public float rotationDegree;

    public Vector3 zoom;
    public Vector3 zoomDepth;

    private float zoomLimitMin = 2f;
    private float zoomLimitMax = 80f;

    private float moveLimitMin = 0f;
    private float moveLimitMax = 100f;

    void Start()
    {
        position = transform.position;
        rotation = transform.rotation;
        zoom = cameraTransform.localPosition;
    }

    void Update()
    {
        HandlInputMovement();
    }

    void HandlInputMovement()
    {
        DetermineMovementSpeed();

        HandleRotation();

        HandleZooming();

        HandleKeyboardMovement();

        // make movement smooth
        transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, zoom, Time.deltaTime * movementTime);
    }

    void HandleKeyboardMovement()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            MoveAlongY(true);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            MoveAlongY(false);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            MoveAlongX(true);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            MoveAlongX(false);
        }
    }

    void MoveAlongX(bool IsAlong)
    {
        movementSpeed = IsAlong ? movementSpeed : (-1) * movementSpeed;
        position += transform.right * movementSpeed;
        position.x = Mathf.Clamp(position.x, moveLimitMin, moveLimitMax);
    }

    void MoveAlongY(bool IsAlong)
    {
        movementSpeed = IsAlong ? movementSpeed : (-1) * movementSpeed;
        position += transform.forward * movementSpeed;
        position.z = Mathf.Clamp(position.z, moveLimitMin, moveLimitMax);
    }

    void DetermineMovementSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }
    }

    void HandleRotation()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            rotation *= Quaternion.Euler(Vector3.up * rotationDegree);
        }

        if (Input.GetKey(KeyCode.E))
        {
            rotation *= Quaternion.Euler(Vector3.up * -rotationDegree);
        }
    }

    void HandleZooming()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            zoom -= Input.mouseScrollDelta.y * zoomDepth;
        }

        if (Input.GetKey(KeyCode.R))
        {
            zoom += zoomDepth;
        }

        if (Input.GetKey(KeyCode.F))
        {
            zoom -= zoomDepth;
        }

        SetZoomLimits();
    }

    private void SetZoomLimits()
    {
        zoom.z = Mathf.Clamp(zoom.z, moveLimitMin, moveLimitMin);
        zoom.y = Mathf.Clamp(zoom.y, zoomLimitMin, zoomLimitMax);
    }
}

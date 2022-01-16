using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraMovementSpeed = 0.05f;

    private Vector3? basePointerPosition = null;
    private int camXMin, camXMax, camZMin, camZMax;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveCamera(Vector3 pointerPosition)
    {
        if (basePointerPosition.HasValue == false)
        { 
            basePointerPosition = pointerPosition;
        }

        Vector3 newPosition = pointerPosition - basePointerPosition.Value;
        newPosition = new Vector3(newPosition.x, 0, newPosition.y);
        transform.Translate(newPosition * cameraMovementSpeed);
        LimitCameraMovementWithinBounds();
    }

    public void StopCameraMovememt()
    {
        basePointerPosition = null;
    }

    public void SetCameraBounds(int _camXMin, int _camXMax, int _camZMin, int _camZMax)
    {
        camXMin = _camXMin;
        camXMax = _camXMax;
        camZMin = _camZMin;
        camZMax = _camZMax;
    }

    private void LimitCameraMovementWithinBounds()
    { 
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, camXMin, camXMax), 0,
            Mathf.Clamp(transform.position.z, camZMin, camZMax));
    }
}

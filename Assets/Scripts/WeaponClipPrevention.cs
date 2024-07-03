using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponClipPrevention : MonoBehaviour
{
    [SerializeField] private GameObject clipProjector;
    [SerializeField] private float checkDistance;
    [SerializeField] private Vector3 newDirection;
    [SerializeField] private LayerMask layer;
    [SerializeField] private Vector3 offsetFromCamera = new Vector3(0f, -0.2f, 0.5f);
    [SerializeField] private float positionSmoothTime = 0.1f; // Smoothing time for position
    [SerializeField] private float rotationSpeed = 5f; // Rotation speed

    private float lerpPos;
    private RaycastHit hit;
    private WeaponSwayAndBob swayNBob;
    private Transform cameraTransform;
    private Vector3 initialLocalPosition;
    private Vector3 currentVelocity = Vector3.zero;


    private void Start()
    {
        swayNBob = GetComponent<WeaponSwayAndBob>();
        cameraTransform = Camera.main.transform;
        initialLocalPosition = transform.localPosition;
    }

    private void Update()
    {
        if(Physics.Raycast(clipProjector.transform.position, clipProjector.transform.forward, out hit, checkDistance, layer))
        {
            // Get a percentage from 0 to max distance
            lerpPos = 1 - (hit.distance / checkDistance);
        }
        else
        {
            // If we are not hitting anything, set to 0
            lerpPos -= 0.075f;
        }

        lerpPos = Mathf.Clamp01(lerpPos);

        swayNBob.enabled = lerpPos == 0;

        Vector3 targetPosition = cameraTransform.TransformPoint(offsetFromCamera);

        //transform.localPosition = Vector3.Lerp(initialLocalPosition, transform.parent.InverseTransformPoint(targetPosition), lerpPos);
        //transform.localRotation = Quaternion.Lerp(Quaternion.Euler(Vector3.zero), Quaternion.Euler(newDirection), lerpPos);

        Vector3 desiredPosition = Vector3.Lerp(initialLocalPosition, transform.parent.InverseTransformPoint(targetPosition), lerpPos);
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, desiredPosition, ref currentVelocity, positionSmoothTime);

        // Smoothly rotate the weapon
        Quaternion desiredRotation = Quaternion.Lerp(Quaternion.Euler(Vector3.zero), Quaternion.Euler(newDirection), lerpPos);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, desiredRotation, Time.deltaTime * rotationSpeed);
    }
}

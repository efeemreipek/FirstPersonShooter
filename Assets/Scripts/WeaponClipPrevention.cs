using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponClipPrevention : MonoBehaviour
{
    [SerializeField] private GameObject clipProjector;
    [SerializeField] private float checkDistance;
    [SerializeField] private Vector3 newDirection;
    [SerializeField] private LayerMask layer;

    private float lerpPos;
    private RaycastHit hit;
    private WeaponSwayAndBob swayNBob;

    private void Start()
    {
        swayNBob = GetComponent<WeaponSwayAndBob>();
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

        if (lerpPos != 0)
        {
            swayNBob.enabled = false;
        }
        else
        {
            swayNBob.enabled = true;
        }

        transform.localRotation = Quaternion.Lerp(Quaternion.Euler(Vector3.zero), Quaternion.Euler(newDirection), lerpPos);
    }
}

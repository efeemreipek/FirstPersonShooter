using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRange = 5f;

    private Transform _cameraTransform;
    private RaycastHit _hit;
    private PlayerInputControls _playerInputControls;
    private InputAction _interactionAction;



    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
        _playerInputControls = new PlayerInputControls();
    }
    private void OnEnable()
    {
        _interactionAction = _playerInputControls.Player.Interaction;

        _playerInputControls.Enable();
    }
    private void OnDisable()
    {
        _playerInputControls.Disable();
    }

    private void Update()
    {
        if(Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out _hit, interactionRange))
        {
            if(_hit.collider.TryGetComponent(out IInteractable interactable))
            {
                if (_interactionAction.triggered)
                {
                    interactable.Interact();
                }
            }
        }
    }
}

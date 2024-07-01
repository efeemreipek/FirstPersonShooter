using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;
    [SerializeField] private int maxDistance;

    [Header("Reload")]
    [SerializeField] private int currentAmmo;
    [SerializeField] private int maxAmmo;
    [SerializeField] private int fireRate;
    [SerializeField] private float reloadTime;
    private bool isReloading;

    [Header("Camera Shake")]
    [SerializeField] private float shakeDuration;
    [SerializeField] private float shakeAmount;

    [Header("References")]
    [SerializeField] private Transform firePointTransform;

    // INPUT
    private PlayerInputControls _playerInputControls;
    private InputAction _shootAction;
    private InputAction _reloadAction;

    private Camera _camera;

    private float timeSinceLastShot = 0f;

    private void Awake()
    {
        currentAmmo = maxAmmo;
        isReloading = false;

        _camera = Camera.main;

        _playerInputControls = new PlayerInputControls();
    }

    private void OnEnable()
    {
        _shootAction = _playerInputControls.Player.Shoot;
        _reloadAction = _playerInputControls.Player.Reload;

        _playerInputControls.Enable();
    }

    private void OnDisable()
    {
        _playerInputControls.Disable();
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if (_shootAction.IsPressed())
        {
            Shoot();
        }
        if (_reloadAction.triggered)
        {
            StartReloading();
        }
    }

    private void Shoot()
    {
        if(currentAmmo > 0 && CanShoot())
        {
            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f));
            RaycastHit hit;

            // Hit something
            if (Physics.Raycast(ray, out hit, maxDistance)) 
            {
                Debug.Log("Shot " + hit.collider.name);
                Debug.DrawRay(_camera.transform.position, _camera.transform.forward * maxDistance, Color.red, 2f);
                currentAmmo--;
                CameraShake.Instance.Shake(shakeDuration, shakeAmount);
                var muzzleFlash = Instantiate(GameAssets.Instance.muzzleFlashPrefab, firePointTransform.position, firePointTransform.rotation, firePointTransform);
                Destroy(muzzleFlash.gameObject, 0.1f);

                timeSinceLastShot = 0f;
            }
            // Hit nothing
            else
            {
                Debug.Log("Shot nothing");
                Debug.DrawRay(_camera.transform.position, _camera.transform.forward * maxDistance, Color.red, 2f);
                currentAmmo--;
                CameraShake.Instance.Shake(shakeDuration, shakeAmount);
                var muzzleFlash = Instantiate(GameAssets.Instance.muzzleFlashPrefab, firePointTransform.position, firePointTransform.rotation, firePointTransform);
                Destroy(muzzleFlash.gameObject, 0.1f);

                timeSinceLastShot = 0f;
            }
        }
    }
    private bool CanShoot() => !isReloading && timeSinceLastShot > 60f / fireRate; // timeSinceLastShot > 1f / (fireRate / 60f)
    private void StartReloading()
    {
        if (!isReloading && currentAmmo < maxAmmo) StartCoroutine(Reload());
    }
    private IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }
}

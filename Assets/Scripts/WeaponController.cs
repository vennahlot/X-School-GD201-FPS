using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Fire")]
    [SerializeField] private float muzzlePower = 0.1f;

    [Header("Weapon Recoil")]
    [SerializeField] private float verticalRecoil = 10.0f;
    [SerializeField] private float horizontalRecoil = 2.0f;
    [SerializeField] private float recoilBack = 1f;
    [SerializeField] private float recoilReturnSpeed = 2f;

    [Header("Weapon Reload")]
    [SerializeField] private int magazineSize = 10;
    [SerializeField] private int remainingAmmo = 30;
    private int currentAmmo;
    private bool isReloading = false;

    [Header("References")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform muzzle;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private ParticleSystem muzzleFlash;
    
    // Attributes
    private Vector3 initialPosition;
    private Vector3 currentRecoilPosition;
    private AudioSource audioSource;
    private Animator animator;

    // Events
    public static event Action<int> OnMagAmmoChanged;
    public static event Action<int> OnReloadFinished;

    // Input Actions
    InputAction attackAction;
    InputAction reloadAction;

    void Awake()
    {
        attackAction = InputSystem.actions.FindAction("Attack");
        attackAction.performed += context => Fire();

        reloadAction = InputSystem.actions.FindAction("Reload");
        reloadAction.started += context => Reload();

        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        initialPosition = transform.localPosition;
        currentRecoilPosition = initialPosition;
        currentAmmo = magazineSize;
    }

    void Update()
    {
        CalculateRecoil();
        animator.SetBool("Reloading", isReloading);
    }

    void Fire()
    {
        // Prevent firing while reloading.
        if (isReloading)
        {
            return;
        }
        // Auto reload
        if (currentAmmo <= 0)
        {
            Reload();
            return;
        }
        // Instantiate bullet.
        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, transform.rotation);
        // Apply a force.
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * muzzlePower, ForceMode.Impulse);
        // Apply recoil.
        cameraController.ApplyRecoil(verticalRecoil, horizontalRecoil);
        currentRecoilPosition = initialPosition + new Vector3(0, 0, -recoilBack);
        // Play the muzzle flash.
        muzzleFlash.Play();
        // Play the fire sound.
        audioSource.Play();
        // Ammo change.
        currentAmmo -= 1;
        OnMagAmmoChanged?.Invoke(currentAmmo);
        print("Fired: " + currentAmmo + " remaining.");
    }

    void Reload()
    {
        // Prevent reloading while already reloading or no ammo.
        if (remainingAmmo <= 0 || isReloading)
        {
            return;
        }
        isReloading = true;
    }

    public void FinishReload()
    {
        isReloading = false;
        int ammoToReload = Math.Min(magazineSize - currentAmmo, remainingAmmo);
        currentAmmo += ammoToReload;
        remainingAmmo -= ammoToReload;
        OnMagAmmoChanged?.Invoke(currentAmmo);
        OnReloadFinished?.Invoke(remainingAmmo);
        print("Reloaded: " + remainingAmmo + " remaining.");
    }

    void CalculateRecoil()
    {
        currentRecoilPosition = Vector3.Lerp(currentRecoilPosition, initialPosition, recoilReturnSpeed * Time.deltaTime);
        transform.localPosition = currentRecoilPosition;
    }
}

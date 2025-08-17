using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float idleSwayAmount = 0.02f;  // Offset of the sway motion.
    [SerializeField] private float dashingSwayAmount = 0.05f;
    [SerializeField] private float idleSwaySpeed = 2.0f;  // Speed of the entire motion.
    [SerializeField] private float dashingSwaySpeed = 4.0f;
    private Vector3 initialPosition;

    [SerializeField] private float recoilReturnSpeed = 2.0f;  // How fast it will recover.
    private Vector3 currentRecoilRotation;
    private bool isSprinting = false;

    void Start()
    {
        initialPosition = transform.localPosition;
        currentRecoilRotation = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        IdleSway();
        CalculateRecoil();
    }

    void IdleSway()
    {
        float swayAmount = isSprinting ? dashingSwayAmount : idleSwayAmount;
        float swaySpeed = isSprinting ? dashingSwaySpeed : idleSwaySpeed;
        float swayX = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
        float swayY = Mathf.Cos(Time.time * swaySpeed) * swayAmount;
        transform.localPosition = initialPosition + new Vector3(swayX, swayY, 0);
    }

    void CalculateRecoil()
    {
        currentRecoilRotation = Vector3.Lerp(currentRecoilRotation, Vector3.zero, recoilReturnSpeed * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(currentRecoilRotation);
    }

    public void ApplyRecoil(float verticalRecoil, float horizontalRecoil)
    {
        currentRecoilRotation += new Vector3(-verticalRecoil, Random.Range(-horizontalRecoil, horizontalRecoil), 0);
    }
    
    public void SetSprinting(bool sprinting)
    {
        isSprinting = sprinting;
    }

}

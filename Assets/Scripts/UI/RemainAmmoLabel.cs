using TMPro;
using UnityEngine;

public class RemainAmmoLabel : MonoBehaviour
{
    TextMeshProUGUI text;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        WeaponController.OnReloadFinished += UpdateRemainingAmmo;
    }

    void OnDisable()
    {
        WeaponController.OnReloadFinished -= UpdateRemainingAmmo;
    }

    void UpdateRemainingAmmo(int remainingAmmo)
    {
        text.text = remainingAmmo.ToString();        
    }
}

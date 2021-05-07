using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lifeSupportHealth;
    [SerializeField] private TextMeshProUGUI fuel;
    [SerializeField] private TextMeshProUGUI ammo;

    void Update()
    {
        lifeSupportHealth.text = GameManager.instance.playerReference.m_health.ToString("F2") + "%";
        fuel.text = GameManager.instance.playerReference.m_fuel.ToString("F2") + "%";
        ammo.text = GameManager.instance.playerReference.m_ammo.ToString();
    }
}

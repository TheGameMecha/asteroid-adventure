using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [Header("Ship Movement")]
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private float rotateSpeed = 5.0f;

    [Header("Ship Stats")]
    [SerializeField] private float health = 100.0f;
    [SerializeField] private float healthDrainRate = 0.5f;
    [SerializeField] private float fuel = 100.0f;
    [SerializeField] private float fuelDrainRate = 0.5f;
    [SerializeField] private int ammo = 25;

    public float m_health
    {
        get
        {
            return health;
        }
        protected set
        {
            health = value;
        }
    }
    public float m_fuel
    {
        get
        {
            return fuel;
        }
        protected set
        {
            fuel = value;
        }
    }
    public int m_ammo
    {
        get
        {
            return ammo;
        }
        protected set
        {
            ammo = value;
        }
    }

    [Header("Attachments")]
    [SerializeField] private Transform shipShootPoint;

    Rigidbody2D rb;

    float m_horizontal;
    float m_vertical;

    InteractableObject m_nearbyInteractable = null;
    bool m_isNearInteractable = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        m_horizontal = Input.GetAxis("Horizontal");
        m_vertical = Input.GetAxis("Vertical");

        health -= healthDrainRate * Time.deltaTime; // Constantly drain Life Support, since it is being used

        if (health <= 0)
        {
            Debug.Log("NO HEALTH");
            health = 0;
        }

        if (ammo <= 0)
        {
            Debug.Log("NO AMMO");
            ammo = 0;
        }

        if (Input.GetButtonDown("Fire1") && ammo > 0)
        {
            GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject("Player Bullet");
            if (bullet != null)
            {
                bullet.transform.position = shipShootPoint.position;
                bullet.transform.rotation = shipShootPoint.rotation;
                bullet.SetActive(true);
                ammo--;
            }
        }

        if (Input.GetButtonDown("Interact") && m_isNearInteractable)
        {
            Debug.Log("Interacted");
            m_nearbyInteractable.WasInteractedWith();
        }
    }

    private void FixedUpdate()
    {
        Vector2 m_Input = transform.up * m_vertical;

        if (fuel <= 0)
        {
            Debug.Log("NO FUEL");
            fuel = 0;
            m_Input = Vector2.zero; // Prevent movement if out of fuel, but can still rotate and fire
        }

        // Might need to move this to Update()
        if (m_Input.magnitude > 0)
        {
            fuel -= m_Input.magnitude * fuelDrainRate * Time.fixedDeltaTime;
        }

        rb.MovePosition(rb.position + m_Input * movementSpeed * Time.fixedDeltaTime);
        rb.transform.Rotate(0.0f, 0.0f, -m_horizontal * rotateSpeed);
    }

    public void SetNearbyInteractable(bool state, InteractableObject obj)
    {
        if (state)
        {
            m_nearbyInteractable = obj;
        }
        m_isNearInteractable = state;
    }
}

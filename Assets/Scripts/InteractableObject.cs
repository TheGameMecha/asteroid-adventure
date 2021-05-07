using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InteractableObject : MonoBehaviour
{
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<ShipMovement>().SetNearbyInteractable(true, this);
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<ShipMovement>().SetNearbyInteractable(false, null);
        }
    }

    public virtual void WasInteractedWith()
    {
        Debug.Log(gameObject.name + " was Interacted with");
    }
}

public enum InteractableType
{
    POI,
    AMMO,
    FUEL
}

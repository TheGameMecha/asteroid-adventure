using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue_System;

public class PointOfInterest : InteractableObject
{
    [SerializeField] private DialogueData poi_dialogue;

    public override void WasInteractedWith()
    {
        base.WasInteractedWith();
        GameManager.instance.poiPanel.SetActive(true);
        DialogueSystem.instance.StartDialogue(poi_dialogue.dialogue);
    }
}

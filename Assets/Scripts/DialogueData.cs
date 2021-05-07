using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dialogue_System
{
    [CreateAssetMenu(menuName = "Dialogue System/Dialogue")]
    public class DialogueData : ScriptableObject
    {
        public Dialogue dialogue;
    }
}
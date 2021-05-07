using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Dialogue_System
{
    public class DialogueSystem : MonoBehaviour
    {
        #region Singleton
        public static DialogueSystem instance;

        void Awake()
        {
            if (instance != null)
                Destroy(gameObject);

            instance = this;
            DontDestroyOnLoad(this);
        }
        #endregion

        public GameObject dialoguePanel;
        public GameObject continueButton;
        public TextMeshProUGUI speakerName;
        public TextMeshProUGUI dialogueText;
        private Queue<string> sentences;

        bool clearNextLine = false;
        [SerializeField] private float typeSpeedDelay = 0.1f;

        void Start()
        {
            if (!speakerName)
                Debug.Log("No Speaker name is setup. Ignoring...");

            sentences = new Queue<string>();
            if (speakerName != null)
                speakerName.text = "";
            dialogueText.text = "";
            dialoguePanel.SetActive(false);

        }

        public void StartDialogue(Dialogue dialogue)
        {
            if (speakerName != null)
                speakerName.text = dialogue.speakerName;
            dialogueText.text = "";
            dialoguePanel.SetActive(true);
            continueButton.SetActive(false);
            sentences.Clear();
            foreach (string sentence in dialogue.sentences)
            {
                string localizedText = sentence;
                sentences.Enqueue(localizedText);
            }

            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            continueButton.SetActive(false);
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            string sentence = sentences.Dequeue();
            StartCoroutine(TypeText(sentence));
        }

        public void EndDialogue()
        {
            dialoguePanel.SetActive(false);
        }

        IEnumerator TypeText(string sentence)
        {

            // Check if we want to use [CLS]
            if (clearNextLine)
            {
                dialogueText.text = "";
                clearNextLine = false;
            }

            if (CheckTextForKey(sentence, DialogueKeys.cls))
            {
                clearNextLine = true;
                sentence = RemoveKeyFromString(sentence, DialogueKeys.cls);
            }

            // Actual Typing is done here
            foreach (char letter in sentence.ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typeSpeedDelay);
            }

            yield return new WaitForEndOfFrame();
            continueButton.SetActive(true);
        }

        bool CheckTextForKey(string text, string key)
        {
            if (text.Contains(key))
            {
                return true;
            }

            return false;
        }

        string RemoveKeyFromString(string text, string key)
        {
            string output = text.Remove(text.IndexOf(key));
            return output;
        }
    }

    [System.Serializable]
    public class Dialogue
    {
        public string speakerName;
        [TextArea] public List<string> sentences;
    }

    /// <summary>
    /// A collection of the unique keys for the dialogue system
    /// The system parses these "keys" and changes what appears in the dialogue box
    /// These keys will not be visible in the dialogue box
    /// </summary>
    public static class DialogueKeys
    {
        public static string cls = "[CLS]"; // "Clear Screen" - Tells the dialogue screen to clear the text field after hitting continue
    }
}
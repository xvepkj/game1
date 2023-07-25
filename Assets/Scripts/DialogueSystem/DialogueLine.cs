using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueLine : DialogueBaseClass
    {
        private Text textHolder;

        [Header("Text Options")]
        [SerializeField] private string input;
        [SerializeField] private Color textColor;
        [SerializeField] private Font textFont;

        [Header("Time Params")]
        [SerializeField] private float delay;
        [SerializeField] private float delayBetweenLines;

        [Header("Sound Params")]
        [SerializeField] private AudioClip textSound;

        [Header("Character Image")]
        [SerializeField] private Sprite characterSprite;
        [SerializeField] private Image imageHolder;

        private IEnumerator lineAppear;

        private void Awake()
        {
            textHolder = GetComponent<Text>();
            textHolder.text = "";

            imageHolder.sprite = characterSprite;
            imageHolder.preserveAspect = true;
        }

        private void Start()
        {
            lineAppear = WriteText(input, textHolder, textColor, textFont, delay, textSound, delayBetweenLines);
            StartCoroutine(lineAppear);
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                if (textHolder.text != input)
                {
                    StopCoroutine(lineAppear);
                    textHolder.text = input;
                }
                else finished = true;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueBaseClass : MonoBehaviour
    {
        public bool finished { get; protected set; }

        private bool Skip = false;
        protected IEnumerator WriteText(string input, Text textholder, Color textColor, Font textFont, float delay, AudioClip sound, float delayBetweenLines)
        {
            textholder.color = textColor;
            textholder.font = textFont;
            for (int i = 0; i < input.Length; i++)
            {
                textholder.text += input[i];
                SoundManager.instance.PlaySound(sound);
                yield return new WaitForSeconds(delay);
            }

            //yield return new WaitForSeconds(delayBetweenLines);
            yield return new WaitUntil(() => Input.GetMouseButton(0));

            finished = true;
        }
    }
}
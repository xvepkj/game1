using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueHolder : MonoBehaviour
    {

        [SerializeField] private GameObject player;

        public bool dialoguesOver = false;

        public IEnumerator dialogueSequence()
        {
            gameObject.SetActive(true);
            player.GetComponent<Animator>().SetBool("run", false);
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<PlayerAttack>().enabled = false;

            for (int i = 0; i < transform.childCount; i++)
            {
                Deactivate();
                transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<DialogueLine>().finished);
            }
            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<PlayerAttack>().enabled = true;
            gameObject.SetActive(false);
            dialoguesOver = true;
        }

        private void Deactivate()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
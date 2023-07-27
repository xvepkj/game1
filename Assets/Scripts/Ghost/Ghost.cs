using DialogueSystem;
using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private DialogueHolder holder;
    [SerializeField] private Animator anim;
    [SerializeField] public string ghostPref;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void playDialogueAndDisappear()
    {
        gameObject.SetActive(true);
        StartCoroutine(holder.dialogueSequence());
        StartCoroutine(Disappear());
    }

    private IEnumerator Disappear()
    {
        yield return new WaitUntil(() => holder.dialoguesOver);
        anim.SetTrigger("ghostDisappear");
        PlayerPrefs.SetInt(ghostPref, 1);
    }

    public void GhostDeactivate()
    {
        gameObject.SetActive (false);
    }
}

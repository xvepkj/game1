using DialogueSystem;
using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private DialogueHolder holder;
    [SerializeField] private Animator anim;
    [SerializeField] private string ghostPref;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        Debug.Log(holder);
        if(PlayerPrefs.GetInt(ghostPref, 0) == 0)
        {
            Debug.Log("Ghost Pref not set");
            gameObject.SetActive(true);
            StartCoroutine(holder.dialogueSequence());
            StartCoroutine(Disappear());
        }
        else gameObject.SetActive(false);
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

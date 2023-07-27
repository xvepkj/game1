using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class EndScreenUI : MonoBehaviour
{
    [SerializeField] private GameObject ghost;
    [SerializeField] private GameObject boss;
    [SerializeField] private float endScreenDelay;
    [SerializeField] private GameObject endScreen;

    private bool endScreenShown = false;

    private void Update()
    {
        if (!endScreenShown && ghost.activeInHierarchy == false && boss.activeInHierarchy == false)
        {
            endScreenShown = true;
            StartCoroutine(ShowEndScreen());
        }
    }

    public IEnumerator ShowEndScreen()
    {
        yield return new WaitForSeconds(endScreenDelay);
        endScreen.SetActive(true);
    }
}

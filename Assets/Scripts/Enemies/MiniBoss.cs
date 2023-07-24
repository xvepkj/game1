using DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniBoss : MonoBehaviour
{

    [SerializeField] private Transform spellPoint;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float spellDelay;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private GameObject[] spells;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private float damage;
    [SerializeField] private BoxCollider2D boxCollider;


    [Header("Dialog Parameters")]
    [SerializeField] private string dialoguePref;
    [SerializeField] private DialogueHolder holder;

    private float cooldownTimer = Mathf.Infinity;


    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

        cooldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if (PlayerPrefs.GetInt(dialoguePref, 0) == 0)
            {
                gameObject.GetComponent<MeleeEnemy>().enabled = false;
                PlayerPrefs.SetInt(dialoguePref, 1);
                StartCoroutine(holder.dialogueSequence());         
            }
            else if ((holder.dialoguesOver || PlayerPrefs.GetInt(dialoguePref) == 2) && cooldownTimer >= attackCooldown)
            {
                PlayerPrefs.SetInt(dialoguePref, 2);
                if (!gameObject.GetComponent<MeleeEnemy>().enabled) gameObject.GetComponent<MeleeEnemy>().enabled = true;
                cooldownTimer = 0;
                StartCoroutine(spellAttack());
            }
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left
            , 0, playerMask);

        return hit.collider != null;
    }

    private IEnumerator spellAttack()
    {
        Transform currentSpellPos = spellPoint;
        cooldownTimer = 0;
        yield return new WaitForSeconds(1f);
        anim.SetTrigger("rangedAttack");
        spells[FindSpell()].transform.position = spellPoint.position;
        spells[FindSpell()].GetComponent<Spell>().CastSpell();

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(spellPoint.position, new Vector3(2, 3, 0));
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private int FindSpell()
    {
        for (int i = 0; i < spells.Length; i++)
        {
            if (!spells[i].activeInHierarchy) return i;
        }
        return 0;
    }

    public void LoadNextLevel()
    {
        NextLevelLoader.instance.GoToNextLevel();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private LayerMask groundLayer;
    [SerializeField] private LayerMask movingPlatformLayer;
    [SerializeField]private float speed; 
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private Animator anim;
    private float horizontalInput;

    [Header("Sound")]
    [SerializeField] private AudioClip jumpSound;

    private void Awake() {
        // grab the references from your rigid body
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update() {

        float horizontalInput = Input.GetAxis("Horizontal");

        // Left-Right movement
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Flip Player left-right
        if(horizontalInput > 0.01f)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * 1, transform.localScale.y, transform.localScale.z);
        else if(horizontalInput < -0.01f)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * - 1, transform.localScale.y, transform.localScale.z);

        //Jump 
        if(Input.GetKey(KeyCode.Space) && isGrounded()) {
            Jump();
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SoundManager.instance.PlaySound(jumpSound);
            }
        }


        // Set animator parameters 
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

    }

    private void Jump() {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("jump");
    }

    private bool isGrounded() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size,0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack() {
        return horizontalInput == 0 && isGrounded();
     }
}

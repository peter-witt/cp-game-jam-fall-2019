using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int walkSpeed = 10;
    public int jumpSpeed = 100;

    bool spriteFlipped;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    bool IsGrounded()
    {
        var hit = Physics2D.Raycast(transform.position + (Vector3.down * (GetComponent<Collider2D>().bounds.extents.y)), Vector2.down, 0.01f, LayerMask.GetMask("JumpAble"));
        return hit;
    }

    void Update()
    {
        if (transform.position.y < -50)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        bool walking = false;
        Vector2 direction = Vector2.zero;

        if (Input.GetKey(KeyCode.A))
        {
            walking = true;
            spriteFlipped = true;
            direction.x -= walkSpeed;
            
        }

        if (Input.GetKey(KeyCode.D))
        {
            walking = true;
            spriteFlipped = false;
            direction.x += walkSpeed;
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && IsGrounded())
        {
            direction.y += jumpSpeed;
        }

        GetComponent<Animator>().SetBool("walking", walking);
        GetComponent<Animator>().SetBool("jumping", !IsGrounded());
        GetComponent<SpriteRenderer>().flipX = spriteFlipped;

        rb.AddForce(direction * Time.deltaTime);
    }
}

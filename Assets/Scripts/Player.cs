using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int walkSpeed = 10;
    public int jumpSpeed = 100;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    bool IsGrounded()
    {
        // Use a raycast that begins at the bottom of the player, and is 0.01 long.
        // If the raycast hits anything that is on the layer "Jumpable" the player is on the ground.
        var hit = Physics2D.Raycast(
            transform.position + (Vector3.down * (GetComponent<Collider2D>().bounds.extents.y)),
            Vector2.down,
            0.01f,
            LayerMask.GetMask("JumpAble"));

        return hit;
    }

    void Update()
    {
        // Reset the scene if the player falls below the level
        if (transform.position.y < -50)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Set the direction to 0
        Vector2 direction = Vector2.zero;

        // If the player presses a, add walkingspeed to the directions X parameter
        if (Input.GetKey(KeyCode.A))
        {
            direction.x -= walkSpeed;
        }

        // If the player presses d, subtract walkingspeed to the directions X parameter
        if (Input.GetKey(KeyCode.D))
        {
            direction.x += walkSpeed;
        }

        // If the player presses space or w, add jumping speed to the directions Y parameter
        // This means that the player jumps
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && IsGrounded())
        {
            direction.y += jumpSpeed;
        }

        // Set the walking bool in the animator. If the direction.x paramter is not 0
        // That means that the player has pressed W or D buttons
        GetComponent<Animator>().SetBool("walking", direction.x != 0);
        // Set the jumping bool int he animator. If the player is not grounded, he is jumping
        GetComponent<Animator>().SetBool("jumping", !IsGrounded());
        // If the direction.x is negative, that means we are moving left, and should flip the sprite
        GetComponent<SpriteRenderer>().flipX = direction.x < 0;

        // Add force to the player in the direction the player pressed.
        rb.AddForce(direction * Time.deltaTime);
    }
}

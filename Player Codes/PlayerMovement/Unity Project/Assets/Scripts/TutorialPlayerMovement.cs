using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlayerMovement : MonoBehaviour
{
    #region Movement 2 Variables
    float horizontal;
    float vertical;
    #endregion

    #region Movement 3 Variables
    Vector2 movement;
    #endregion

    #region Movement 4 Variables
    float pHorizontal;
    bool groundCheck;
    public float groundCheckRadius = .35f;
    float jumpPover = 10f;
    public GameObject groundCheckPosition;
    public LayerMask groundCheckLayer;
    #endregion

    float speed = 15f;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        //Movement1();
        //Movement2();
        //Movement3();
        //Movement4();
    }

    private void Update()
    {
        CheckSurfaceForMovement4();
        Jump();
    }
    void Movement1()
    {
        //It's a bit of a time loss, but I'm planning to write down every method I know.
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            transform.position += new Vector3(0, 10 * Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            transform.position -= new Vector3(0, 10 * Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            transform.position -= new Vector3(10 * Time.deltaTime, 0, 0);
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            transform.position += new Vector3(10 * Time.deltaTime, 0, 0);
    }
    void Movement2()
    {
        //This time, we are creating 'horizontal' and 'vertical' variables of type float in the global scope.
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        //And we need Rigitbody2D component and speed value
        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
    }
    void Movement3()
    {
        //This is what Brackeys recommends for top-down 2D games. I use this in every game I make lol.
        //This time, we are creating a 'movement' variable of type Vector2 in the global scope.
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }
    //This codes just for Movement 4
    void Movement4()
    {
        //I might not be experienced enough to write the most optimal movement code for a 2D platformer game. But if someone came here for that, I wanted to jot down a few things.
        //We need just Horizontal Axis
        pHorizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(pHorizontal * speed, rb.velocity.y);
        //We will use a method to prevent our character from endlessly jumping by rapidly pressing the Space key.
    }
    void CheckSurfaceForMovement4()
    {
        //We will create a circular collider under our character's feet to determine whether they are on the ground or not.
        //For this, we'll need an empty game object placed where the character makes contact with the surface in the game engine. We'll use this to position the created circle collider accurately.
        groundCheck = Physics2D.OverlapCircle(groundCheckPosition.transform.position, groundCheckRadius, groundCheckLayer);

    }
    void Jump()
    {
        if (groundCheck == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPover);
            }
        }
        //For some reason that I don't understand, these methods are not working properly on the FixedUpdate.
        //That's why we will use the Update instead.
    }
    void OnDrawGizmos()
    {
        //To visualize the size or appearance of the created circle collider, we can generate gizmos.
        Gizmos.DrawWireSphere(groundCheckPosition.transform.position, groundCheckRadius);
        //If you make the "GroundCheckRadius" variable public, you can adjust the size of the collider circle that we draw.
    }
}

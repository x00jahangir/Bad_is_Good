using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movespeed;
    PlayerHealth playerHealth;
     Rigidbody2D rb;
     public Animator animator;
    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.playerOn == true)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        else
        {
            movement.x = movement.y = 0f;
        }
       
    }
    private void FixedUpdate()
    {
        //If the Game Manager says the it is switch time, exit
        if (GameManager.IsSwitchingScene())
        {
            playerHealth.col.enabled = false;
            return;
        }

        if (GameManager.IsGameOver())
            return;

        playerHealth.col.enabled = true;

        rb.velocity = new Vector2(movement.x * movespeed, movement.y * movespeed);

        if(movement != Vector2.zero)
        {
            animator.Play("CPlayerwalk");
        }
        else
            animator.Play("none");
    }

   
}

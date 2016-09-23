using UnityEngine;
using System.Collections;

public class CharacterMovementController : MonoBehaviour {

    private Rigidbody2D rigidBody2d;
    private Animator characterAnimator;

    public const int movementSpeed = 5;
    public const int jumpForce = 7;

    public int groundTriggersCount = 0;

    private bool isLeftPressed = false;
    private bool isRightPressed = false;

    private const string groundTag = "Ground";

    void Start () 
    {
        rigidBody2d = gameObject.GetComponent<Rigidbody2D>();
        rigidBody2d.freezeRotation = true;

        characterAnimator = gameObject.GetComponent<Animator>();
	}

    void FixedUpdate() 
    {
        int velocityX = 0;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            isLeftPressed = true;
            isRightPressed = false;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            isRightPressed = true;
            isLeftPressed = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            isLeftPressed = false;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            isRightPressed= false;
        }
        if (isRightPressed) 
        {
            velocityX = movementSpeed;
        }
        else if (isLeftPressed)
        {
            velocityX = -movementSpeed;
        }
        if (isGrounded)
        {
            rigidBody2d.velocity = new Vector2(velocityX, rigidBody2d.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rigidBody2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
            
        }
        updateAnimation();
        if (Input.GetKeyDown(KeyCode.LeftControl)) 
        {
            characterAnimator.SetTrigger("isAttacking");
        }
        flipCharacterByDirection();
    }

    private void flipCharacterByDirection()
    {
        if (rigidBody2d.velocity.x >= 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else 
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == groundTag) 
        {
            groundTriggersCount++;
        }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == groundTag)
        {
            groundTriggersCount--;
            Debug.Assert(groundTriggersCount >= 0,"wron");
        }
    }

    private bool isGrounded 
    {
        get 
        {
            return groundTriggersCount > 0;
        }
    } 

    private bool isWalking
    {
        get
        {
            bool res = rigidBody2d.velocity.x != 0;
            if (res) 
            {
                Debug.Log("a");
            }
            return res;
        }
    }

    private void updateAnimation()
    {
        characterAnimator.SetBool("isGrounded", isGrounded);
        characterAnimator.SetBool("isWalking", isWalking);
    }

}

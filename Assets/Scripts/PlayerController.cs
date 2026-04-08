 using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public float jumpForce = 5.0f;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private bool isGrounded;
    private int jumpsRemaining;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        count = 0;
        rb = GetComponent <Rigidbody>();
        SetCountText();
        winTextObject.SetActive(false);
    }

   void OnMove (InputValue movementValue)
   {
    Vector2 movementVector = movementValue.Get<Vector2>();
    movementX = movementVector.x;
    movementY = movementVector.y;

   }

   void OnJump(InputValue value)
   {
    if (jumpsRemaining > 0)
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        jumpsRemaining--;
    }
   }

    void SetCountText()
   {
       countText.text =  "Count: " + count.ToString();
        if (count >= 8)
       {
           winTextObject.SetActive(true);
       }
   }
   void FixedUpdate()
   {
    Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
    rb.AddForce(movement * speed);
   }

  void OnTriggerEnter(Collider other)
   {
    if (other.gameObject.CompareTag("PickUp"))
    {
       other.gameObject.SetActive(false);
       count = count + 1;
       SetCountText();
    }

   }

   void OnCollisionEnter(Collision collision)
   {
    if (collision.gameObject.CompareTag("Ground"))
    {
        isGrounded = true;
        jumpsRemaining = 2;
    }
   }

   void OnCollisionExit(Collision collision)
   {
    if (collision.gameObject.CompareTag("Ground"))
    {
        isGrounded = false;
    }
   }

}
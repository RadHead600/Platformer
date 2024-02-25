using System.Collections;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jump;
    [SerializeField] private LayerMask block_Stay;
    [SerializeField] private CharacterMovementAnimation _characterMovementAnimation;

    private Rigidbody2D rigidBody;
    private bool _isIgnorePlatform;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Grounded();
        Move();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && Grounded())
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            IgnorePlatform();
            Invoke("IgnorePlatform", 0.5f);
        }
    }
    private void IgnorePlatform()
    {
        _isIgnorePlatform = !_isIgnorePlatform;
        Physics2D.IgnoreLayerCollision(10, 18, _isIgnorePlatform);
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector3(horizontal * speed * Time.fixedDeltaTime, 0.0f);
        rigidBody.AddForce(movement);
        _characterMovementAnimation.MoveAnimation(horizontal);
    }

    public void Jump()
    {
        rigidBody.AddForce(transform.up * jump, ForceMode2D.Impulse);
    }

    public bool Grounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, .3F, block_Stay);
        return colliders.Length > 0.8;
    }

}

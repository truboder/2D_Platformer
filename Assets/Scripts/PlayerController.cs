using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    private float _movespeed;
    private float _jumpForce;
    private bool _isGrounded;
    private float _moveHorizontal;
    private float _moveVertical;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

        _movespeed = 3f;
        _jumpForce = 60f;
        _isGrounded = false;

    }

    private void Update()
    {
        _moveHorizontal = Input.GetAxisRaw("Horizontal");
        _moveVertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Vector3 rotate = transform.eulerAngles;

        if (_moveHorizontal > 0.1f)
        {
            rotate.y = 180;
            _animator.SetTrigger("Moved");
            _rigidbody2D.AddForce(new Vector2(_moveHorizontal * _movespeed, 0f), ForceMode2D.Impulse);
            _rigidbody2D.transform.rotation = Quaternion.Euler(rotate);
        }
        else if (_moveHorizontal < -0.1f)
        {
            rotate.y = 0;
            _animator.SetTrigger("Moved");
            _rigidbody2D.AddForce(new Vector2(_moveHorizontal * _movespeed, 0f), ForceMode2D.Impulse);
            _rigidbody2D.transform.rotation = Quaternion.Euler(rotate);
        }

        if (!_isGrounded && _moveVertical > 0.1f)
        {
            _rigidbody2D.AddForce(new Vector2(0f, _moveVertical * _jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            _isGrounded = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            _isGrounded = true;
        }
    }
}

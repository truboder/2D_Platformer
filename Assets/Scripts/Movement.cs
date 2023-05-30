using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public static int StringToHash;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _moveDelta;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private int _idleHash = Animator.StringToHash("Moved");

    private bool _isGrounded;
    private float _moveHorizontal;
    private float _moveVertical;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
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

        if (_moveHorizontal > _moveDelta)
        {
            rotate.y = 180;
            _animator.SetTrigger(_idleHash);
            _rigidbody2D.AddForce(new Vector2(_moveHorizontal * _moveSpeed, 0f), ForceMode2D.Impulse);
            _rigidbody2D.transform.rotation = Quaternion.Euler(rotate);
        }
        else if (_moveHorizontal < -_moveDelta)
        {
            rotate.y = 0;
            _animator.SetTrigger(_idleHash);
            _rigidbody2D.AddForce(new Vector2(_moveHorizontal * _moveSpeed, 0f), ForceMode2D.Impulse);
            _rigidbody2D.transform.rotation = Quaternion.Euler(rotate);
        }

        if (!_isGrounded && _moveVertical > _moveDelta)
        {
            _rigidbody2D.AddForce(new Vector2(0f, _moveVertical * _jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Platform>() != null)
        {
            _isGrounded = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Platform>() != null)
        {
            _isGrounded = true;
        }
    }
}

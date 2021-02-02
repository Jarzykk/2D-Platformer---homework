using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCharatcer : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private UnityEvent _isKilled;

    private Animator _animator;
    private WayPointMovement _wayPointMovement;
    private int _currentHealth;
    private bool _isAlive = true;
    private bool _isFacingRight = true;
    private float _currentPositionX;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _currentPositionX = transform.position.x;
        _animator = GetComponent<Animator>();
        _wayPointMovement = GetComponent<WayPointMovement>();
    }

    private void Update()
    {
        if (_isAlive)
            _wayPointMovement.Patrol();

        if(_currentPositionX != transform.position.x)
        {
            if(_currentPositionX > transform.position.x)
            {
                if (_isFacingRight)
                    FlipCharacter();

                _isFacingRight = false;
            }

            if(_currentPositionX < transform.position.x)
            {
                if (_isFacingRight == false)
                    FlipCharacter();

                _isFacingRight = true;
            }

            _animator.SetBool("isMoving", true);
        }
        else
        {
            _animator.SetBool("isMoving", false);
        }

        _currentPositionX = transform.position.x;
    }

    private void FlipCharacter()
    {
        transform.Rotate(0, 180, 0);
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _animator.SetTrigger("isHurt");

        if (_currentHealth <= 0)
            Death();
    }

    private void Death()
    {
        _isAlive = false;
        _animator.SetBool("isDead", true);
        GetComponent<Collider2D>().enabled = false;
        _isKilled?.Invoke();
        this.enabled = false;
    }
}

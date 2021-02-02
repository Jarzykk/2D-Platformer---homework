using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayersAnimation : MonoBehaviour
{
    private Animator _animator;
    private bool _isFasingRight = true;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Horizontal") > 0)
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                if (_isFasingRight)
                    FlipCharatcer();

                _isFasingRight = false;
            }
            if (Input.GetAxis("Horizontal") > 0)
            {
                if (_isFasingRight == false)
                    FlipCharatcer();

                _isFasingRight = true;
            }

            _animator.SetBool("isRunning", true);
        }
        else
        {
            _animator.SetBool("isRunning", false);
        }
    }

    private void FlipCharatcer()
    {
        transform.Rotate(0, 180, 0);
    }
}

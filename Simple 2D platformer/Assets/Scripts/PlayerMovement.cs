using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float GravityModifier = 1f;
    public float MinGroundNormalY = 0.65f;
    public float MovementSpeed = 1;
    public Vector2 Velocity;
    public LayerMask LayerMask;

    private Vector2 _targetVelocity;
    private bool _grounded;
    private Vector2 _groundNormal;
    private Rigidbody2D _rigidBody;
    private ContactFilter2D _contactFilter;
    private RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];
    private List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(16);

    private const float _minMoveDistance = 0.001f;
    private const float _shellRadius = 0.01f;

    private Vector2 _deltaPosition;
    private Vector2 _moveAlongGround;
    private Vector2 _move;

    private void OnEnable()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(LayerMask);
        _contactFilter.useLayerMask = true;
    }

    private void Update()
    {
        _targetVelocity = new Vector2(Input.GetAxis("Horizontal"), 0);

        if (Input.GetKey(KeyCode.Space) && _grounded)
            Velocity.y = 5;
    }

    private void FixedUpdate()
    {
        Velocity += GravityModifier * Physics2D.gravity * Time.deltaTime;
        Velocity.x = _targetVelocity.x;

        _grounded = false;

        _deltaPosition = Velocity * Time.deltaTime;
        _moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
        _move = _deltaPosition.x * _moveAlongGround * MovementSpeed;

        Movement(_move, false);

        _move = Vector2.up * _deltaPosition;

        Movement(_move, true);
    }

    private void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if(distance > _minMoveDistance)
        {
            int count = _rigidBody.Cast(move, _contactFilter, _hitBuffer, distance + _shellRadius);

            _hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                _hitBufferList.Add(_hitBuffer[i]);
            }

            for (int i = 0; i < _hitBufferList.Count; i++)
            {
                Vector2 currentNormal = _hitBufferList[i].normal;

                if(currentNormal.y > MinGroundNormalY)
                {
                    _grounded = true;
                    if(yMovement)
                    {
                        _groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(Velocity, currentNormal);
                if (projection < 0)
                    Velocity = Velocity - projection * currentNormal;

                float modifiedDistance = _hitBufferList[i].distance - _shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        _rigidBody.position = _rigidBody.position + move.normalized * distance;
    }
}

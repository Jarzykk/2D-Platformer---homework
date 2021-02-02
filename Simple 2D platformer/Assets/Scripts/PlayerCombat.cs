using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private int _atackDamage = 50;
    [SerializeField] private float _atackRateInSeconds = 2f;
    [SerializeField] private Transform _atackPoint;
    [SerializeField] private float _atackRange = 0.75f;
    [SerializeField] private LayerMask _enemyLayer;

    private Animator _animator;
    private Collider2D[] _enemiesHit;
    private float _AtackTimeCount;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _AtackTimeCount = _atackRateInSeconds;
    }

    void Update()
    {
        if(_AtackTimeCount < _atackRateInSeconds)
        {
            _AtackTimeCount += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.F) && _AtackTimeCount >= _atackRateInSeconds)
        {
            _AtackTimeCount = 0;
            Atack();
        }
    }

    private void Atack()
    {
        _animator.SetTrigger("Atack");
        _enemiesHit = Physics2D.OverlapCircleAll(_atackPoint.position, _atackRange, _enemyLayer);

        foreach (var enemy in _enemiesHit)
        {
            enemy.GetComponent<EnemyCharatcer>().TakeDamage(_atackDamage);
        }
    }
}

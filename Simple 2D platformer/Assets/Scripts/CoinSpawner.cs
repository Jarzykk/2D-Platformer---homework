using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private int _coinsAmountToSpawn;
    [SerializeField] private GameObject _coin;

    private Vector3 _coinsPosition;

    private void Start()
    {
        _coinsPosition = transform.position;
    }

    public void SpawnCoins(int amountToSpawn)
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            Instantiate(_coin, _coinsPosition, Quaternion.identity);
            _coinsPosition.x += 0.5f;
        }
    }
}

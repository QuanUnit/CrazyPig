using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private AreaDamager _damager;
    [SerializeField] private float _chargingTime;
    [SerializeField] private GameObject _explosionEffect;

    private void Start()
    {
        StartCoroutine(Tool.Wait(_chargingTime, Explode));
    }

    private void Explode()
    {
        _damager.DamageSubjects();
        Instantiate(_explosionEffect, transform.position, Random.rotation);
        Destroy(gameObject);
    }
}


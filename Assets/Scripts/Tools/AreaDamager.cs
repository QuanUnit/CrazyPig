using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Trigger))]
public class AreaDamager : MonoBehaviour
{

    protected List<IDamageReceived> _damageReseiveds = new List<IDamageReceived>();

    [SerializeField] private Trigger _trigger;
    [SerializeField] private int _damage;
    [SerializeField] private List<int> _layers;


    private void OnEnable()
    {
        _trigger.OnTriggerExit += OnTriggerExitHandler;
        _trigger.OnTriggerEnter += OnTriggerEnterHandler;
    }

    private void OnDisable()
    {
        _trigger.OnTriggerExit -= OnTriggerExitHandler;
        _trigger.OnTriggerEnter -= OnTriggerEnterHandler;
    }

    private void OnTriggerEnterHandler(Collider2D other, Trigger trigger)
    {
        Rigidbody2D attachedRB = other.attachedRigidbody;
        if (attachedRB == null) return;

        GameObject go = attachedRB.gameObject;

        if (go.TryGetComponent<IDamageReceived>(out var subject) && _damageReseiveds.Contains(subject) == false
            && _layers.Contains(go.layer) == true)
        {
            _damageReseiveds.Add(subject);
        }
    }

    private void OnTriggerExitHandler(Collider2D other, Trigger trigger)
    {
        Rigidbody2D attachedRB = other.attachedRigidbody;
        if (attachedRB == null) return;

        GameObject go = attachedRB.gameObject;

        if (go.TryGetComponent<IDamageReceived>(out var subject))
            _damageReseiveds.Remove(subject);
    }

    public void DamageSubjects()
    {
        foreach (var subject in _damageReseiveds.ToArray())
        {
            subject.TakeDamage(_damage);
        }
    }
}

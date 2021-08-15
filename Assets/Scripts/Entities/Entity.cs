using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityBehaviour))]
public abstract class Entity : MonoBehaviour, IDamageReceived
{
    public Animator Animator => _animator;
    public GridWarden Warden => _warden;
    public float MovementSpeed => _movementSpeed;
    public float RunMultiplie => _runMultiplie;
    public int MaxHealthPoints => _maxHealthPoints;

    public event Action<Entity> OnDeath;
    public event Action<int> OnHPChanged;
    

    [SerializeField][Range(0, 1)] private float _movementSpeed;
    [SerializeField] [Range(0, 2)] private float _runMultiplie;

    [SerializeField] private int _maxHealthPoints;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _deathEffect;

    private int _healthPoints;
    private EntityBehaviour _behaviour;
    private GridWarden _warden = new GridWarden();

    private void Awake()
    {
        _behaviour = GetComponent<EntityBehaviour>();
        _healthPoints = _maxHealthPoints;
    }

    private void Start()
    {
        _behaviour.Initialize(this);
    }

    public void TakeDamage(int value)
    {
        _healthPoints -= value;
        _animator.SetTrigger("Impact");


        if (_healthPoints <= 0) Die();
        else OnHPChanged?.Invoke(_healthPoints);
    }

    public IEnumerator Move(Cell[] path, float speed, Action callBack = null)
    {
        foreach (var cell in path)
        {
            yield return Move(cell, speed);
            _warden.ChangeCell(cell);
        }
        callBack?.Invoke();
    }

    public IEnumerator Move(Cell target, float speed, Action callBack = null)
    {
        if (speed <= 0)
            throw new Exception("Speed is zero");

        float t = 0;
        Vector2 startPosition = transform.position;
        Vector2 dir = (target.Position - startPosition).normalized;

        _animator.SetFloat("X", dir.x);
        _animator.SetFloat("Y", dir.y);

        while ((Vector2)transform.position != target.Position)
        {
            t += speed;
            transform.position = Vector2.Lerp(startPosition, target.Position, t);
            yield return new WaitForFixedUpdate();
        }
        _warden.ChangeCell(target);
        callBack?.Invoke();
    }

    private void Die()
    {
        OnDeath?.Invoke(this);
        Instantiate(_deathEffect, transform.position, UnityEngine.Random.rotation);
        Destroy(gameObject);
    }
}

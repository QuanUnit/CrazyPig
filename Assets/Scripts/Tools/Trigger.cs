using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Collider2D))]
public class Trigger : MonoBehaviour
{
    public event Action<Collider2D, Trigger> OnTriggerEnter;
    public event Action<Collider2D, Trigger> OnTriggerExit;
    public event Action<Collider2D, Trigger> OnTriggerStay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerEnter?.Invoke(collision, this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnTriggerExit?.Invoke(collision, this);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        OnTriggerStay?.Invoke(collision, this);
    }
}

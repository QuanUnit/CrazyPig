using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class PhoneButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public event Action OnDown;
    public event Action OnUp;
    public event Action OnClamped;

    private bool _isClamped;

    private void Update()
    {
        if(_isClamped)
            OnClamped?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isClamped = false;
        OnUp?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isClamped = true;
        OnDown?.Invoke();
    }
}

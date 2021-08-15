using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [SerializeField] private Entity _target;
    [SerializeField] private Slider _slider;

    private void OnEnable() => _target.OnHPChanged += SetValue;
    private void OnDisable() => _target.OnHPChanged -= SetValue;

    public void SetValue(int value)
    {
        _slider.value = (float)value / _target.MaxHealthPoints;
    }
}

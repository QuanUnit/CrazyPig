using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public event System.Action<Vector2> OnMovementButtonDrag;
    public event System.Action OnBombButtonClick;

    [SerializeField] private PseudoSerializableDictionary<PhoneButton, Vector2> _movementButtons;
    [SerializeField] private PhoneButton _bombButton;

    private void Awake()
    {
        foreach (var pair in _movementButtons.Dictionary)
        {
            pair.Key.OnClamped += delegate { OnMovementButtonDrag?.Invoke(pair.Value.normalized); };
        }

        _bombButton.OnDown += delegate { OnBombButtonClick?.Invoke(); } ;

    }
}

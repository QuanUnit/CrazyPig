using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicAreaDamager : AreaDamager
{
    [SerializeField] private float _deltaTime;

    private void Start()
    {
        StartCoroutine(DealDamage());
    }

    private IEnumerator DealDamage()
    {
        while(true)
        {
            DamageSubjects();
            yield return new WaitForSeconds(_deltaTime);
        }
    }
}

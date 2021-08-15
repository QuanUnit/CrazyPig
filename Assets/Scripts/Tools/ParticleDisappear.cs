using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleDisappear : MonoBehaviour
{
    [SerializeField] float timeAfterEndDuration;

    void Start()
    {
        var delay = GetComponent<ParticleSystem>().main.duration;
        StartCoroutine(Tool.Wait(delay + timeAfterEndDuration, delegate { Destroy(gameObject); }));
    }
}

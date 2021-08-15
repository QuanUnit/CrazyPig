using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PseudoSerializableDictionary<TKey, TValue>
{
    public Dictionary<TKey, TValue> Dictionary
    {
        get
        {
            Dictionary<TKey, TValue> output = new Dictionary<TKey, TValue>();

            for (int i = 0; i < pairs.Count; i++)
            {
                output.Add(pairs[i].Key, pairs[i].Value);
            }
            return output;
        }
    }
    public int Count { get => pairs.Count; }

    [SerializeField] private List<Pair<TKey, TValue>> pairs;
}

[System.Serializable]
public struct Pair<TKey, TValue>
{
    public TKey Key;

    public TValue Value;
}

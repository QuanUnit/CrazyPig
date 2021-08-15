using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tool
{
    public static readonly float DeviationAgnleFromVectorUp = -7;

    private static Vector2 _worldUpVector = Vector2.zero;

    public static Vector2 WorldUpVector
    {
        get
        {
            if (_worldUpVector == Vector2.zero)
            {
                float angleInRadian = DeviationAgnleFromVectorUp * Mathf.Deg2Rad;

                float x = Vector2.up.x * Mathf.Cos(angleInRadian) - Vector2.up.y * Mathf.Sin(angleInRadian);
                float y = Vector2.up.x * Mathf.Sin(angleInRadian) + Vector2.up.y * Mathf.Cos(angleInRadian);

                _worldUpVector = new Vector2(x, y);
            }
            return _worldUpVector.normalized;
        }
    }
    public static TKey FindValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TValue value)
    {
        foreach (var item in dictionary)
        {
            if ((object)item.Value == (object)value)
            {
                return item.Key;
            }
        }
        return default;
    }

    public static IEnumerator Wait(float time, System.Action callBack)
    {
        yield return new WaitForSeconds(time);
        callBack?.Invoke();
    }
}

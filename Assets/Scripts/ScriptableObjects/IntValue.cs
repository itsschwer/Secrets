﻿using UnityEngine;

[CreateAssetMenu]
[System.Serializable]   // Is this attribute still necessary?
public class IntValue : ScriptableObject  // Should implement ISerializationCallbackReceiver and make `value = defaultValue;`?
{
    // [SerializeField] private float defaultValue = default;
    public int value;
}
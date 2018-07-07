using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "Settings")]
public class SettingsPresets : ScriptableObject
{
    public GameObject playerBulletPrefab;
    public float playerShootCooldown = 0.1f;
    public float playerCollisionRadius = 0.15f;
    public Vector2 playerCollisionOffset = new Vector2(0, -0.15f);

}

using System;
using UnityEngine;

public class EnemyTargetBehavior : MonoBehaviour
{
    public Transform Target { get; private set; }

    public Action OnTargetChanged;

    private void Awake()
    {
        Target = FindObjectOfType<PlayerController>().transform;
    }
}

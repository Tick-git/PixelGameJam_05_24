﻿using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player/PlayerStats")]
internal class PlayerStats : ScriptableObject
{
    public float Speed;
    public float Health;
    public float AttackCooldown;

    public float WaterExchangeCooldown;
}
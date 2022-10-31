using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Dawn of Alina/Enemy Data")]

public class EnemyData : ScriptableObject
{
    public string enemyName;
    public string description;
    //public GameObject enemyModel;
    public float health = 100;
    public float walkSpeed = 2f;
    public float runSpeed = 5f;
    public float detectRange = 10f;
    public float attackRange = 2f;
    public float walkPointRange = 10f;
    public float timeBetweenAttacks = 1f;
    //public int damage = 1;
}
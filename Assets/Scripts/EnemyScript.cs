using UnityEngine;

public abstract class EnemyScript : MonoBehaviour {

    public int DamageTreshold = 1;
    public int HitPoints = 1;

    public abstract void TakeDamage();
    public abstract void Die();
}
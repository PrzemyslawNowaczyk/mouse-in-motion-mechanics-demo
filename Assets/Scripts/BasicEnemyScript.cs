using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyScript : EnemyScript {

    private Material _material;
    private Animator _animator;
    public Color MaterialTint;

    private void Awake() {
        Renderer _renderer = GetComponent<SpriteRenderer>();
        _material = _renderer.material;
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other) {
        var character = other.GetComponent<CharacterScript>();
        if (character) {
            if(character.CurrentSpeedLevelNumber >= DamageTreshold) {
                TakeDamage();
            }
            else {
                Debug.Log("Attack Failed! >Not yet implemented<");
            }
        }
    }

    public override void TakeDamage() {
        HitPoints--;
        ManagersToolbox.CameraManager.ShakeCamera(1.0f, 3.0f);
        _animator.SetTrigger("hit_trigger");
        if(HitPoints <= 0) {
            Die();
        }
    }

    public override void Die() {
        Destroy(gameObject);
    }

    private void Update() {
        _material.color = MaterialTint;
    }

}

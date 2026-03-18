using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Scripts.PlayerScripts;

public class Trap : MonoBehaviour
{
    [SerializeField] private float damagePercent = 0.2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement playerMovement = collision.GetComponentInParent<PlayerMovement>();
        PlayerHealth playerHealth = collision.GetComponentInParent<PlayerHealth>();

        if (playerMovement != null && playerHealth != null)
        {
            playerMovement.RespawnAtLastSafePosition();
            playerHealth.TakePercentageDamage(damagePercent);
        }
    }
}
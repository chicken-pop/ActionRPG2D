using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private int damageAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<CharacterStats>().TakeDamage(damageAmount);
    }
}

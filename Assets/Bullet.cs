using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Collider2D player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponents<Player>().Length!=0 || other.gameObject.GetComponents<DeathI>().Length!=0)
        {
            return;
        }
        if (other.gameObject.IsDestroyed())
        {
            return;
        }
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
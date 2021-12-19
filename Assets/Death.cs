using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour, DeathI
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponents<Player>().Length != 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            if (other.gameObject.IsDestroyed())
            {
                return;
            }

            Debug.Log("Smert");
            Destroy(other.gameObject);
        }
    }
}
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace 
{
    public class CheckWinOrNot: MonoBehaviour
    {
        public Text binText;
        private float secondsToRestart = 5;

        private void Update()
        {
            Collider2D[] collidersArray = Physics2D
                .OverlapCircleAll(transform.position, 99999);
            if (collidersArray.Length == 5)
            {
                binText.text = "You win restart in " + secondsToRestart + " seconds";
                secondsToRestart -= Time.deltaTime;
                if (secondsToRestart < 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
    }
}
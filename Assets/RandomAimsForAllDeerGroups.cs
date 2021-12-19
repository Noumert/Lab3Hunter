using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class RandomAimsForAllDeerGroups : MonoBehaviour
    {
        public static Vector3[] moveAims;
        
        
        private void Start()
        {
            InvokeRepeating("randomAim", 0, 2);
        }

        void randomAim()
        {
            Vector3[] vector3s = new Vector3[Spawn.deerGroupsAmountG];
            for (int i = 0; i < vector3s.Length; i++)
            {
                int x = Random.Range(-48, 48);
                int y = Random.Range(-28, 28);
                vector3s[i] = new Vector3(x, y);
            }

            moveAims = vector3s;
        }
    }
}
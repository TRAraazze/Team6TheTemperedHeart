using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

namespace CGP
{
    public class SpawnManager : MonoBehaviour
    {
        public static Vector3[] spawnCoords = new Vector3[15];
        
        private void Awake()
        {
            int i = 0;
            foreach (Transform child in transform)
            {
                // child is the child transform
                spawnCoords[i] = child.position;

                i++;
            }

        }
        
    }
}

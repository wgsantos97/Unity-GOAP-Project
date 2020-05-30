using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP_Demo
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] GameObject patientPrefab = null;
        [SerializeField] int population = 0;

        // Start is called before the first frame update
        void Start()
        {
            for(int i=0; i<population; i++)
            {
                Invoke("SpawnPatient", Random.Range(2, 20));
            }
        }

        // Called by Invoke
        void SpawnPatient()
        {
            var child = Instantiate(patientPrefab, transform.position, Quaternion.identity);
            child.transform.parent = transform;
        }
    }
}

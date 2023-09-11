using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] private GameObject _defaultSpawn;
    private void Awake()
    {
        
    }

    private Transform GetSpawnPosition()
    {
            return true?  transform : _defaultSpawn.transform;
    }
}

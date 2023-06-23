using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    [SerializeField]
    private LevelManager _level;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _level.EndLevel();
            Destroy(gameObject);
        }
    }
}

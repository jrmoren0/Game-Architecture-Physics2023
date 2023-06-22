using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorKey : MonoBehaviour
{

    public UnityEvent _onKeyPicked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _onKeyPicked?.Invoke();
            Destroy(gameObject);
        }
    }
}

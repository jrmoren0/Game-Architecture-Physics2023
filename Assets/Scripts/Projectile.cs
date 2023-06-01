using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        IDestroyable bob = collision.gameObject.GetComponent<IDestroyable>();

        if(bob != null)
        {
            bob.OnCollided();
        }


        Destroy(gameObject);
    }
}

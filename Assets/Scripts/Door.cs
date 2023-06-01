using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _doorRenderer;

    [SerializeField]
    private Material defualtDoorColor, detectedDoorColor;

    [SerializeField]
    private Material defualtLockColor, detectedzLockColor;


    [SerializeField]
    private MeshRenderer _LockRenderer;

    [SerializeField]
    private Animator doorAnimator;


    private float timer = 0;


    private float waitTime = 1.0f;


    public bool _isLocked;




    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player") {

            timer = 0;
            _doorRenderer.material = detectedDoorColor;


        }

    }



    private void OnTriggerStay(Collider other)
    {

        if (!_isLocked)
        {
            timer += Time.deltaTime;

            if (other.tag == "Player")
            {
                if (timer >= waitTime)
                {
                    timer = waitTime;
                    doorAnimator.SetBool("Open", true);
                }


            }
        }

    }



    private void OnTriggerExit(Collider other)
    {

        doorAnimator.SetBool("Open", false);
        _doorRenderer.material = defualtDoorColor;



    }


    public void LockDoor(){
        _isLocked = true;
        _LockRenderer.material = defualtLockColor;
}

    public void unLockDoor()
    {
        _isLocked = false;
        _LockRenderer.material = detectedzLockColor;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField]
    private float _moveDistance;


    [SerializeField]
    private bool _isUp;

    [SerializeField]

    private float _speed;

    private float distanceThreshold = 2;



    private bool _isMoving;


    private Vector3 _targetPosition;


    [SerializeField]
    GameObject player;


    private CharacterController controller;

    public void ToggleLift()
    {

        if (_isUp)
        {
            _targetPosition = transform.localPosition - new Vector3(0, _moveDistance, 0);
            _isUp = false;
           // player.transform.SetParent(null);

        }
        else
        {

            _targetPosition = transform.localPosition + new Vector3(0, _moveDistance, 0);
            _isUp = true;
           // player.transform.SetParent(gameObject.transform);

           // controller = player.GetComponent<CharacterController>();

           // controller.detectCollisions = false;


            

        }

    }





    // Start is called before the first frame update
    void Start()
    {
        _targetPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, _targetPosition, _speed * Time.deltaTime);

        if (Vector3.Distance(transform.localPosition, _targetPosition) < distanceThreshold)
        {
            EnablePlayerCharacterController();
            _isMoving = false;
        }
        else
        {
            DisablePlayerCharacterController();
            _isMoving = true;
        }
    }


    

private void DisablePlayerCharacterController()
{
    player.transform.SetParent(transform);
    player.GetComponent<CharacterController>().enabled = false;
}

private void EnablePlayerCharacterController()
{
    player.transform.SetParent(null);
    player.GetComponent<CharacterController>().enabled = true;
}
}

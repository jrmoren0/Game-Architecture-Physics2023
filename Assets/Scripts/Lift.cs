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



    private bool _isMoving;


    private Vector3 _targetPosition;

  


    public void ToggleLift()
    {

        if (_isUp)
        {
            _targetPosition = transform.localPosition - new Vector3(0, _moveDistance, 0);
            _isUp = false;

        }
        else
        {

            _targetPosition = transform.localPosition + new Vector3(0, _moveDistance, 0);
            _isUp = true;
            

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
    }
}

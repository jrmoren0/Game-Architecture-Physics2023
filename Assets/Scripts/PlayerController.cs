using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField]
    private float _turnSpeed;

    [SerializeField]
    private float _moveSpeed;

    [SerializeField]
    private Transform _cameraTransform;

    [SerializeField]
    private bool _invertedMouse;

    [SerializeField] 
    private float _gravity = -9.8f;

    [SerializeField]
    private float _jumpForce;



    [SerializeField]
    private GameObject _redProjectile;


    [SerializeField]
    private GameObject _blueProjectile;

    [SerializeField]
    private GameObject _launcher;


    private float _mouseX, _mouseY;

    private float _horizontal, _vertical;



    private CharacterController _characterController;

    private float _cameraXRotation;

    private Vector3 _playerVelocity;


    [Header("Interaction")]
    [SerializeField] private Camera _cam;

    [SerializeField]
    private float _interactionDisctance;


    [SerializeField]
    private LayerMask _interactionLayerMask;


    //Raycast
    private RaycastHit _raycastHit;

    private ISelectable _selectable;


    [Header("Pick and Drop")]
    [SerializeField] LayerMask _pickupLayerMask;

    [SerializeField] float _pickupDisctance;

    [SerializeField] Transform _attachTransform;


    // Pick and Drop

    private bool _isPicked = false;
    private IPickable _pickable;



    void Start()
    {
        _characterController = GetComponent<CharacterController>();

        //Hide Mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;


    }


    void Update()
    {
        GetInput();
        RotatePlayer();
        MovePlayer();
        Jump();
        LaunchRedProjectile();
        LaunchBlueProjectile();
        Interact();
        PickAndDrop();

    }


    void LaunchRedProjectile() {

        if (Input.GetButtonDown("Fire1"))
        {

            GameObject projectile = Instantiate(_redProjectile, _launcher.transform.position, _launcher.transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(_launcher.transform.forward * 1000);
            Destroy(projectile, 5f);
        }

    }


    void LaunchBlueProjectile()
    {

        if (Input.GetButtonDown("Fire2"))
        {
            GameObject projectile = Instantiate(_blueProjectile, _launcher.transform.position, _launcher.transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(_launcher.transform.forward * 1000);
            Destroy(projectile, 5f);
        }

    }


    void Jump()
    {

        if (_characterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {

                _playerVelocity.y = _jumpForce;
            }
        }

    }


    void MovePlayer()
    {

        _characterController.Move(((transform.forward * _vertical) + (transform.right * _horizontal)) * _moveSpeed * Time.deltaTime); // (0,0,1)+ (1,0,0) = (1,0,1)


        if(_characterController.isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -2f;
        }



        _playerVelocity.y += _gravity * Time.deltaTime;

        _characterController.Move(_playerVelocity * Time.deltaTime);

    }


    void RotatePlayer()
    {
        //Turn Player side to side
        transform.Rotate(Vector3.up * _turnSpeed * Time.deltaTime * _mouseX);

        //Turn Player head up and down
        _cameraXRotation += Time.deltaTime * _mouseY * _turnSpeed * (_invertedMouse ? 1 : -1);

        _cameraXRotation = Mathf.Clamp(_cameraXRotation, -85, 85);

        _cameraTransform.localRotation = Quaternion.Euler(_cameraXRotation, 0, 0);

    }

    void GetInput()
    {
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");

        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
    }


    // Turnary Operator Longhand (_invertedMouse ? 1 : -1);
    int InvertMouseCheck()
    {
        if (_invertedMouse)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    void Interact() {

        Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));


        if(Physics.Raycast(ray, out _raycastHit, _interactionDisctance, _interactionLayerMask))
        {

            _selectable = _raycastHit.transform.GetComponent<ISelectable>();


            if(_selectable != null)
            {

                _selectable.OnHoverEnter();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    _selectable.OnSelect();
                }
            }
            
        }

        if(_selectable!= null && _raycastHit.transform == null) {


            _selectable.OnHoverExit();
            _selectable = null;
        }

    }


    void PickAndDrop()
    {
        Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));


        if (Physics.Raycast(ray, out _raycastHit, _pickupDisctance, _pickupLayerMask))
        {

            if(Input.GetKeyDown(KeyCode.E) && !_isPicked)
            {
                _pickable = _raycastHit.transform.GetComponent<IPickable>();
                if(_pickable != null)
                {
                    _pickable.OnPicked(_attachTransform);
                    _isPicked = true;
                    return;
                }
            }
            if(Input.GetKeyDown(KeyCode.E) && _isPicked)
            {
                _pickable.OnDropped();
                _isPicked = false;
            }

        }


        }


    }


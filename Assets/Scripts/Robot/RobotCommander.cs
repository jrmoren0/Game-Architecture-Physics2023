using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotCommander : MonoBehaviour
{



    Queue<Command> _commands = new Queue<Command>();


    Stack<Command> _doneCommnds = new Stack<Command>();

    PlayerInput _input;

    [SerializeField]
    NavMeshAgent _agent;

    [SerializeField]
    private GameObject _pointerPrefab;

    [SerializeField]
    private Camera _cam;

    private Command _currentCommand;

    

    

    // Start is called before the first frame update
    void Start()
    {
        _input = PlayerInput.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        Interact();
    }




    public void  Interact()

    {
        if (_input._commandPressed)
        {
            Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

            if(Physics.Raycast(ray, out var hitInfo))
            {

            
                if (hitInfo.transform.CompareTag("Ground"))
                {
                    //Visulaizer for destination
                    GameObject pointer = Instantiate(_pointerPrefab);
                    pointer.transform.position = hitInfo.point;

                    _commands.Enqueue(new MoveCommand(_agent, hitInfo.point));

                }else if (hitInfo.transform.CompareTag("Builder"))
                {
                    _commands.Enqueue(new BuildCommand(_agent, hitInfo.transform.GetComponent<Builder>()));
                }


            }
        }

        ProccessCommands();

    }



    void ProccessCommands()
    {

        if(_input._undoPressed && _doneCommnds.Count != 0)
        {
            if(_doneCommnds.Peek() == _currentCommand)
            {
                _doneCommnds.Pop().Undo();
            }
            _doneCommnds.Pop().Undo();
        }


        if(_currentCommand != null && !_currentCommand._isComplete)
        {
            return;
        }

        if(_commands.Count == 0)
        {
            return;
        }

        _currentCommand = _commands.Dequeue();


        _doneCommnds.Push(_currentCommand);


        _currentCommand.Execute();

    }


}

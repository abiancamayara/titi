using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

//Manda o conteudo
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumForce;

    private Rigidbody2D _rigidbody2D;
    private Vector2 _moveDirection;

    private Stack<Command> _playerCommands;

    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private bool _isRecording;
    private bool _isPlaying;
    private int _playHead;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerCommands = new Stack<Command>();
    }

    public void RegisterJump(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        
        
        _playerCommands.Push(new Jump(Time.time, _rigidbody2D, jumForce));
        _playerCommands.Peek().Do();

        if (!_isRecording) _playerCommands.Pop();
        
        Debug.Log("pulou");
    }

    public void RegisterMovement(InputAction.CallbackContext context)
    {
        _playerCommands.Push(new Movement(Time.time, context.ReadValue<Vector2>(), this));
        _playerCommands.Peek().Do();
        
        if (!_isRecording) _playerCommands.Pop();
        
        Debug.Log("moveu");
        
    }

    private void Update()
    {
        if (Keyboard.current.rKey.isPressed)
        {
            if (!_isRecording)
            {
                _isRecording = true;
                _startPosition = transform.position;
                _startRotation = transform.rotation;
            }
            else
            {
                _isRecording = false;
            }
        }

        if (Keyboard.current.pKey.isPressed)
        {
            if (!_isPlaying)
            {
                _isPlaying = true;
            }
        }

        if (_isPlaying)
        {
            if(_playerCommands.ElementAt(_playHead))
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.AddForce(_moveDirection * (moveSpeed * Time.fixedDeltaTime));
    }

    public void SetMove(Vector2 move)
    {
        _moveDirection = move;
    }
}

public abstract class Command
{
    public float Time;

    protected Command(float time)
    {
        this.Time = Time;
    }
    
    public abstract void Do();
    public abstract void Undo();
}

public class Jump : Command
{
    private Rigidbody2D _rigidbody2D;
    private float jumpForce;
    
    public Jump(float time, Rigidbody2D rb2D, float jump) : base(time)
    {
        _rigidbody2D = rb2D;
        jumpForce = jump;
    }

    public override void Do()
    {
        _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public override void Undo()
    {
        throw new System.NotImplementedException();
    }
}

public class Movement : Command
{
    private Vector2 moveDirection;
    private PlayerController playerController;
    
    public Movement(float time, Vector2 move, PlayerController player) : base(time)
    {
        moveDirection = move;
        playerController = player;
    }

    public override void Do()
    {
        playerController.SetMove(moveDirection);
    }

    public override void Undo()
    {
        playerController.SetMove(moveDirection*-1);
    }
}

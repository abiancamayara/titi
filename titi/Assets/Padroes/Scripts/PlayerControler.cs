
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

private Stack<Command1> _playerCommands;
    public int coins;

    private void Start()
    {
        _playerCommands = new Stack<Command1>();
    }


    // Colocar os movimentos no Update
    public void Update()
    {
        
        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            _playerCommands.Push(new MovementCommand(Time.time, Vector3.up, transform));
            _playerCommands.Peek().Do();
        }
        
        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            _playerCommands.Push(new MovementCommand(Time.time, Vector3.down, transform));
            _playerCommands.Peek().Do();
        }
        
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            _playerCommands.Push(new MovementCommand(Time.time, Vector3.right, transform));
            _playerCommands.Peek().Do();
        }
        
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            _playerCommands.Push(new MovementCommand(Time.time, Vector3.left, transform));
            _playerCommands.Peek().Do();
        }

        if (Keyboard.current.uKey.wasPressedThisFrame)
        {
            Desfazer();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            _playerCommands.Push(new CollectCoin(Time.time, other.gameObject, this));
            _playerCommands.Peek().Do();
        }
    }

    public void Desfazer()
    {
        if (_playerCommands.Count > 0)
        {
            _playerCommands.Pop().Undo();
        }
    }
}

    // Classe Comando
public abstract class Command1
{
    public float Time;
    protected Command1(float time)
    {
            this.Time = time;
    }
    public abstract void Do();
    public abstract void Undo();
}
    
    // Classe Movement command
public class MovementCommand : Command1
{
    public Vector3 moveDirection;
    private Transform transform;

    public MovementCommand(float time, Vector3 move, Transform trans) : base(time)
    {
        moveDirection = move;
        transform = trans;
    }

    public override void Do()
    {
        transform.position += moveDirection * 2.6F;
    }

    public override void Undo()
    {
        transform.position -= moveDirection * 2.6F;
    }
}

public class CollectCoin : Command1
{
    public GameObject coin;
    public PlayerController player;
    
    public CollectCoin(float time, GameObject coin1, PlayerController player1) : base(time)
    {
        coin = coin1;
        player = player1;
    }

    public override void Do()
    {
        player.coins += 1;
        coin.gameObject.SetActive(false);
    }

    public override void Undo()
    {
        player.coins -= 1;
        player.Desfazer();
        coin.gameObject.SetActive(true);
    }
}

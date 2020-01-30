using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractController : MonoBehaviour
{
    public delegate void OnMove(Vector2 direction);

    public delegate void OnJump();

    private event OnMove onMove;

    private event OnJump onJump;

    protected bool IsInit = false;

    public void AddListeners(OnMove moveAction, OnJump jumpAction)
    {
        OnAddListeners();
        onMove += moveAction;
        onJump += jumpAction;
    }

    protected void MakeJump()
    {
        onJump?.Invoke();
    }

    protected void MakeMove(Vector2 direction)
    {
        onMove?.Invoke(direction);
    }

    protected abstract void OnAddListeners();
}
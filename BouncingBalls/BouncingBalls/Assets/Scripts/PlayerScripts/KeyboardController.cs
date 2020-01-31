using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class KeyboardController : AbstractController
{
    private Coroutine coroutineInstance;
    private Vector2 Direction;

    protected override void OnAddListeners()
    {
        if (!IsInit)
        {
            coroutineInstance = StartCoroutine(ControllerCoroutine());
            IsInit = true;
        }
    }

    private IEnumerator ControllerCoroutine()
    {
        while (true)
        {
            Direction.x = (Input.GetKey(KeyCode.A)) ? (-1) : (Input.GetKey(KeyCode.D) ? (1) : (0));
            Direction.y = (Input.GetKey(KeyCode.S)) ? (-1) : (Input.GetKey(KeyCode.W) ? (1) : (0));
            MakeMove(Direction);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                MakeJump();
            }
            yield return null;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class JoystickController : AbstractController
{
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private Button JumpButton;

    private bool isMove = false;
    private Coroutine coroutineInstance;

    private void OnValidate()
    {
        Debug.Assert(joystick != null, "PlayerController joystick is null!");
        Debug.Assert(JumpButton != null, "PlayerController JumpButton is null!");
    }

    protected override void OnAddListeners()
    {
        if (!IsInit)
        {
            JumpButton.onClick.AddListener(MakeJump);

            joystick.onPointerDown += () => { isMove = true; };
            joystick.onPointerUp += () => { isMove = false; };
            coroutineInstance = StartCoroutine(ControllerCoroutine());
        }
    }

    private IEnumerator ControllerCoroutine()
    {
        while (true)
        {
            if (isMove)
            {
                MakeMove(joystick.Direction);
            }
            yield return null;
        }
    }
}
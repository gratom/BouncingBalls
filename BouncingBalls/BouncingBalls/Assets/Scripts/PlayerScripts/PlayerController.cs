using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
#pragma warning disable

    [HideInInspector] [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private AbstractController controller;

#pragma warning restore

    private AbstractController.OnJump jumpAction;
    private AbstractController.OnMove moveAction;
    private Coroutine controlCoroutineInstance;

    public Vector2 ImpulsMultiplier;
    public float JumpForce;

    public Rigidbody Rig { get => rigidbody; }
    public AbstractController.OnJump JumpAction { set { if (value != null) jumpAction = value; } }
    public AbstractController.OnMove MoveAction { set { if (value != null) moveAction = value; } }

    #region Unity functions

    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            ImpulsMultiplier = GameManager.Instance.settingController.settings.playerMovement.ImpulsMutiplier;
            JumpForce = GameManager.Instance.settingController.settings.playerMovement.JumpForce;
        }
        ResetBehaviour();
        controller.AddListeners(Move, Jump);
    }

    private void OnValidate()
    {
        rigidbody = GetComponent<Rigidbody>();
        Debug.Assert(Rig != null, "PlayerController must have non-null rigidbody component!");
        Debug.Assert(controller != null, "PlayerController controller is null!");
    }

    #endregion Unity functions

    #region public functions

    public void ResetJump()
    {
        jumpAction = DefaultJump;
    }

    public void ResetMove()
    {
        moveAction = DefaultMove;
    }

    public void ResetBehaviour()
    {
        jumpAction = DefaultJump;
        moveAction = DefaultMove;
    }

    #endregion public functions

    #region private functions

    private void Jump()
    {
        jumpAction?.Invoke();
    }

    private void Move(Vector2 direction)
    {
        moveAction?.Invoke(direction);
    }

    private void DefaultJump()
    {
        Rig.AddForce(new Vector3(Rig.velocity.x * Rig.mass * 2f, JumpForce));
    }

    private void DefaultMove(Vector2 direction)
    {
        Rig.AddForce(new Vector3(direction.x * ImpulsMultiplier.x * Time.deltaTime, direction.y * ImpulsMultiplier.y * Time.deltaTime));
    }

    #endregion private functions
}
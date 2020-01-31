using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public delegate void JumpAction();

#pragma warning disable

    [HideInInspector] [SerializeField] public new Rigidbody rigidbody { get; private set; }
    [SerializeField] private AbstractController controller;

#pragma warning restore

    public Vector2 ImpulsMultiplier;
    public float JumpForce;
    public JumpAction jumpAction;
    private Coroutine controlCoroutineInstance;

    #region Unity functions

    private void Awake()
    {
        jumpAction = StandartJump;
        controller.AddListeners(Move, Jump);
    }

    private void OnValidate()
    {
        rigidbody = GetComponent<Rigidbody>();
        Debug.Assert(rigidbody != null, "PlayerController must have non-null rigidbody component!");
        Debug.Assert(controller != null, "PlayerController controller is null!");
    }

    #endregion Unity functions

    #region public functions

    public void ResetJumpAction()
    {
        jumpAction = StandartJump;
    }

    #endregion public functions

    #region private functions

    private void Jump()
    {
        jumpAction?.Invoke();
    }

    private void StandartJump()
    {
        rigidbody.AddForce(new Vector3(rigidbody.velocity.x * 2f, JumpForce));
    }

    private void Move(Vector2 direction)
    {
        rigidbody.AddForce(new Vector3(direction.x * ImpulsMultiplier.x * Time.deltaTime, direction.y * ImpulsMultiplier.y * Time.deltaTime));
    }

    #endregion private functions
}
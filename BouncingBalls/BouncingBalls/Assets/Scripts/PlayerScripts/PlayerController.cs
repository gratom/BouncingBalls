using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
#pragma warning disable

    [HideInInspector] [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private AbstractController controller;

#pragma warning restore

    public Vector2 ImpulsMultiplier;
    public float JumpForce;
    private Coroutine controlCoroutineInstance;

    #region Unity functions

    private void Awake()
    {
        controller.AddListeners(Move, Jump);
    }

    private void OnValidate()
    {
        rigidbody = GetComponent<Rigidbody>();
        Debug.Assert(rigidbody != null, "PlayerController must have non-null rigidbody component!");
        Debug.Assert(controller != null, "PlayerController controller is null!");
    }

    #endregion Unity functions

    #region private functions

    private void Jump()
    {
        rigidbody.AddForce(new Vector3(rigidbody.velocity.x * 2f, JumpForce));
    }

    private void Move(Vector2 direction)
    {
        rigidbody.AddForce(new Vector3(direction.x * ImpulsMultiplier.x * Time.deltaTime, direction.y * ImpulsMultiplier.y * Time.deltaTime));
    }

    #endregion private functions
}
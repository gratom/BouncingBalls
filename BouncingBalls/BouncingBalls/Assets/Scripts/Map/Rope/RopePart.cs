using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RopePart : MonoBehaviour
{
#pragma warning disable

    [HideInInspector] [SerializeField] private Rigidbody rigidbody;

#pragma warning restore

    #region Unity functions

    private void OnValidate()
    {
        rigidbody = GetComponent<Rigidbody>();
        Debug.Assert(rigidbody != null, "RopePart have not rigidbody component!");
    }

    #endregion Unity functions
}
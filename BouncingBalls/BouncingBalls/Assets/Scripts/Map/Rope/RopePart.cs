using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(FixedJoint))]
public class RopePart : AbstractMapObject
{
#pragma warning disable

    [HideInInspector] [SerializeField] private Rigidbody rigidbody;
    [HideInInspector] [SerializeField] private FixedJoint joint;

#pragma warning restore

    public Rigidbody Rig { get => rigidbody; }
    public FixedJoint Joint { get => joint; }

    public RopeMapObject MainRope
    {
        set
        {
            if (mainRope == null)
            {
                mainRope = value;
            }
        }
    }

    private RopeMapObject mainRope;

    public override MapObjectDelegate EnterAction => mainRope?.EnterAction;

    public override MapObjectDelegate ExitAction => mainRope?.ExitAction;

    public override MapObjectDelegate UseAction => mainRope?.UseAction;

    public override MapObjectMoveDelegate MoveAction => mainRope?.MoveAction;

    #region Unity functions

    private void OnValidate()
    {
        rigidbody = GetComponent<Rigidbody>();
        Debug.Assert(rigidbody != null, "RopePart have not rigidbody component!");
        joint = GetComponent<FixedJoint>();
        Debug.Assert(joint != null, "RopePart have not fixedJoint component!");
    }

    #endregion Unity functions
}
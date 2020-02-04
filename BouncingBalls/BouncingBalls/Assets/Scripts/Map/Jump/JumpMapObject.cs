using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMapObject : AbstractMapObject
{
#pragma warning disable

    [SerializeField] private float ForceMultiplier;

#pragma warning restore

    public override MapObjectDelegate EnterAction => (player) =>
    {
        player.PlayerController.Rig.velocity = Vector2.Reflect(player.PlayerController.Rig.velocity, transform.right);
    };

    public override MapObjectDelegate ExitAction => null;

    public override MapObjectDelegate UseAction => null;

    public override MapObjectMoveDelegate MoveAction => null;
}
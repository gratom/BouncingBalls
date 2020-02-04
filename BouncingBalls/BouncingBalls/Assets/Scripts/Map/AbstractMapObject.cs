using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMapObject : MonoBehaviour
{
    public delegate void MapObjectDelegate(Player player);

    public delegate void MapObjectMoveDelegate(Player player, Vector2 direction);

    public abstract MapObjectDelegate EnterAction { get; }

    public abstract MapObjectDelegate ExitAction { get; }

    public abstract MapObjectDelegate UseAction { get; }

    public abstract MapObjectMoveDelegate MoveAction { get; }
}
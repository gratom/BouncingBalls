using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMapObject : MonoBehaviour
{
    public delegate void MapObjectDelegate(Player player);

    public abstract MapObjectDelegate EnterAction { get; }

    public abstract MapObjectDelegate ExitAction { get; }

    public abstract MapObjectDelegate UseAction { get; }
}
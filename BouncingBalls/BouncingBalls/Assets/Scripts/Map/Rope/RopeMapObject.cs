using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeMapObject : AbstractMapObject
{
#pragma warning disable
    [HideInInspector] [SerializeField] private LineRenderer line;
    [SerializeField] private float ForceBonus;
    [SerializeField] private float MinDistance;
#pragma warning restore

    private Coroutine coroutineInstance;

    public override MapObjectDelegate EnterAction => throw new System.NotImplementedException();

    public override MapObjectDelegate ExitAction => throw new System.NotImplementedException();

    public override MapObjectDelegate UseAction => throw new System.NotImplementedException();
}
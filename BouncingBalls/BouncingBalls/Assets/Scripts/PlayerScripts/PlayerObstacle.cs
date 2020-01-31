using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PlayerObstacle : MonoBehaviour
{
    public delegate void ObstacleDelegate(AbstractMapObject currentMapObject);

#pragma warning disable
    [HideInInspector] [SerializeField] private SphereCollider collider;
#pragma warning restore

    public event ObstacleDelegate EnterAction;

    public event ObstacleDelegate ExitAction;

    public AbstractMapObject currentMapObject { get; private set; }

    private AbstractMapObject checkMapObject;

    #region Unity functions

    private void OnValidate()
    {
        collider = GetComponent<SphereCollider>();
        collider.isTrigger = true;
        Debug.Assert(collider != null, "PlayerObstacle have not a collider!");
    }

    private void OnTriggerEnter(Collider other)
    {
        checkMapObject = other.GetComponent<AbstractMapObject>();
        if (checkMapObject != null)
        {
            currentMapObject = checkMapObject;
            EnterAction?.Invoke(currentMapObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentMapObject?.gameObject)
        {
            ExitAction?.Invoke(currentMapObject);
            currentMapObject = null;
        }
    }

    #endregion Unity functions
}
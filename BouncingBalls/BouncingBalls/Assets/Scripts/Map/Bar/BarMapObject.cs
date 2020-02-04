using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BarMapObject : AbstractMapObject
{
#pragma warning disable
    [HideInInspector] [SerializeField] private LineRenderer line;
    [SerializeField] private float ForceBonus = 50;
    [SerializeField] private float MinDistance;
#pragma warning restore

    private Coroutine coroutineInstance;

    public override MapObjectDelegate EnterAction => (player) =>
    {
        if (coroutineInstance == null)
        {
            coroutineInstance = StartCoroutine(BarCoroutine(player));
        }
    };

    public override MapObjectDelegate ExitAction => null;

    public override MapObjectDelegate UseAction => (player) =>
    {
        if (coroutineInstance != null)
        {
            StopCoroutine(coroutineInstance);
            coroutineInstance = null;
            line.SetPosition(1, transform.position);
            player.PlayerController.Rig.AddForce(player.PlayerController.Rig.velocity * ForceBonus);
        }
    };

    public override MapObjectMoveDelegate MoveAction => null;

    #region Unity functions

    private void OnValidate()
    {
        line = GetComponent<LineRenderer>();
        Debug.Assert(line != null, "BarMapObject have not LineRendererComponent!");
        line.positionCount = 2;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position);
    }

    #endregion Unity functions

    #region private functions

    private IEnumerator BarCoroutine(Player player)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (Vector2.Distance(player.transform.position, transform.position) > MinDistance)
            {
                Vector2 v1 = player.transform.position - transform.position;
                float angle1 = Vector2.SignedAngle(v1, player.PlayerController.Rig.velocity);
                Vector2 v2 = Vector2.Perpendicular(v1).normalized;
                player.PlayerController.Rig.velocity = v2 * player.PlayerController.Rig.velocity.magnitude * Mathf.Sin(Mathf.Deg2Rad * angle1);
                player.transform.position = transform.position + ((player.transform.position - transform.position).normalized * MinDistance);
            }
            line.SetPosition(1, player.transform.position);
        }
    }

    #endregion private functions
}
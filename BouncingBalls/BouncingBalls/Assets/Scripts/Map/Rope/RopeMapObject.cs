using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LineRenderer))]
public class RopeMapObject : AbstractMapObject
{
#pragma warning disable

    [HideInInspector] [SerializeField] private Rigidbody rigidbody;
    [HideInInspector] [SerializeField] private LineRenderer line;
    [HideInInspector] [SerializeField] private List<RopePart> Ropes;
    [SerializeField] private float ForceBonus = 50;

    [SerializeField] private Vector2 ImpulsMultiplier;

    [Header("Rope phisyc settings")]
    [SerializeField] private RopePart ropePart;

    [SerializeField] private int PartCount = 5;
    [SerializeField] private float PartDistance = 0.4f;
    [SerializeField] private float ReposeMagnityde;
    [SerializeField] private float MinDistance;
#pragma warning restore

    private Coroutine coroutineInstance;
    private Player playerInstance;
    private float ropeLength;

    private float pointOfPlayer
    {
        get
        {
            return _pointOfPlayer;
        }
        set
        {
            _pointOfPlayer = value > ropeLength ? ropeLength : value;
        }
    }

    private float _pointOfPlayer;
    private bool isContinuous = false;
    private Vector2 playerPointPosition;

    public override MapObjectDelegate EnterAction => (player) =>
    {
        playerInstance = player;
        player.PlayerController.Rig.useGravity = false;
        if (!isContinuous)
        {
            pointOfPlayer = GetIndexOfNearestDot() * PartDistance;
            isContinuous = true;
        }
        if (coroutineInstance == null)
        {
            SetKinematic(false);
            coroutineInstance = StartCoroutine(RopeCoroutine());
        }
    };

    public override MapObjectDelegate ExitAction => null;

    public override MapObjectDelegate UseAction => (player) =>
    {
        if (playerInstance != null)
        {
            player.PlayerController.Rig.AddForce(player.PlayerController.Rig.velocity * ForceBonus);
            player.PlayerController.Rig.useGravity = true;
            playerInstance = null;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, transform.position);
            isContinuous = false;
        }
    };

    public override MapObjectMoveDelegate MoveAction => (player, direction) =>
    {
        pointOfPlayer -= direction.y * Time.deltaTime * ImpulsMultiplier.y;
        Vector3 force = new Vector3(direction.x * ImpulsMultiplier.x * Time.deltaTime, 0);
        player.PlayerController.Rig.AddForce(force);
        Ropes[GetIndexOfNearestDot()].Rig.AddForce(force);
    };

    #region Unity functions

    private void Awake()
    {
        InitiateRopeParts();
    }

    private void OnValidate()
    {
        rigidbody = GetComponent<Rigidbody>();
        line = GetComponent<LineRenderer>();
        Debug.Assert(rigidbody != null, "RopeMapObject have not Rigidbody");
        Debug.Assert(line != null, "RopeMapObject have not LineRenderer");
        Debug.Assert(ropePart != null, "Rope part in rope is null!");
    }

    #endregion Unity functions

    #region private functions

    private void InitiateRopeParts()
    {
        Ropes = new List<RopePart>(PartCount);
        ropeLength = PartCount * PartDistance;
        line.positionCount = 2;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position);
        for (int i = 0; i < PartCount; i++)
        {
            Ropes.Add(Instantiate(ropePart, transform));
            Ropes[i].MainRope = this;
        }
        for (int i = 0; i < Ropes.Count; i++)
        {
            Ropes[i].transform.position = transform.position - new Vector3(0, (i + 1) * PartDistance, 0);
            if (i > 0)
            {
                Ropes[i].Joint.connectedBody = Ropes[i - 1].Rig;
            }
            else
            {
                Ropes[i].Joint.connectedBody = rigidbody;
            }
            Ropes[i].Rig.isKinematic = true;
        }
    }

    private void SetKinematic(bool value)
    {
        for (int i = 0; i < Ropes.Count; i++)
        {
            Ropes[i].Rig.isKinematic = value;
        }
    }

    private void RopePhisyc()
    {
        if (playerInstance != null)
        {
            int n = (int)System.Math.Truncate(pointOfPlayer / PartDistance);
            Vector2 v;
            if (n + 1 < Ropes.Count)
            {
                v = Ropes[n + 1].transform.position - Ropes[n].transform.position;
            }
            else
            {
                v = new Vector2(0, 0);
            }
            playerPointPosition = (Vector2)Ropes[n].transform.position + v * (pointOfPlayer % PartDistance) / PartDistance;

            if (Vector2.Distance(playerInstance.transform.position, playerPointPosition) > MinDistance)
            {
                Vector2 v1 = (Vector2)playerInstance.transform.position - playerPointPosition;
                float angle = Vector2.SignedAngle(v1, playerInstance.PlayerController.Rig.velocity);
                Vector2 v2 = Vector2.Perpendicular(v1).normalized;
                playerInstance.PlayerController.Rig.velocity = v2 * playerInstance.PlayerController.Rig.velocity.magnitude * Mathf.Sin(Mathf.Deg2Rad * angle);
                Vector3 newPos = playerPointPosition + (((Vector2)playerInstance.transform.position - playerPointPosition).normalized * MinDistance);
                newPos.z = 0;
                playerInstance.transform.position = newPos;
            }
            line.SetPosition(0, playerPointPosition);
            line.SetPosition(1, playerInstance.transform.position);
        }
    }

    private int GetIndexOfNearestDot()
    {
        float distance = float.MaxValue;
        float distCur;
        int index = 0;
        for (int i = 0; i < Ropes.Count; i++)
        {
            distCur = Vector2.Distance(playerInstance.transform.position, Ropes[i].transform.position);
            if (distCur < distance)
            {
                distance = distCur;
                index = i;
            }
        }
        return index;
    }

    private bool CheckRepose()
    {
        if (playerInstance != null)
        {
            return true;
        }
        for (int i = 0; i < Ropes.Count; i++)
        {
            if (Ropes[i].Rig.velocity.magnitude > ReposeMagnityde)
            {
                return true;
            }
        }
        return false;
    }

    private IEnumerator RopeCoroutine()
    {
        while (CheckRepose())
        {
            yield return new WaitForFixedUpdate();
            RopePhisyc();
        }
        coroutineInstance = null;
        SetKinematic(true);
    }

    #endregion private functions
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollower : MonoBehaviour
{
#pragma warning disable

    [HideInInspector]
    [SerializeField] private new Camera camera;

    [SerializeField] private GameObject FollowThat;

#pragma warning restore

    public Vector3 Offset;
    public float MinDistance;
    public float Smoothing;
    public bool IsFollow { get; set; } = true;
    private Coroutine followerCoroutineInstance;

    #region Unity functions

    private void Awake()
    {
        followerCoroutineInstance = StartCoroutine(FolowerCoroutine());
    }

    private void OnValidate()
    {
        camera = GetComponent<Camera>();
        Debug.Assert(camera != null, "Camera component nust be on this object!");
        Debug.Assert(FollowThat != null, "'FollowThat' is null!");
    }

    #endregion Unity functions

    #region private functions

    private void Follow()
    {
        if (Vector3.Distance(FollowThat.transform.position + Offset, camera.transform.position) > MinDistance)
        {
            camera.transform.position += (FollowThat.transform.position + Offset - camera.transform.position) / Smoothing;
        }
    }

    private IEnumerator FolowerCoroutine()
    {
        while (true)
        {
            while (IsFollow)
            {
                yield return new WaitForFixedUpdate();
                Follow();
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    #endregion private functions
}
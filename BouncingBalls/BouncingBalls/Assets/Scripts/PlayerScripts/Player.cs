﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
#pragma warning disable
    public PlayerController PlayerController { get => playerController; }
    [HideInInspector] [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerObstacle playerObstacle;
#pragma warning restore

    #region Unity functions

    private void Awake()
    {
        playerObstacle.EnterAction += OnEnter;
        playerObstacle.ExitAction += OnExit;
    }

    private void OnValidate()
    {
        playerController = GetComponent<PlayerController>();
        Debug.Assert(playerController != null, "Player have not PlayerController!");
        Debug.Assert(playerObstacle != null, "Player have not PlayerObstacle!");
    }

    #endregion Unity functions

    #region private functions

    private void OnEnter(AbstractMapObject mapObject)
    {
        if (mapObject.UseAction != null)
        {
            playerController.jumpAction = () =>
            {
                mapObject.UseAction(this);
                playerController.ResetJumpAction();
            };
        }
        mapObject.EnterAction?.Invoke(this);
    }

    private void OnExit(AbstractMapObject mapObject)
    {
        mapObject.ExitAction?.Invoke(this);
    }

    #endregion private functions
}
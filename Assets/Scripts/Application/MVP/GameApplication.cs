using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameApplication : MonoBehaviour
{
    private static GameApplication instance;
    public static GameApplication Instance { get => instance ??= FindObjectOfType<GameApplication>(); }

    private GameModel gameModel;
    public GameModel GameModel 
    {
        get => gameModel ??= FindObjectOfType<GameModel>();
    }

    private EntityController entityController;
    public EntityController EntityController
    {
        get => entityController ??= FindObjectOfType<EntityController>();
    }

    private PlayerManager playerManager;
    public PlayerManager PlayerManager
    {
        get => playerManager ??= FindObjectOfType<PlayerManager>();
    }
}

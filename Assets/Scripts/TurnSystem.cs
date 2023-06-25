using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurnSystem : MonoBehaviour
{

    public static TurnSystem Instance;
    public event EventHandler OnTurnChanged;

    public void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one TurnSystem! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private int turnNumber = 1;
    [SerializeField] private bool isPlayerTurn;

    public void NextTurn()
    {
        turnNumber++;
        isPlayerTurn = !isPlayerTurn;
        OnTurnChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetTurnNumber()
    {
        return turnNumber;
    }

    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }

}

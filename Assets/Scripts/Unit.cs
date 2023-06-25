using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Unit : MonoBehaviour
{
    private const int ACTION_POINT_MAX = 2;

    public static event EventHandler OnAnyActionPointsChanged;

    [SerializeField] private bool isEnemy;

    private GridPosition gridPosition;
    private HealthSystem healthSystem;
    private MoveAction moveAction;
    private SpinAction SpinAction;
    private BaseAction[] baseActionArray;
    [SerializeField] private int actionPoints = ACTION_POINT_MAX;

    private void Awake()
    {
        moveAction = GetComponent<MoveAction>();
        SpinAction = GetComponent<SpinAction>();
        baseActionArray = GetComponents<BaseAction>();
        healthSystem = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);

        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanges;
        healthSystem.OnDead += HealthSystem_OnDead;
    }



    private void Update()
    {
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition)
        {
            // Unit changed Grid Position
            LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }
    }

    public MoveAction GetMoveAction()
    {
        return moveAction;
    }

    public SpinAction GetSpinAction()
    {
        return SpinAction;
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    public BaseAction[] GetBaseActionsArray()
    {
        return baseActionArray;
    }

    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }

    public bool TrySpendActionPointsToTakeAction(BaseAction baseAction)
    {
        if(CanSpendActionPointsToTakeActions(baseAction))
        {
            SpendActionPoints(baseAction.GetActionPointCost());
            return true;
        }

        return false;
    }

    public bool CanSpendActionPointsToTakeActions(BaseAction baseAction)
    {
        if(actionPoints >= baseAction.GetActionPointCost())
        {
            return true;
        }

        return false;   
    }

    private void SpendActionPoints(int amount)
    {
        actionPoints -= amount;

        OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
    }

    private void TurnSystem_OnTurnChanges(object sender, System.EventArgs e)
    {

        if(IsEnemy())
        {

            if(!TurnSystem.Instance.IsPlayerTurn())
            {
                actionPoints = ACTION_POINT_MAX;
                OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
            }

        }

        if (!IsEnemy())
        {
            Debug.Log("REPLENISH");
            if (TurnSystem.Instance.IsPlayerTurn())
            {
                actionPoints = ACTION_POINT_MAX;
                OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
            }

        }

    }

    public int GetActionPoints()
    {
        return actionPoints;
    }

    public bool IsEnemy()
    {
        return isEnemy;
    }

    public void Damage(int damageAmount)
    {
        healthSystem.Damage(damageAmount);
    }

    private void HealthSystem_OnDead(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        LevelGrid.Instance.RemoveUnitAtGridPosition(gridPosition, this);
        healthSystem.OnDead -= HealthSystem_OnDead;
    }
}

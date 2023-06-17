using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem instance { get; private set; }
    public event EventHandler OnSelectedUnitChange;

    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitsMask;

    private void Awake()
    {
        if (instance != null) 
        { 
            Debug.LogError("There's more than one UnitActionSystem!"); 
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (TryHandleUnitSelection()) return;
            selectedUnit.Move(MouseWorld.GetPosition());
        }
    }

    private bool TryHandleUnitSelection()
    {
        //if(MouseWorld.GetUnit() != null)
        //selectedUnit = MouseWorld.GetUnit();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, unitsMask))
        {
            if(hit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                SetSelectedUnit(unit);
                return true;
            }
        }

        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;

        OnSelectedUnitChange?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }
}

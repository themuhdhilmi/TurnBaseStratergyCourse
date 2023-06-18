using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject 
{

    private GridPosition gridPosition;
    private GridSystem gridSystem;
    private List<Unit> unitList;

    public GridObject(GridSystem gridSystem , GridPosition gridPosition)
    {
        this.gridPosition = gridPosition;
        this.gridSystem = gridSystem;
        unitList = new List<Unit>();
    }

    public void AddUnit(Unit unit)
    {
        unitList.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        unitList.Remove(unit);
    }

    public List<Unit> GetUnitList()
    {
        return unitList;
    }

    public override string ToString()
    {
        string unitString = "";
        foreach (var item in unitList)
        {
            unitString += item.ToString() + "\n";
        }
        return gridPosition.ToString() + "\n" + unitString;
    }
}

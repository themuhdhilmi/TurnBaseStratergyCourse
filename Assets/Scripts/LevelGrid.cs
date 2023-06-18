using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    private GridSystem gridSystem;
    [SerializeField] private Transform gridDebugObjectPrefabs;

    private void Awake()
    {
        gridSystem = new GridSystem(10, 10, 2f);
        gridSystem.CreateDebugObjects(gridDebugObjectPrefabs);
    }

    //public void SetUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    //{

    //}

    //public Unit GetUnitAtGridPosition(GridPosition gridPosition)
    //{

    //}

    //public void ClearUnitAtGridPosition(GridPosition gridPosition)
    //{

    //}

    void Update()
    {
        Debug.Log(gridSystem.GetGridPosition(MouseWorld.GetPosition()));
    }
}

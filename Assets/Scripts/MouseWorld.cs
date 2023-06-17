using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    private static MouseWorld instance;
    [SerializeField] private LayerMask mousePlaneMask;
    //[SerializeField] private LayerMask unitsMask;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        transform.position = MouseWorld.GetPosition();
    }

    public static Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, instance.mousePlaneMask);
        return hit.point;
    }

    //public static Unit GetUnit()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //    if(Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, instance.unitsMask))
    //    {
    //        return hit.transform.gameObject.GetComponent<Unit>();
    //    }

    //    return null;  
    //}

}

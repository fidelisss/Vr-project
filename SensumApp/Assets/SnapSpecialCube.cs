using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapSpecialCube : MonoBehaviour
{
    [SerializeField] private Vector3 gridSize = default;
    private void FixedUpdate()
    {
        SnapToGrid();
    }

    private void SnapToGrid()
    {
        var position = new Vector3(
            Mathf.RoundToInt(this.transform.position.x / this.gridSize.x) * this.gridSize.x,
            Mathf.RoundToInt(this.transform.position.y / this.gridSize.y) * this.gridSize.y,
            Mathf.RoundToInt(this.transform.position.z / this.gridSize.z) * this.gridSize.z
            );
        this.transform.position = position;
        this.transform.rotation = Quaternion.identity;
    }
}

using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GridItem : MonoBehaviour
{
    [PunRPC]
    public void SyncUIGrid(string parentID, int i, int columns, float margin)
    {
        transform.parent = ParentManager.instance.GetParent(parentID);
        int column = i % columns;
        int row = (int)(i / columns);
        transform.localPosition = new Vector3(margin * row, 0, margin * column);
    }
}

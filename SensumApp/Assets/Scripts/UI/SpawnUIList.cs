using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SpawnUIList : MonoBehaviour
{
    [SerializeField] private GameObject[] _listItems;
    [SerializeField] private float margin;
    [SerializeField] private int columns;
    

    private void Awake()
    {
        // CreateList();
    }

    public void CreateList()
    {
        StartCoroutine(CreateListCoroutine());
    }

    private IEnumerator CreateListCoroutine()
    {
        for (int i = 0; i < _listItems.Length; i++)
        {

            GameObject listItem = null;
            if (PhotonNetwork.InRoom)
            {
                listItem = PhotonNetwork.Instantiate(_listItems[i].name, transform.position, Quaternion.identity);
                listItem.GetComponent<PhotonView>().RPC("SyncUIGrid", RpcTarget.All, GetComponent<UniqueID>().ID, i, columns, margin);
            }
            else
            {
                listItem = Instantiate(_listItems[i], transform.position, Quaternion.identity);
                listItem.GetComponent<GridItem>().SyncUIGrid(GetComponent<UniqueID>().ID, i, columns, margin);
            }
            yield return null;
        }
    }

    // [PunRPC]
    // private void SyncUIGrid(GameObject listItem, int i)
    // {
    //     listItem.transform.parent = transform;
    //     int column = i % columns;
    //     int row = (int)(i / columns);
    //     listItem.transform.localPosition = new Vector3(margin * row, 0, margin * column);
    // }
}

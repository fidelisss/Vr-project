using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class StaticIcon : MonoBehaviour
{
    [SerializeField] private GameObject StaticContent;    // content prefab for lecture
    [SerializeField] private GameObject SummonPoint;  // gameobject- position reference for content to spawn at
    [SerializeField] private GameObject Button;
    private Collider colliderComp;
    private Vector3 startPos;
    public bool OnActive;

    private void Start()
    {
        OnActive = false;
        colliderComp = gameObject.GetComponent<Collider>();
        startPos = colliderComp.transform.position;
    }
    private void FixedUpdate() //temp solution before adding holograms
    {
        if (Button.activeSelf == false)
        {
            colliderComp.transform.position = new Vector3(0, 0, 0);

        }
        else if (Button.activeSelf == true)
        {
           colliderComp.transform.position = startPos;
        }
    }
    public  void OnSwitch()
    {
        if (GameObject.FindGameObjectsWithTag("StaticContent").Length == 0) OnActive = false;
      
        if (OnActive == true && Button.activeSelf == true)
        {
            // Destroy all objects with this tag, just to be shure
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("StaticContent"))
            {
                PhotonNetwork.Destroy(g);
            }
            OnActive = false;
        }
        else if (OnActive == false && Button.activeSelf == true)
        {
            // Replacing model from another lecture
            GameObject oldObject = GameObject.FindGameObjectWithTag("StaticContent");
            if (oldObject) 
            {
                if (PhotonNetwork.InRoom)
                    PhotonNetwork.Destroy(oldObject);
                else 
                    Destroy(oldObject);
            }

            GameObject g = null;
            if (PhotonNetwork.InRoom)
                g = PhotonNetwork.Instantiate(StaticContent.name, SummonPoint.transform.position, SummonPoint.transform.rotation);
            else
                g = Instantiate(StaticContent, SummonPoint.transform.position, SummonPoint.transform.rotation);
                
            g.tag = ("StaticContent");
            
            OnActive = true;
        }
        
    }
}

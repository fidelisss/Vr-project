using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletionZone : MonoBehaviour
{
    public float deletionTime = 2f;

    public List<string> ignoreTags = new List<string> {
        "Liquid"
    };

    public List<string> onlyTags = new List<string> {
        
    };

    private IEnumerator WaitCoroutineInstance;

    private void OnTriggerEnter(Collider col)
    {
        bool willDelete = false;
        if (onlyTags.Count != 0)
            willDelete = onlyTags.Contains(col.gameObject.tag);
        else
            willDelete = !ignoreTags.Contains(col.gameObject.tag);

        if (willDelete)
            if (col.TryGetComponent(out Spawner spawner))
            {
                WaitCoroutineInstance = WaitCoroutine(deletionTime, spawner);
                StartCoroutine(WaitCoroutineInstance);
            }
    }

    private void OnTriggerExit(Collider col)
    {
        if (!ignoreTags.Contains(col.gameObject.tag))
            if (col.gameObject.GetComponent<Spawner>() && WaitCoroutineInstance != null)
            {
                StopCoroutine(WaitCoroutineInstance);
            }
    }

    private IEnumerator WaitCoroutine(float time, Spawner item)
    {
        if (time > 0)
            yield return new WaitForSeconds(time);

       item.Respawn();
    }

}

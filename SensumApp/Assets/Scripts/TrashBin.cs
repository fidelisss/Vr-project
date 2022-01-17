using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : MonoBehaviour
{
    [SerializeField] private SoundEffectPlayer soundEffect;
    private bool cooldown;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Spawner>())
        {
            other.GetComponent<Spawner>().Despawn(); //destroys any item that has RigidBody
            if (cooldown) { soundEffect.Play("TrashCan"); cooldown = false; StartCoroutine(CooldownTimer(cooldown)); }
        }
    }
    private IEnumerator CooldownTimer(bool _coolDown)
    {
        cooldown = _coolDown;
        new WaitForSeconds(1f);

        _coolDown = true;
        yield return null;
    }

}





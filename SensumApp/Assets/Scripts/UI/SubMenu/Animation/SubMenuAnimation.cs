using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class SubMenuAnimation
{
    private static IEnumerator Simple(Transform transform, Vector3 endPosition, float animationDuration)
    {
        transform.DOLocalMove(endPosition, animationDuration);
        yield return new WaitForSeconds(animationDuration);
    }
}
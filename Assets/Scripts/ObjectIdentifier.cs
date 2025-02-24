using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectIdentifier : MonoBehaviour
{
    [SerializeField] private LayerMask target;

    protected bool LayerCheck(LayerMask _layerMask)
    {
        if (target.value==(target.value|(1<<_layerMask.value)))
        {
            return true;
        }
        return false;
    }

}

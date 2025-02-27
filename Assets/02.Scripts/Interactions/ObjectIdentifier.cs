using UnityEngine;

public class ObjectIdentifier : MonoBehaviour
{
    [SerializeField] private LayerMask target;

    protected bool LayerCheck(LayerMask layerMask)
    {
        if (target.value==(target.value|(1<<layerMask.value)))
        {
            return true;
        }
        return false;
    }

}

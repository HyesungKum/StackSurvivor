using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
public class CardGizmo : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(this.transform.position, this.transform.lossyScale);
    }
}
#endif

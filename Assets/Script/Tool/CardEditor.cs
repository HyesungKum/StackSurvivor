using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// this script custermizing to card edit place in inspector window
/// </summary>
[CustomEditor(typeof(BaseCard))]

[CanEditMultipleObjects]
public class CardEditor : Editor
{

    [SerializeField] bool aa = false;
    [SerializeField] bool bb = false;
    [SerializeField] bool cc = false;
    // Start is called before the first frame update
    public override void OnInspectorGUI()
    {
        BaseCard baseCard = (BaseCard)target;

        DrawDefaultInspector();
    }
}

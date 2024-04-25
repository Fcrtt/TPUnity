using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayCell : MonoBehaviour
{
    Material mat;
    [HideInInspector] public Vector3Int coords;
    GameObject token;

    public bool HasToken => token != null;

    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        mat = renderer.material;

        UpdateCoords();
    }

    void UpdateCoords()
    {
        Vector3Int coordsFromWorldSpace = new Vector3Int();
        coordsFromWorldSpace.x = Mathf.RoundToInt(transform.position.x);
        coordsFromWorldSpace.y = Mathf.RoundToInt(transform.position.y);
        coordsFromWorldSpace.z = Mathf.RoundToInt(transform.position.z);
        coords = coordsFromWorldSpace;
    }

    public void OnHoverEnter()
    {
        mat.SetFloat("_IsHovered", 1);
    }

    public void OnHoverExit()
    {
        mat.SetFloat("_IsHovered", 0);
    }

    public void SetToken(GameObject token)
    {
        this.token = token;
        GetComponent<BoxCollider>().enabled = false;
        mat.SetFloat("_HasToken", 1);

        OnHoverExit();
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(PlayCell))]
public class PlayCellEditor : Editor
{
    SerializedProperty coords;

    void OnEnable()
    {
        coords = serializedObject.FindProperty("coords");
    }

    public override void OnInspectorGUI()
    {
        GUI.enabled = false;
        EditorGUILayout.PropertyField(coords);
        GUI.enabled = true;
    }
}
#endif
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Transform), true)]

public class ColliderX : Editor
{
    #region private members
    private bool _enforceMeshColliders = false;
    private bool _useConvex = true;
    private bool _keep = false;

    private Transform _target;
    private GUISkin skin;

    //Unity's built-in editor
    Editor defaultEditor;
    private bool showInfo;
    private int _vertCount;
    private static string myPubID = "46749";

    [Tooltip("Number of Vertices below which a Box Collider will be used")]
    private int vertLimit = 200;
    //[Range(0.1f, 1f)]
    private float blockinessThreshold = 0.6f;

    private bool isBox;
    private string meshType;
    private ColliderLimits colliderLimits;
    private bool settingsSection;
    #endregion

    void OnEnable()
    {
        //When this inspector is created, also create the built-in inspector
        defaultEditor = Editor.CreateEditor(targets, Type.GetType("UnityEditor.TransformInspector, UnityEditor"));

        //initialization
        showInfo = true;
        skin = Resources.Load<GUISkin>("guiStyles/Default");
        colliderLimits = Resources.Load<ColliderLimits>("settings/ColliderLimits");//there's already one scriptable object asset provided and you don't actually need to create another one, just find it and change its variables
        vertLimit = colliderLimits.VertLimit;
        blockinessThreshold = colliderLimits.BlockinessThreshold;
    }

    void OnDisable()
    {
        //When OnDisable is called, the default editor we created should be destroyed to avoid memory leakage.
        //Also, make sure to call any required methods like OnDisable
        MethodInfo disableMethod = defaultEditor.GetType().GetMethod("OnDisable", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        if (disableMethod != null)
            disableMethod.Invoke(defaultEditor, null);
        DestroyImmediate(defaultEditor);
    }

    /// <summary>
    /// Draws UI. Called every time the mouse hovers over the editor
    /// </summary>
    public override void OnInspectorGUI()
    {
        //Drawing UI
        EditorGUILayout.LabelField("Local Space", EditorStyles.boldLabel);
        defaultEditor.OnInspectorGUI();

        //Uncomment to show world space position, rotation and scale
        //ShowWorldSpace();

        GUI.enabled = true;
        DrawXColliderInspector();
    }

    /// <summary>
    /// Reveals WorldSpace data
    /// </summary>
    private void ShowWorldSpace()
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("World Space", EditorStyles.boldLabel);

        GUI.enabled = false;
        Vector3 localPosition = _target.localPosition;
        _target.localPosition = _target.position;

        Quaternion localRotation = _target.localRotation;
        _target.localRotation = _target.rotation;

        Vector3 localScale = _target.localScale;
        _target.localScale = _target.lossyScale;

        defaultEditor.OnInspectorGUI();
        _target.localPosition = localPosition;
        _target.localRotation = localRotation;
        _target.localScale = localScale;
    }

    /// <summary>
    /// Draws the Editor UI
    /// </summary>
    private void DrawXColliderInspector()
    {
        _target = (Transform)target;

        int _meshCount = _target.GetComponentsInChildren<MeshFilter>().Length;
        int _colliderCount = _target.GetComponentsInChildren<Collider>().Length;
        int _boxColliderCount = _target.GetComponentsInChildren<BoxCollider>().Length;
        int _meshColliderCount = _target.GetComponentsInChildren<MeshCollider>().Length;

        _vertCount = 0;
        MeshFilter _item = _target.GetComponent<MeshFilter>();

        if (_meshCount > 0)
        {
            if (_target.GetComponent<MeshFilter>() != null)
                _vertCount = _target.GetComponent<MeshFilter>().sharedMesh.vertexCount;

            EditorGUILayout.Space(20);
            //GUILayout.Space(20);
            showInfo = EditorGUILayout.BeginFoldoutHeaderGroup(showInfo, "ColliderX", skin.GetStyle("PanHeaderDefault"));
            if (showInfo)
            {


                EditorGUILayout.Space(5);
                //GUILayout.Label($"Colliders");

                if (_item != null)
                {
                    GUILayout.BeginHorizontal();

                    isBox = (IsBlocky(_item) || _item.sharedMesh.vertexCount < vertLimit);
                    //meshType = isBox ? $"Box Colliders for blocky objects under {vertLimit}" : $"Mesh Colliders for blocky objects under {vertLimit}";
                    meshType = isBox ? "Box" : "Mesh";
                    GUILayout.Label($"Is Blocky? {IsBlocky(_item)}, Is LowPoly? { _item.sharedMesh.vertexCount < vertLimit}");


                    GUILayout.Label($"{_item.name} Verts: {_vertCount}");
                    GUILayout.EndHorizontal();

                    GUILayout.Label(meshType, isBox ? skin.GetStyle("GreenText") : skin.GetStyle("RedText"));
                    EditorGUILayout.Space(5);
                }

                GUILayout.BeginHorizontal();
                _keep = GUILayout.Toggle(_keep, "Keep Existing");

                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();

                _enforceMeshColliders = GUILayout.Toggle(_enforceMeshColliders, "Enforce Mesh Colliders");
                _useConvex = GUILayout.Toggle(_useConvex, "Use Convex");


                GUILayout.EndHorizontal();
                EditorGUILayout.Space(5);
                if (GUILayout.Button($"Generate Colliders for {_meshCount} objects"))
                {
                    colliderLimits.VertLimit = vertLimit;
                    colliderLimits.BlockinessThreshold = blockinessThreshold;
                    AddColliders(_enforceMeshColliders, _useConvex);

                }

                if (_colliderCount > 0)
                {
                    var msg = (_colliderCount > 1) ?
                        $"Remove {_boxColliderCount} Box & {_meshColliderCount} Mesh Colliders" :
                        $"Remove {_colliderCount} Colliders";
                    if (GUILayout.Button(msg))
                        RemoveAllColliders();
                }


            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            GUILayout.Space(15);
            settingsSection = EditorGUILayout.BeginFoldoutHeaderGroup(settingsSection, "Detection Settings");//, skin.GetStyle("H2"));
            if (settingsSection)
            {

                vertLimit = EditorGUILayout.IntField("Verts Limit:", Mathf.Clamp(vertLimit, 3, 10000));
                blockinessThreshold = EditorGUILayout.FloatField("Block Thresh Limit:", Mathf.Clamp(blockinessThreshold, 0.01f, 1.5f));

            }

            EditorGUILayout.EndFoldoutHeaderGroup();

            GUILayout.Space(10);


            if (GUILayout.Button("More cool tools...", skin.GetStyle("PanStoreLink")))
            {
                Application.OpenURL($"https://assetstore.unity.com/publishers/" + myPubID);
                Application.OpenURL($"https://panettonegames.com/");
            }
            GUILayout.Space(5);
        }
    }


    public int GetCollidersCount()
    {
        return _target.GetComponentsInChildren<MeshFilter>().Length;
    }

    /// <summary>
    /// Generates Colliders to all child transforms
    /// </summary>
    /// <param name="enfoceMeshCollider">Always Creates mesh colliders regardless of vertics count and GameObject Dimensions/Blockiness</param>
    /// <param name="useConvex">Enforces Convex colliders</param>
    public void AddColliders(bool enfoceMeshCollider, bool useConvex)
    {
        var items = _target.GetComponentsInChildren<MeshFilter>();
        foreach (var item in items)
        {
            if (!_keep)
                RemoveColliders(item);

            if (enfoceMeshCollider)
            {
                if (item.gameObject.GetComponent<Collider>() == null)
                    item.gameObject.AddComponent<MeshCollider>().convex = useConvex;
            }
            else
            {

                if ((IsBlocky(item) || item.sharedMesh.vertexCount < vertLimit))
                {
                    if (item.gameObject.GetComponent<Collider>() == null)
                        item.gameObject.AddComponent<BoxCollider>();
                }
                else
                {
                    if (item.gameObject.GetComponent<Collider>() == null)
                        item.gameObject.AddComponent<MeshCollider>().convex = useConvex;
                }
            }
        }
    }

    /// <summary>
    /// Decides whether or not the GameObject is blocky based on the BlockinessThreshold settings in the ColliderLimits Scriptable Objects
    /// </summary>
    /// <param name="item">The Target GameObject Mesh</param>
    /// <returns></returns>

    private bool IsBlocky(MeshFilter item)
    {
        return
            Mathf.Abs(item.sharedMesh.bounds.size.x - item.sharedMesh.bounds.size.y) < blockinessThreshold ||
            Mathf.Abs(item.sharedMesh.bounds.size.x - item.sharedMesh.bounds.size.z) < blockinessThreshold ||
            Mathf.Abs(item.sharedMesh.bounds.size.y - item.sharedMesh.bounds.size.z) < blockinessThreshold;
    }

    /// <summary>
    /// Clears colliders for a particular GameObject with a MeshFilter
    /// </summary>
    /// <param name="item">Child GameObject</param>
    private void RemoveColliders(MeshFilter item)
    {
        foreach (var xCollider in item.gameObject.GetComponents<Collider>())
            DestroyImmediate(xCollider);
    }

    /// <summary>
    /// Clears colliders from all children with MeshFilter
    /// </summary>
    public void RemoveAllColliders()
    {
        var items = _target.GetComponentsInChildren<MeshFilter>();
        foreach (var item in items)
            RemoveColliders(item);
    }
}
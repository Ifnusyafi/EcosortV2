#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;

public class RemoveMissing : MonoBehaviour
{
    [MenuItem("Tools/Clean Up Prefabs (Remove Missing Scripts)")]
    static void CleanPrefabs()
    {
        string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { "Assets" });

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab == null) continue;

            GameObject prefabInstance = PrefabUtility.LoadPrefabContents(path);
            GameObjectUtility.RemoveMonoBehavioursWithMissingScript(prefabInstance);
            PrefabUtility.SaveAsPrefabAsset(prefabInstance, path);
            PrefabUtility.UnloadPrefabContents(prefabInstance);

            Debug.Log("Cleaned: " + path);
        }

        AssetDatabase.Refresh();
        Debug.Log("All prefabs cleaned!");
    }
}
#endif

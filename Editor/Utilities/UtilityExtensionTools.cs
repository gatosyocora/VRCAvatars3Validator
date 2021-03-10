using UnityEditor;
using UnityEngine;

public class UtilityExtensionTools : Editor
{
    [MenuItem("Assets/Copy GUID")]
    public static void CopyGUID()
    {
        var asset = Selection.activeObject;
        var path = AssetDatabase.GetAssetPath(asset);
        var guid = AssetDatabase.AssetPathToGUID(path);
        GUIUtility.systemCopyBuffer = guid;
    }
}

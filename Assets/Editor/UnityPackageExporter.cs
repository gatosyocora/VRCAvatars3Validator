using UnityEditor;


public static class UnityPackageExporter
{
    public static void Export()
    {
        var exportDirs = new[]
        {
            "Assets/VRCAvatars3Validator"
        };
        var exportPath = "./VRCAvatars3Validator.unitypackage";

        AssetDatabase.ExportPackage(
            exportDirs,
            exportPath,
            ExportPackageOptions.Recurse);
    }
}

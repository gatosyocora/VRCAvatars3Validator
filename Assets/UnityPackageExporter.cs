using UnityEditor;

public static class UnityPackageExporter
{
    private static string[] exportDirs =
    {
        "Assets/VRCAvatars3Validator"
    };

    private static string exportPath = "./VRCAvatars3Validator.unitypackage";

    public static void Export()
        => AssetDatabase.ExportPackage(
            exportDirs,
            exportPath,
            ExportPackageOptions.Recurse);
}

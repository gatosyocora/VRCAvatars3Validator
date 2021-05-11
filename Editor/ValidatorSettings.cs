using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ValidatorSettings : ScriptableObject
{
    private const string SETTINGS_FILE_PATH = "Assets/VRCAvatars3Validator/Editor/Settings.asset";

    public bool validateOnUploadAvatar = true;

    public static ValidatorSettings GetOrCreateSettings()
    {
        ValidatorSettings settings;
        settings = AssetDatabase.LoadAssetAtPath<ValidatorSettings>(SETTINGS_FILE_PATH);
        if (settings) return settings;

        settings = CreateInstance<ValidatorSettings>();
        AssetDatabase.CreateAsset(settings, SETTINGS_FILE_PATH);
        AssetDatabase.SaveAssets();
        return settings;
    }
}

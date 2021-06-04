using Kogane;
using System;
using UnityEditor;
using UnityEngine;
using VRCAvatars3Validator.Models;
using VRCAvatars3Validator.Utilities;

namespace VRCAvatars3Validator 
{
    public class Localize
    {
        public static string LANGUAGE_PACK_FOLDER = "Assets/VRCAvatars3Validator/Editor/Langs/";

        public static ValidatorSettings settings = ValidatorSettingsUtility.GetOrCreateSettings();
        public static LanguageType languageType;

        public static LanguagePack languagePack;

        public static JsonDictionary translateDictionary;

        public static string Translate(string textId)
        {
            if (languageType != settings.languageType)
            {
                languageType = settings.languageType;
                languagePack = null;
                translateDictionary = null;
            }

            if (languagePack == null)
            {
                languagePack = AssetDatabase.LoadAssetAtPath<LanguagePack>($"{LANGUAGE_PACK_FOLDER}{languageType}.asset");
            }

            if (translateDictionary == null)
            {
                translateDictionary = JsonUtility.FromJson<JsonDictionary>(languagePack.data);
            }
            return translateDictionary.Dictionary[textId];
        }
    }
}

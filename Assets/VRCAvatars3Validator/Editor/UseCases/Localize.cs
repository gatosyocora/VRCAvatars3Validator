using Kogane;
using System;
using UnityEditor;
using UnityEngine;
using VRCAvatars3Validator.Models;

namespace VRCAvatars3Validator 
{
    public class Localize
    {
        public static string LANGUAGE_PACK_FOLDER = "Assets/VRCAvatars3Validator/Editor/Langs/";

        public static LanguageType languageType = LanguageType.EN;

        public static LanguagePack languagePack;

        public static JsonDictionary translateDictionary;

        public static string Tr(string textId)
        {
            if (languagePack == null)
            {
                languagePack = AssetDatabase.LoadAssetAtPath<LanguagePack>($"{LANGUAGE_PACK_FOLDER}{languageType}.asset");
                Debug.Log($"{LANGUAGE_PACK_FOLDER}{languageType}.asset");
            }

            if (translateDictionary == null)
            {
                translateDictionary = JsonUtility.FromJson<JsonDictionary>(languagePack.data);
            }
            return translateDictionary[textId];
        }
    }
}

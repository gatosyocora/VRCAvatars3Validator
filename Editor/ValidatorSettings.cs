using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace VRCAvatars3Validator 
{
    public class ValidatorSettings : ScriptableObject
    {
        private const string SETTINGS_FILE_PATH = "Assets/VRCAvatars3Validator/Editor/Settings.asset";

        public bool validateOnUploadAvatar = true;

        public Dictionary<string, bool> validateRuleDictionary = new Dictionary<string, bool>();

        public void OnEnable()
        {
            if (validateRuleDictionary.Count <= 0)
            {
                var ruleNames = RuleManager.GetRuleNames();
    
                foreach (var ruleName in ruleNames)
                {
                    validateRuleDictionary.Add(ruleName, true);
                }
            }
        }

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
}
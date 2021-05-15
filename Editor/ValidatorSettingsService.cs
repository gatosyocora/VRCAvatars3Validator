using UnityEditor;

namespace VRCAvatars3Validator
{
    public class ValidatorSettingsService : Editor
    {
        private const string SETTINGS_FILE_PATH = "Assets/VRCAvatars3Validator/Editor/Settings.asset";

        private ValidatorSettings _validatorSettings;

        public void OnEnable()
        {
            if (_validatorSettings == null)
            {
                _validatorSettings = GetOrCreateSettings();
            }

            if (_validatorSettings.validateRuleDictionary.Count <= 0)
            {
                var ruleNames = RuleManager.GetRuleNames();

                foreach (var ruleName in ruleNames)
                {
                    _validatorSettings.validateRuleDictionary.Add(ruleName, true);
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
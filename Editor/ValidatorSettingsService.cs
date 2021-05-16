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
        }

        public static ValidatorSettings GetOrCreateSettings()
        {
            var settings = AssetDatabase.LoadAssetAtPath<ValidatorSettings>(SETTINGS_FILE_PATH);
            if (settings) return settings;

            return CreateSettings();
        }

        private static ValidatorSettings CreateSettings()
        {
            var settings = CreateInstance<ValidatorSettings>();

            settings.validateOnUploadAvatar = true;

            var ruleNames = RuleManager.GetRuleNames();
            foreach (var ruleName in ruleNames)
            {
                settings.validateRuleDictionary.Add(ruleName, true);
            }

            AssetDatabase.CreateAsset(settings, SETTINGS_FILE_PATH);
            AssetDatabase.SaveAssets();
            return settings;
        }
    }
}
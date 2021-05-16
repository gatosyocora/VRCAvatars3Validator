using System;
using System.Collections.Generic;
using System.Linq;
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
            if (settings)
            {
                FetchRules(settings);
                return settings;
            }

            return CreateSettings();
        }

        private static ValidatorSettings CreateSettings()
        {
            var settings = CreateInstance<ValidatorSettings>();

            settings.validateOnUploadAvatar = true;

            var filePaths = RuleManager.GetRuleFilePaths();
            foreach (var filePath in filePaths)
            {
                AddRule(settings, filePath);
            }

            AssetDatabase.CreateAsset(settings, SETTINGS_FILE_PATH);
            AssetDatabase.SaveAssets();
            return settings;
        }

        private static void FetchRules(ValidatorSettings settings = null)
        {
            if (settings == null)
            {
                settings = GetOrCreateSettings();
            }
            var currentRuleFilePaths = RuleManager.GetRuleFilePaths().ToArray();
            var settingsFilePaths = settings.rules.Select(rule => rule.FilePath).ToArray();

            // 現在の設定にないものを追加する
            foreach (var filePath in currentRuleFilePaths)
            {
                if (!settingsFilePaths.Contains(filePath))
                {
                    AddRule(settings, filePath);
                }
            }

            // 現在の設定に余分にあるものを削除する
            var deleteRuleIndices = new List<int>();
            foreach (var filePath in settingsFilePaths)
            {
                if (!currentRuleFilePaths.Contains(filePath))
                {
                    var index = Array.IndexOf(currentRuleFilePaths, filePath);
                    deleteRuleIndices.Add(index);
                }
            }

            foreach (int index in deleteRuleIndices.OrderByDescending(x => x))
            {
                settings.rules.RemoveAt(index);
            }
        }

        private static void AddRule(ValidatorSettings settings, string filePath)
            => settings.rules.Add(new RuleItem
            {
                Name = RuleManager.FilePath2RuleName(filePath),
                Enabled = true,
                FilePath = filePath,
            });
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VRCAvatars3Validator.Models;

namespace VRCAvatars3Validator.Utilities
{
    public class ValidatorSettingsUtility
    {
        private const string SETTINGS_FILE_PATH = "Assets/VRCAvatars3Validator/Editor/Settings.asset";

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
            var settings = ScriptableObject.CreateInstance<ValidatorSettings>();

            settings.validateOnUploadAvatar = true;

            var filePaths = RuleUtility.GetRuleFilePaths();
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
            var currentRuleFilePaths = RuleUtility.GetRuleFilePaths().ToArray();
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
        {
            var settingable = RuleUtility.FilePath2Settingable(filePath);
            object options = null;
            if (settingable != null)
            {
                options = settingable.Options;
            }
            settings.rules.Add(new RuleItem
            {
                Name = RuleUtility.FilePath2RuleName(filePath),
                Enabled = true,
                FilePath = filePath,
                Options = new RuleItemOptions(options),
            });
        }
    }
}
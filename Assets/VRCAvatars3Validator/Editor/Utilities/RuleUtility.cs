using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;

namespace VRCAvatars3Validator.Utilities
{
    public class RuleUtility
    {
        public const string RULES_FOLDER_PATH = "Assets/VRCAvatars3Validator/Editor/Rules";
        public const string IGNORE_RULE_NAME = "TemplateRule";

        public static IEnumerable<string> GetRuleFilePaths()
            => Directory.EnumerateFiles(RULES_FOLDER_PATH, "*.cs", SearchOption.AllDirectories)
                .Where(filePath => !Path.GetFileNameWithoutExtension(filePath).Equals(IGNORE_RULE_NAME));

        public static IEnumerable<IRule> GetRules()
            => GetRuleFilePaths()
                .Select((filePath, index) =>
                {
                    return FilePath2IRule(filePath);
                });

        public static IEnumerable<string> GetRuleNames()
            => GetRuleFilePaths()
                .Select(filePath => Path.GetFileNameWithoutExtension(filePath));

        public static IRule FilePath2IRule(string filePath)
        {
            var ruleAsset = AssetDatabase.LoadAssetAtPath<MonoScript>(filePath);
            var type = ruleAsset.GetClass();
            return Activator.CreateInstance(type) as IRule;
        }

        public static string FilePath2RuleName(string filePath)
            => Path.GetFileNameWithoutExtension(filePath);
    }
}
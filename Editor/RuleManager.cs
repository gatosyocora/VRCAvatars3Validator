using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;

namespace VRCAvatars3Validator
{
    public class RuleManager
    {
        public const string RULES_FOLDER_PATH = "Assets/VRCAvatars3Validator/Editor/Rules";
        public const string IGNORE_RULE_NAME = "TemplateRule";

        private static IEnumerable<string> GetRuleFilePaths()
            => Directory.EnumerateFiles(RULES_FOLDER_PATH, "*.cs", SearchOption.AllDirectories)
                .Where(filePath => !Path.GetFileNameWithoutExtension(filePath).Equals(IGNORE_RULE_NAME));

        public static IEnumerable<IRule> GetRules()
            => GetRuleFilePaths()
                .Select((filePath, index) =>
                {
                    var ruleAsset = AssetDatabase.LoadAssetAtPath<MonoScript>(filePath);
                    var type = ruleAsset.GetClass();
                    return Activator.CreateInstance(type) as IRule;
                });

        public static IEnumerable<string> GetRuleNames()
            => GetRuleFilePaths()
                .Select(filePath => Path.GetFileNameWithoutExtension(filePath));
    }
}
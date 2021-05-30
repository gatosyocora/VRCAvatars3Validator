using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VRCAvatars3Validator.Rules;

namespace VRCAvatars3Validator.Models
{
    public class ValidatorSettings : ScriptableObject
    {
        public bool validateOnUploadAvatar = true;
        public bool suspendUploadingByWarningMessage = true;

        public List<RuleItem> rules = new List<RuleItem>();

        public List<string> ignoreAnimationFileRegexs = HaveNoMissingAnimationPathRule.ignoreAnimationFileRegexsDefault.ToList();
    }
}
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace VRCAvatars3Validator.Models
{
    public class ValidatorSettings : ScriptableObject
    {
        public bool validateOnUploadAvatar = true;
        public bool suspendUploadingByWarningMessage = true;

        public LanguageType languageType = LanguageType.EN;

        public List<RuleItem> rules = new List<RuleItem>();
    }
}
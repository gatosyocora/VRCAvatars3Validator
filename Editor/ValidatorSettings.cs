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
        public bool validateOnUploadAvatar = true;

        public List<RuleItem> rules = new List<RuleItem>();
    }
}
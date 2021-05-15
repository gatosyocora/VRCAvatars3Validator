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

        public Dictionary<string, bool> validateRuleDictionary = new Dictionary<string, bool>();
    }
}
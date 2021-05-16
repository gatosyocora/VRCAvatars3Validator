using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRCAvatars3Validator
{
    [System.Serializable]
    public class RuleItem
    {
        public string Name { get; set; }
        public IRule Rule { get; set; }
        public bool Enabled { get; set; }
    }
}
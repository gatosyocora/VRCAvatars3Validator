using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace VRCAvatars3Validator
{
    [CreateAssetMenu(menuName = "VRCAvatars3Validator/RuleSet")]
    public class ValidationRules : ScriptableObject
    {
        [SerializeField]
        public MonoScript[] rules;
    }
}
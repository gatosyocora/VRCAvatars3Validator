using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;

namespace VRCAvatars3Validator
{
    public abstract class RuleBase : MonoScript
    {
        public string Id { get; private set; }

        public abstract string RuleSummary { get; }

        public RuleBase(string id)
        {
            Id = id;
        }

        public abstract IEnumerable<ValidateResult> Validate(VRCAvatarDescriptor avatar);
    }
}

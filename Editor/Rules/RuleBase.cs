using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRC.SDK3.Avatars.Components;

namespace VRCAvatars3Validator
{
    public abstract class RuleBase
    {
        public string Id { get; private set; }

        public abstract string RuleSummary { get; }

        public RuleBase(string id)
        {
            Id = id;
        }

        public abstract IEnumerable<Error> Validate(VRCAvatarDescriptor avatar);
    }
}

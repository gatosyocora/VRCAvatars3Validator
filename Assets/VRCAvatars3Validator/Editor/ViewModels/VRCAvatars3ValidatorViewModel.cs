using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using VRC.SDK3.Avatars.Components;
using VRCAvatars3Validator.Models;

namespace VRCAvatars3Validator.ViewModels
{
    public class VRCAvatars3ValidatorViewModel
    {
        public VRCAvatarDescriptor avatar { get; private set; }

        public Dictionary<int, IEnumerable<ValidateResult>> resultDictionary { get; private set; }

        public ValidatorSettings settings { get; private set; }

        public static string EDITOR_NAME = "VRCAvatars3Validator";

        public bool HasNeverValided = true;

        public void OnOpen()
        {
            if (settings == null)
            {
                settings = ValidatorSettingsService.GetOrCreateSettings();
            }

            if (!avatar && Selection.activeGameObject)
            {
                avatar = Selection.activeGameObject.GetComponent<VRCAvatarDescriptor>();
                Validate();
            }
        }

        public void OnSwitchAvatar(VRCAvatarDescriptor avatar)
        {
            this.avatar = avatar;
            HasNeverValided = true;
        }

        public void OnValidateClick()
        {
            Validate();
        }

        public void OnSelectClick(ValidateResult result)
        {
            FocusTarget(result);
        }

        public void OnAutoFixClick(ValidateResult result)
        {
            result.AutoFix();
        }

        public bool IsSelectionAvatar() => avatar == null;
        public bool ExistValidationResult() => resultDictionary != null && resultDictionary.Any();

        private void Validate()
        {
            resultDictionary = VRCAvatars3Validator.ValidateAvatars3(avatar, settings.rules);
            HasNeverValided = false;
        }

        private void FocusTarget(ValidateResult result)
        {
            Selection.activeObject = result.Target;
            EditorGUIUtility.PingObject(result.Target);
        }
    }
}
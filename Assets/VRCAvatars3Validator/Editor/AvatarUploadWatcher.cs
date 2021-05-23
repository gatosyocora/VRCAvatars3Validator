using System.Linq;
using System.Reflection;
using UnityEditor;
using VRC.SDK3.Avatars.Components;
using VRC.SDKBase.Editor;
using VRC.SDKBase.Editor.BuildPipeline;
using VRCAvatars3Validator.Models;
using VRCAvatars3Validator.Utilities;
using VRCAvatars3Validator.Views;

namespace VRCAvatars3Validator
{
    public class AvatarUploadWatcher : IVRCSDKBuildRequestedCallback
    {
        public int callbackOrder => -1;

        public bool OnBuildRequested(VRCSDKRequestedBuildType requestedBuildType)
        {
            var settings = ValidatorSettingsUtility.GetOrCreateSettings();

            if (!settings.validateOnUploadAvatar) return true;

            var type = typeof(VRCSdkControlPanelAvatarBuilder);
            var field = type.GetField("_selectedAvatar", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            var avatar = field.GetValue(null) as VRCAvatarDescriptor;
            if (avatar == null) return true;
            Selection.activeObject = avatar.gameObject;

            var resultDictionary = VRCAvatars3Validator.ValidateAvatars3(avatar, settings.rules);

            if (resultDictionary
                    .Any(result => result.Value.Any(
                        r => r.ResultType == ValidateResult.ValidateResultType.Error ||
                            r.ResultType == ValidateResult.ValidateResultType.Warning)))
            {
                VRCAvatars3ValidatorView.Open();
                return false;
            }

            return true;
        }
    }
}
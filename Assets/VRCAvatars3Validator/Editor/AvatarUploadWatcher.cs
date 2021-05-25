using System.Linq;
using System.Reflection;
using UnityEditor;
#if VRC_SDK_VRCSDK3
using VRCAvatarDescriptor = VRC.SDK3.Avatars.Components.VRCAvatarDescriptor;
using VRCSdkControlPanelAvatarBuilder = VRC.SDKBase.Editor.VRCSdkControlPanelAvatarBuilder;
using IVRCSDKBuildRequestedCallback = VRC.SDKBase.Editor.BuildPipeline.IVRCSDKBuildRequestedCallback;
using VRCSDKRequestedBuildType = VRC.SDKBase.Editor.BuildPipeline.VRCSDKRequestedBuildType;
#else
using VRCAvatarDescriptor = VRCAvatars3Validator.Mocks.VRCAvatarDescriptorMock;
using VRCSdkControlPanelAvatarBuilder = VRCAvatars3Validator.Mocks.VRCSdkControlPanelAvatarBuilderMock;
using IVRCSDKBuildRequestedCallback = VRCAvatars3Validator.Mocks.IVRCSDKBuildRequestedCallbackMock;
using VRCSDKRequestedBuildType = VRCAvatars3Validator.Mocks.VRCSDKRequestedBuildTypeMock;
#endif
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
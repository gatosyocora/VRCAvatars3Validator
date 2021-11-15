using UnityEngine;
using VRCAvatars3Validator.Views;

namespace VRCAvatars3Validator.Models
{
    [CreateAssetMenu(menuName = "VRCAvatars3Validator/LanguagePack")]
    public class LanguagePack : ScriptableObject
    {
        [LanguagePack]
        public string data =
        "{\n" +
            "\"dictionary\":[\n" +
            " {\"key\":\"ExpressionsSubMenuRule_summary\",\"value\":\"Exists unset SubMenu\"}\n" +
            ",{\"key\":\"ControllerLayerWeightRule_summary\",\"value\":\"Have weight 0 layer\"}\n" +
            ",{\"key\":\"HaveTransformAnimationRule_summary\",\"value\":\"Have other than Transform Animation in other than FX\"}\n" +
            ",{\"key\":\"HaveExParamsInControllersRule_summary\",\"value\":\"Missing Expression Parameter\"}\n" +
            ",{\"key\":\"HaveNoMissingAnimationPathRule_summary\",\"value\":\"Exists missing path in AnimationClips\"}\n" +
            ",{\"key\":\"HaveEmptyStateRule_summary\",\"value\":\"Exists state missing AnimationClip in AnimatorControllers\"}\n" +
            ",{\"key\":\"language\",\"value\":\"Language\"}\n" +
            ",{\"key\":\"ValidateOnUploadAvatar\",\"value\":\"Validate OnUploadAvatar\"}\n" +
            ",{\"key\":\"SuspendWarning\",\"value\":\"Suspend uploading by warning message\"}\n" +
            ",{\"key\":\"EnableRules\",\"value\":\"Enable Rules\"}\n" +
            ",{\"key\":\"Settings\",\"value\":\"Settings\"}\n" +
            ",{\"key\":\"Avatar\",\"value\":\"Avatar\"}\n" +
            ",{\"key\":\"Rules\",\"value\":\"Rules\"}\n" +
            ",{\"key\":\"Validate\",\"value\":\"Validate\"}\n" +
            ",{\"key\":\"Errors\",\"value\":\"Errors\"}\n" +
            ",{\"key\":\"NoError\",\"value\":\"No Error\"}\n" +
            ",{\"key\":\"Select\",\"value\":\"Select\"}\n" +
            ",{\"key\":\"AutoFix\",\"value\":\"AutoFix\"}\n" +
            ",{\"key\":\"ControllerLayerWeightRule_result\",\"value\":\"`<1>` Layer in <2> is weight 0.\"}\n" +
            ",{\"key\":\"HaveExParamsInControllersRule_result\",\"value\":\"<1> is not found in AnimatorControllers\"}\n" +
            ",{\"key\":\"ExpressionsSubMenuRule_result\",\"value\":\"`<1>` exists unset SubMenu.\"}\n" +
            ",{\"key\":\"HaveTransformAnimationRule_result\",\"value\":\"<1> have key changed other than Transform\"}\n" +
            ",{\"key\":\"HaveNoMissingAnimationPathRule_result\",\"value\":\"`<1>` have missing path. (<2>)\"}\n" +
            ",{\"key\":\"HaveNoMissingAnimationPathRule_options_ignoreFileNameRegex\",\"value\":\"Ignore animation file name pattern\"}\n" +
            ",{\"key\":\"HaveEmptyStateRule_result\",\"value\":\"`<1>` has not animation clip in <2> layer of <3>.\"}\n" +
            ",{\"key\":\"Add\",\"value\":\"Add\"}\n" +
            ",{\"key\":\"Reset\",\"value\":\"Reset\"}\n" +
            ",{\"key\":\"Save\",\"value\":\"Save\"}\n" +
            "]\n" +
        "}";
    }
}

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
            "]\n" +
        "}";
    }
}

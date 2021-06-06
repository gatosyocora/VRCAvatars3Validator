using VRCAvatars3Validator.Models;

namespace VRCAvatars3Validator
{
    public interface Settingable
    {
        void OnGUI(ValidatorSettings settings, RuleItemOptions options);
    }
}

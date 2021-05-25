using VRCSDKRequestedBuildType = VRCAvatars3Validator.Mocks.VRCSDKRequestedBuildTypeMock;

namespace VRCAvatars3Validator.Mocks
{
    public interface IVRCSDKBuildRequestedCallbackMock
    {
        int callbackOrder { get; }
        bool OnBuildRequested(VRCSDKRequestedBuildType requestedBuildType);
    }
}
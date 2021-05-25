using System.Collections.Generic;
using UnityEngine;
using VRCExpressionParameters = VRCAvatars3Validator.Mocks.VRCExpressionParametersMock;
using VRCExpressionsMenu = VRCAvatars3Validator.Mocks.VRCExpressionsMenuMock;

namespace VRCAvatars3Validator.Mocks
{
    public class VRCAvatarDescriptorMock : MonoBehaviour
    {
        public bool customExpressions;
        public bool autoLocomotion;
        public bool autoFootsteps;
        public ScriptableObject AnimationPreset;
        public CustomAnimLayer[] specialAnimationLayers;
        public CustomAnimLayer[] baseAnimationLayers;
        [HideInInspector]
        public List<DebugHash> animationHashSet;
        public CustomEyeLookSettings customEyeLookSettings;
        public bool enableEyeLook;
        public VRCExpressionParameters expressionParameters;
        public VRCExpressionsMenu expressionsMenu;
        public bool customizeAnimationLayers;

        //public VRCAvatarDescriptor();

        //public VRCExpressionParameters.Parameter GetExpressionParameter(int index);
        //public int GetExpressionParameterCount();

        public enum AnimLayerType
        {
            Base = 0,
            SpecialIK = 1,
            Additive = 2,
            Gesture = 3,
            Action = 4,
            FX = 5,
            Sitting = 6,
            TPose = 7,
            IKPose = 8
        }
        public enum EyelidType
        {
            None = 0,
            Bones = 1,
            Blendshapes = 2
        }

        public struct CustomEyeLookSettings
        {
            public EyeMovements eyeMovement;
            public int[] eyelidsBlendshapes;
            public SkinnedMeshRenderer eyelidsSkinnedMesh;
            public EyelidRotations eyelidsLookingDown;
            public EyelidRotations eyelidsLookingUp;
            public EyelidRotations eyelidsClosed;
            public EyelidRotations eyelidsDefault;
            public Transform lowerRightEyelid;
            public Transform lowerLeftEyelid;
            public Transform upperRightEyelid;
            public EyelidType eyelidType;
            public EyeRotations eyesLookingRight;
            public EyeRotations eyesLookingLeft;
            public EyeRotations eyesLookingDown;
            public EyeRotations eyesLookingUp;
            public EyeRotations eyesLookingStraight;
            public Transform rightEye;
            public Transform leftEye;
            public Transform upperLeftEyelid;

            public class EyeRotations
            {
                public bool linked;
                public Quaternion left;
                public Quaternion right;

                //public EyeRotations();
            }
            public class EyeMovements
            {
                public float confidence;
                public float excitement;

                //public EyeMovements();
            }
            public class EyelidRotations
            {
                public EyeRotations upper;
                public EyeRotations lower;

                //public EyelidRotations();
            }
        }
        public struct CustomAnimLayer
        {
            public bool isEnabled;
            public AnimLayerType type;
            public RuntimeAnimatorController animatorController;
            public AvatarMask mask;
            public bool isDefault;
        }
        public struct DebugHash
        {
            public int hash;
            public string name;
        }
    }
}
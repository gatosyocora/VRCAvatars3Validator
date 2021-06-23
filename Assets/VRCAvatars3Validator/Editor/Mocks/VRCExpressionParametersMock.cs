using UnityEngine;

namespace VRCAvatars3Validator.Mocks
{
    public class VRCExpressionParametersMock : ScriptableObject
    {
        public const int MAX_PARAMETERS = 16;
        public Parameter[] parameters;

        //public VRCExpressionParameters();

        //public Parameter FindParameter(string name);
        //public Parameter GetParameter(int index);

        public enum ValueType
        {
            Int = 0,
            Float = 1,
            Bool = 2
        }

        public class Parameter
        {
            public string name;
            public ValueType valueType;

            //public Parameter();
        }
    }
}
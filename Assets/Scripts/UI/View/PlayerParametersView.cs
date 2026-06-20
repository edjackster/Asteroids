using MVVM;
using TMPro;
using UnityEngine;

namespace UI.View
{
    public class PlayerParametersView : MonoBehaviour
    {
        [Data("ParameterPositionX")]
        public TMP_Text PositionX;
    
        [Data("ParameterPositionY")]
        public TMP_Text PositionY;
    
        [Data("ParameterVelocity")]
        public TMP_Text Velocity;
    
        [Data("ParameterRotation")]
        public TMP_Text Rotation;
    }
}
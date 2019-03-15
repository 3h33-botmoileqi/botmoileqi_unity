using UnityEngine;
using UnityEditor;
using Crosstales.RTVoice.EditorUtil;

namespace Crosstales.RTVoice.AWSPolly
{
    /// <summary>Custom editor for the 'VoiceProviderAWS'-class.</summary>
    [CustomEditor(typeof(VoiceProviderAWS))]
    public class VoiceProviderAWSEditor : Editor
    {

        #region Variables

        private VoiceProviderAWS script;

        private string cognitoCredentials = string.Empty;
        private Endpoint endpoint = Endpoint.EUCentral1; //USEast1

        #endregion


        #region Editor methods

        public void OnEnable()
        {
            script = (VoiceProviderAWS)target;
        }

        public override void OnInspectorGUI()
        {
            cognitoCredentials = EditorGUILayout.PasswordField(new GUIContent("Cognito Credentials", "Cognito credentials for AWS Polly."), script.CognitoCredentials);
            if (!cognitoCredentials.Equals(script.CognitoCredentials))
            {
                serializedObject.FindProperty("CognitoCredentials").stringValue = cognitoCredentials;
                serializedObject.ApplyModifiedProperties();
            }

            endpoint = (Endpoint)EditorGUILayout.EnumPopup(new GUIContent("Endpoint", "Active endpoint for AWS Polly."), script.Endpoint);
            if (endpoint != script.Endpoint)
            {
                serializedObject.FindProperty("Endpoint").enumValueIndex = (int)endpoint;
                serializedObject.ApplyModifiedProperties();
            }

            //DrawDefaultInspector();

            //EditorHelper.SeparatorUI();

            if (script.isActiveAndEnabled)
            {
                if (!string.IsNullOrEmpty(script.CognitoCredentials))
                {
                    //TODO add stuff if needed
                }
                else
                {
                    EditorHelper.SeparatorUI();
                    EditorGUILayout.HelpBox("Please add the 'Cognito Credentials'!", MessageType.Warning);
                }
            }
            else
            {
                EditorHelper.SeparatorUI();
                EditorGUILayout.HelpBox("Script is disabled!", MessageType.Info);
            }
        }

        #endregion

    }
}
// © 2018-2019 crosstales LLC (https://www.crosstales.com)
using UnityEngine;
using UnityEditor;

namespace Crosstales.RTVoice.AWSPolly
{
    /// <summary>Editor component for for adding the prefabs from 'AWS Polly' in the "Hierarchy"-menu.</summary>
    public class VoiceProviderAWSGameObject : MonoBehaviour
    {
        [MenuItem("GameObject/" + Util.Constants.ASSET_NAME + "/3rd party/AWS Polly/VoiceProviderAWS", false, EditorUtil.EditorHelper.GO_ID + 20)]
        private static void AddVoiceProviderAWS()
        {
            PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets" + EditorUtil.EditorConfig.ASSET_PATH + "3rd party/AWS Polly/Prefabs/AWS Polly.prefab", typeof(GameObject)));
        }
    }
}
// © 2018-2019 crosstales LLC (https://www.crosstales.com)

using UnityEngine;
using UnityEditor;

namespace Crosstales.RTVoice.AWSPolly
{
    /// <summary>Editor component for for adding the prefabs from 'AWS Polly' in the "Tools"-menu.</summary>
    public class VoiceProviderAWSMenu
    {
        [MenuItem("Tools/" + Util.Constants.ASSET_NAME + "/Prefabs/3rd party/AWS Polly/VoiceProviderAWS", false, EditorUtil.EditorHelper.MENU_ID + 220)]
        private static void AddVoiceProviderAWS()
        {
            PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets" + EditorUtil.EditorConfig.ASSET_PATH + "3rd party/AWS Polly/Prefabs/AWS Polly.prefab", typeof(GameObject)));
        }
    }
}
// © 2018-2019 crosstales LLC (https://www.crosstales.com)
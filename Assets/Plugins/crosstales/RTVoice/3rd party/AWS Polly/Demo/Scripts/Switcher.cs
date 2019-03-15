using UnityEngine;

namespace Crosstales.RTVoice.AWSPolly
{
    /// <summary>Simple switcher to test the functionality of the AWS Polly provider.</summary>
    [HelpURL("https://www.crosstales.com/media/data/assets/rtvoice/api/class_crosstales_1_1_r_t_voice_1_1_speaker.html")] //TODO add correct URL!
    public class Switcher : MonoBehaviour
    {
        #region MonoBehaviour methods

        public void OnEnable()
        {
            // Subscribe event listeners
            Speaker.OnVoicesReady += onVoicesReady;
            Speaker.OnProviderChange += onProviderChange;

        }

        public void OnDisable()
        {
            // Unsubscribe event listeners
            Speaker.OnVoicesReady -= onVoicesReady;
            Speaker.OnProviderChange -= onProviderChange;
        }

        #endregion


        #region Public methods

        public void Switch()
        {
            Speaker.isCustomMode = !Speaker.isCustomMode;
        }

        #endregion


        #region Callbacks

        private void onVoicesReady()
        {
            Debug.Log("onVoicesReady: " + Speaker.Voices.Count);
        }

        private void onProviderChange(string provider)
        {
            Debug.Log("onProviderChange: " + provider);
        }

        #endregion
    }
}
// © 2018-2019 crosstales LLC (https://www.crosstales.com)
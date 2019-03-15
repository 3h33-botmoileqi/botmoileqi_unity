using UnityEngine;
using UnityEngine.UI;

namespace Crosstales.RTVoice.SALSA
{
    [HelpURL("https://www.crosstales.com/media/data/assets/rtvoice/api/class_crosstales_1_1_r_t_voice_1_1_s_a_l_s_a_1_1_speak.html")]
    public class SpeakSimple : MonoBehaviour
    {

        #region Variables

        [Tooltip("Origin AudioSource.")]
        public AudioSource Source;

        [Tooltip("Field with the text to speak.")]
        public InputField EnterText;

        [Tooltip("Slider for the speak rate.")]
        public Slider RateSlider;

        [Tooltip("Slider for the speak pitch (mobile only).")]
        public Slider PitchSlider;

        #endregion


        #region Public methods

        public void Silence()
        {
            Speaker.Silence();
        }

        public void Talk()
        {
            Speaker.Silence();

            if (EnterText != null && RateSlider != null && PitchSlider != null)
                Speaker.Speak(EnterText.text, Source, Speaker.VoiceForCulture("en"), true, RateSlider.value, PitchSlider.value, 1f);
        }

        #endregion

    }
}
// © 2015-2019 crosstales LLC (https://www.crosstales.com)
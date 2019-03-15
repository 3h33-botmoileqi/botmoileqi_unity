using UnityEngine;
using UnityEngine.UI;

namespace Crosstales.RTVoice.SALSA
{
    [HelpURL("https://www.crosstales.com/media/data/assets/rtvoice/api/class_crosstales_1_1_r_t_voice_1_1_s_a_l_s_a_1_1_speak.html")]
    public class Speak : MonoBehaviour
    {

        #region Variables

        [Tooltip("Origin AudioSource.")]
        public AudioSource Source;

        [Tooltip("SALSA scene object.")]
        public CrazyMinnow.SALSA.Salsa3D Salsa;

        [Tooltip("Field with the text to speak.")]
        public InputField EnterText;

        [Tooltip("Slider for the speak rate.")]
        public Slider RateSlider;

        [Tooltip("Slider for the speak pitch (mobile only).")]
        public Slider PitchSlider;

        #endregion


        #region MonoBehaviour methods

        public void Start()
        {
            if (EnterText != null)
                EnterText.text = "Hi there! This is RTVoice combined with SALSA. It's really awesome!";
        }

        public void OnEnable()
        {
            // Subscribe event listeners
            Speaker.OnSpeakAudioGenerationComplete += speakAudioGenerationCompleteMethod;

        }

        public void OnDisable()
        {
            // Unsubscribe event listeners
            Speaker.OnSpeakAudioGenerationComplete -= speakAudioGenerationCompleteMethod;
        }

        #endregion


        #region Public methods

        public void Talk()
        {
            Speaker.Silence();

            if (EnterText != null && RateSlider != null && PitchSlider != null)
                Speaker.Speak(EnterText.text, Source, Speaker.VoiceForCulture("en"), false, RateSlider.value, PitchSlider.value, 1f);
        }

        #endregion


        #region Callback

        private void speakAudioGenerationCompleteMethod(Model.Wrapper wrapper)
        {
            if (Salsa != null)
            {
                Salsa.audioClip = Source.clip;

                Salsa.Play();
            }
        }

        #endregion
    }
}
// © 2015-2019 crosstales LLC (https://www.crosstales.com)
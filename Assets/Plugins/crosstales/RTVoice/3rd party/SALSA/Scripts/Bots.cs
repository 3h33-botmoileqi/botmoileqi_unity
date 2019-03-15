using UnityEngine;

namespace Crosstales.RTVoice.SALSA
{
    /// <summary>This is class for conversations between two SALSA-Bots.</summary>
    public class Bots : MonoBehaviour
    {
        [Tooltip("Origin AudioSource from BotA.")]
        public AudioSource SourceA;

        [Tooltip("Origin AudioSource from BotB.")]
        public AudioSource SourceB;

        [Tooltip("All conversations from BotA.")]
        public string[] ConverstationsA;

        [Tooltip("All conversations from BotB.")]
        public string[] ConverstationsB;

        private bool talking = false;
        private int converstaionAIndex = -1;
        private int converstaionBIndex = -1;
        private string id = string.Empty;

        private bool isBotBSpeaking = true;

        public void OnEnable()
        {
            // Subscribe event listeners
            Speaker.OnSpeakComplete += speakCompleteMethod;

        }

        public void OnDisable()
        {
            // Unsubscribe event listeners
            Speaker.OnSpeakComplete -= speakCompleteMethod;
        }

        public void Update()
        {
            //if (!talking && !Salsa.isTalking && !SalsaPartner.isTalking)
            if (!talking && !Util.Helper.hasActiveClip(SourceA) && !Util.Helper.hasActiveClip(SourceB))
            {
                talking = true;

                if (isBotBSpeaking)
                {
                    if (converstaionAIndex + 1 < ConverstationsA.Length)
                    {
                        converstaionAIndex++;

                        id = Speaker.Speak(ConverstationsA[converstaionAIndex], SourceA, Speaker.VoiceForCulture("en"), true); //speak next conversation
                    }
                    else
                    {
                        Debug.Log("BotA finished talking");
                    }

                    isBotBSpeaking = false;
                }
                else
                {
                    if (converstaionBIndex + 1 < ConverstationsB.Length)
                    {
                        converstaionBIndex++;

                        id = Speaker.Speak(ConverstationsB[converstaionBIndex], SourceB, Speaker.VoiceForCulture("en"), true); //speak next partner conversation
                    }
                    else
                    {
                        Debug.Log("BotB finished talking");
                    }

                    isBotBSpeaking = true;
                }
            }
        }

        private void speakCompleteMethod(Model.Wrapper wrapper)
        {
            if (id.Equals(wrapper.Uid)) //is it our Speak-call?
            {
                Debug.Log("Generated audio: " + wrapper); //contains also the text and much more

                talking = false;
            }
        }
    }
}
// © 2017-2019 crosstales LLC (https://www.crosstales.com)
using UnityEngine;
using System.Collections;
using System.Linq;

namespace Crosstales.RTVoice.AWSPolly
{
    /// <summary>AWS Polly voice provider.</summary>
    [HelpURL("https://www.crosstales.com/media/data/assets/rtvoice/api/class_crosstales_1_1_r_t_voice_1_1_a_w_s_polly_1_1_voice_provider_a_w_s.html")]
    public class VoiceProviderAWS : Provider.BaseCustomVoiceProvider
    {
        #region Variables

        public string CognitoCredentials = string.Empty;
        public Endpoint Endpoint = Endpoint.EUCentral1;

        private Amazon.Polly.AmazonPollyClient client;

        private static System.Collections.Generic.Dictionary<Endpoint, Amazon.RegionEndpoint> endpoints;

        private string lastCredentials = string.Empty;

        #endregion


        #region Properties

        public override string AudioFileExtension
        {
            get
            {
                return ".ogg";
            }
        }

        public override AudioType AudioFileType
        {
            get
            {
                return AudioType.OGGVORBIS;
            }
        }

        public override string DefaultVoiceName
        {
            get
            {
                return "Matthew";
            }
        }

        public override bool isWorkingInEditor
        {
            get
            {
                return false;
            }
        }

        public override bool isPlatformSupported
        {
            get
            {
                return !Util.Helper.isWebPlatform; //TODO still true?
            }
        }

        public override int MaxTextLength
        {
            get
            {
                return 32000; //TODO Test
            }
        }

        public override bool isSpeakNativeSupported
        {
            get
            {
                return false;
            }
        }

        public override bool isSpeakSupported
        {
            get
            {
                return true;
            }
        }

        public override bool isSSMLSupported
        {
            get
            {
                return true;
            }
        }

        public Amazon.RegionEndpoint getAWSEndpoint
        {
            get
            {
                if (endpoints == null)
                {
                    endpoints = new System.Collections.Generic.Dictionary<Endpoint, Amazon.RegionEndpoint>();
                    endpoints.Add(Endpoint.APNortheast1, Amazon.RegionEndpoint.APNortheast1);
                    endpoints.Add(Endpoint.APNortheast2, Amazon.RegionEndpoint.APNortheast2);
                    endpoints.Add(Endpoint.APSouth1, Amazon.RegionEndpoint.APSouth1);
                    endpoints.Add(Endpoint.APSoutheast1, Amazon.RegionEndpoint.APSoutheast1);
                    endpoints.Add(Endpoint.APSoutheast2, Amazon.RegionEndpoint.APSoutheast2);
                    endpoints.Add(Endpoint.CACentral1, Amazon.RegionEndpoint.CACentral1);
                    endpoints.Add(Endpoint.CNNorth1, Amazon.RegionEndpoint.CNNorth1);
                    endpoints.Add(Endpoint.EUCentral1, Amazon.RegionEndpoint.EUCentral1);
                    endpoints.Add(Endpoint.EUWest1, Amazon.RegionEndpoint.EUWest1);
                    endpoints.Add(Endpoint.EUWest2, Amazon.RegionEndpoint.EUWest2);
                    endpoints.Add(Endpoint.SAEast1, Amazon.RegionEndpoint.SAEast1);
                    endpoints.Add(Endpoint.USEast1, Amazon.RegionEndpoint.USEast1);
                    endpoints.Add(Endpoint.USEast2, Amazon.RegionEndpoint.USEast2);
                    endpoints.Add(Endpoint.USGovCloudWest1, Amazon.RegionEndpoint.USGovCloudWest1);
                    endpoints.Add(Endpoint.USWest1, Amazon.RegionEndpoint.USWest1);
                    endpoints.Add(Endpoint.USWest2, Amazon.RegionEndpoint.USWest2);
                }

                return endpoints[Endpoint];
            }
        }

        #endregion


        #region MonoBehaviour methods

        public void Awake()
        {
            Amazon.UnityInitializer.AttachToGameObject(gameObject);
            Amazon.AWSConfigs.HttpClient = Amazon.AWSConfigs.HttpClientOption.UnityWebRequest;
        }

        #endregion


        #region Implemented methods

        public override void Load()
        {
            System.Collections.Generic.List<Model.Voice> voices = new System.Collections.Generic.List<Model.Voice> {
                        new Model.Voice("Zhiyu", "Chinese, Mandarin (cmn-CN)", Model.Enum.Gender.FEMALE, "Adult", "zh"),
                        new Model.Voice("Mads", "Danish (da-DK)", Model.Enum.Gender.MALE, "Adult", "da-DK"),
                        new Model.Voice("Naja", "Danish (da-DK)", Model.Enum.Gender.FEMALE, "Adult", "da-DK"),
                        new Model.Voice("Ruben", "Dutch (nl-NL)", Model.Enum.Gender.MALE, "Adult", "nl-NL"),
                        new Model.Voice("Lotte", "Dutch (nl-NL)", Model.Enum.Gender.FEMALE, "Adult", "nl-NL"),
                        new Model.Voice("Russell", "English, Australian (en-AU)", Model.Enum.Gender.MALE, "Adult", "en-AU"),
                        new Model.Voice("Nicole", "English, Australian (en-AU)", Model.Enum.Gender.FEMALE, "Adult", "en-AU"),
                        new Model.Voice("Brian", "English, British (en-GB)", Model.Enum.Gender.MALE, "Adult", "en-GB"),
                        new Model.Voice("Amy", "English, British (en-GB)", Model.Enum.Gender.FEMALE, "Adult", "en-GB"),
                        new Model.Voice("Emma", "English, British (en-GB)", Model.Enum.Gender.FEMALE, "Adult", "en-GB"),
                        new Model.Voice("Aditi", "English, Indian (en-IN)", Model.Enum.Gender.FEMALE, "Adult", "en-IN"),
                        new Model.Voice("Raveena", "English, Indian (en-IN)", Model.Enum.Gender.FEMALE, "Adult", "en-IN"),
                        new Model.Voice("Joey", "English, US (en-US)", Model.Enum.Gender.MALE, "Adult", "en-US"),
                        new Model.Voice("Justin", "English, US (en-US)", Model.Enum.Gender.MALE, "Adult", "en-US"),
                        new Model.Voice("Matthew", "English, US (en-US)", Model.Enum.Gender.MALE, "Adult", "en-US"),
                        new Model.Voice("Ivy", "English, US (en-US)", Model.Enum.Gender.FEMALE, "Adult", "en-US"),
                        new Model.Voice("Joanna", "English, US (en-US)", Model.Enum.Gender.FEMALE, "Adult", "en-US"),
                        new Model.Voice("Kendra", "English, US (en-US)", Model.Enum.Gender.FEMALE, "Adult", "en-US"),
                        new Model.Voice("Kimberly", "English, US (en-US)", Model.Enum.Gender.FEMALE, "Adult", "en-US"),
                        new Model.Voice("Salli", "English, US (en-US)", Model.Enum.Gender.FEMALE, "Adult", "en-US"),
                        new Model.Voice("Geraint", "English, Welsh (en-GB-WLS)", Model.Enum.Gender.MALE, "Adult", "en-GB-WLS"),
                        new Model.Voice("Mathieu", "French (fr-FR)", Model.Enum.Gender.MALE, "Adult", "fr-FR"),
                        new Model.Voice("Celine", "French (fr-FR)", Model.Enum.Gender.FEMALE, "Adult", "fr-FR"),
                        new Model.Voice("Lea", "French (fr-FR)", Model.Enum.Gender.FEMALE, "Adult", "fr-FR"),
                        new Model.Voice("Chantal", "French (fr-CA)", Model.Enum.Gender.FEMALE, "Adult", "fr-CA"),
                        new Model.Voice("Hans", "German (de-DE)", Model.Enum.Gender.MALE, "Adult", "de-DE"),
                        new Model.Voice("Marlene", "German (de-DE)", Model.Enum.Gender.FEMALE, "Adult", "de-DE"),
                        new Model.Voice("Vicki", "German (de-DE)", Model.Enum.Gender.FEMALE, "Adult", "de-DE"),
                        new Model.Voice("Karl", "Icelandic (is-IS)", Model.Enum.Gender.MALE, "Adult", "is-IS"),
                        new Model.Voice("Dora", "Icelandic (is-IS)", Model.Enum.Gender.FEMALE, "Adult", "is-IS"),
                        new Model.Voice("Giorgio", "Italian (it-IT)", Model.Enum.Gender.MALE, "Adult", "it-IT"),
                        new Model.Voice("Carla", "Italian (it-IT)", Model.Enum.Gender.FEMALE, "Adult", "it-IT"),
                        new Model.Voice("Takumi", "Japanese (ja-JP)", Model.Enum.Gender.MALE, "Adult", "ja-JP"),
                        new Model.Voice("Mizuki", "Japanese (ja-JP)", Model.Enum.Gender.FEMALE, "Adult", "ja-JP"),
                        new Model.Voice("Seoyeon", "Korean (ko-KR)", Model.Enum.Gender.FEMALE, "Adult", "ko-KR"),
                        new Model.Voice("Seoyeon", "Norwegian (no-NO)", Model.Enum.Gender.FEMALE, "Adult", "ko-KR"),
                        new Model.Voice("Jacek", "Polish (pl-PL)", Model.Enum.Gender.MALE, "Adult", "pl-PL"),
                        new Model.Voice("Jan", "Polish (pl-PL)", Model.Enum.Gender.MALE, "Adult", "pl-PL"),
                        new Model.Voice("Ewa", "Polish (pl-PL)", Model.Enum.Gender.FEMALE, "Adult", "pl-PL"),
                        new Model.Voice("Maja", "Polish (pl-PL)", Model.Enum.Gender.FEMALE, "Adult", "pl-PL"),
                        new Model.Voice("Ricardo", "Portuguese, Brazilian (pt-BR)", Model.Enum.Gender.MALE, "Adult", "pt-BR"),
                        new Model.Voice("Vitoria", "Portuguese, Brazilian (pt-BR)", Model.Enum.Gender.FEMALE, "Adult", "pt-BR"),
                        new Model.Voice("Cristiano", "Portuguese, European (pt-PT)", Model.Enum.Gender.MALE, "Adult", "pt-PT"),
                        new Model.Voice("Ines", "Portuguese, European (pt-PT)", Model.Enum.Gender.FEMALE, "Adult", "pt-PT"),
                        new Model.Voice("Carmen", "Romanian (ro-RO)", Model.Enum.Gender.FEMALE, "Adult", "ro-RO"),
                        new Model.Voice("Maxim", "Russian (ru-RU)", Model.Enum.Gender.MALE, "Adult", "ru-RU"),
                        new Model.Voice("Tatyana", "Russian (ru-RU)", Model.Enum.Gender.FEMALE, "Adult", "ru-RU"),
                        new Model.Voice("Enrique", "Spanish, European (es-ES)", Model.Enum.Gender.MALE, "Adult", "es-ES"),
                        new Model.Voice("Conchita", "Spanish, European (es-ES)", Model.Enum.Gender.FEMALE, "Adult", "es-ES"),
                        new Model.Voice("Miguel", "Spanish, US (es-US)", Model.Enum.Gender.MALE, "Adult", "es-US"),
                        new Model.Voice("Penelope", "Spanish, US (es-US)", Model.Enum.Gender.FEMALE, "Adult", "es-US"),
                        new Model.Voice("Astrid", "Swedish (sv-SE)", Model.Enum.Gender.FEMALE, "Adult", "sv-SE"),
                        new Model.Voice("Filiz", "Turkish (tr-TR)", Model.Enum.Gender.FEMALE, "Adult", "tr-TR"),
                        new Model.Voice("Gwyneth", "Welsh (cy-GB)", Model.Enum.Gender.FEMALE, "Adult", "cy-GB"),
                    };

            cachedVoices = voices.OrderBy(s => s.Name).ToList();

            onVoicesReady();
        }

        public override IEnumerator Generate(Model.Wrapper wrapper)
        {
            //Debug.Log("Generate: " + wrapper);
            connectToAmazon();

            if (client == null)
            {
                Debug.LogWarning("'client' is null! Did you add the correct Cognito credentials?");
            }
            else
            {

                if (wrapper == null)
                {
                    Debug.LogWarning("'wrapper' is null!");
                }
                else
                {
                    if (string.IsNullOrEmpty(wrapper.Text))
                    {
                        Debug.LogWarning("'wrapper.Text' is null or empty!");
                    }
                    else
                    {
                        if (!Util.Helper.isInternetAvailable)
                        {
                            string errorMessage = "Internet is not available - can't use AWS Polly right now!";
                            Debug.LogError(errorMessage);
                            onErrorInfo(wrapper, errorMessage);
                        }
                        else
                        {
                            yield return null; //return to the main process (uid)

                            string voiceCulture = getVoiceCulture(wrapper);
                            string voiceName = getVoiceName(wrapper);

                            System.Text.StringBuilder sbXML = new System.Text.StringBuilder();

                            //SSML
                            sbXML.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
                            sbXML.Append("<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\" xml:lang=\"");
                            sbXML.Append(voiceCulture);
                            sbXML.Append("\"");
                            //sbXML.Append (" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"");
                            //sbXML.Append (" xsi:schemaLocation=\"http://www.w3.org/2001/10/synthesis http://www.w3.org/TR/speech-synthesis/synthesis.xsd\"");
                            sbXML.Append(">");

                            sbXML.Append(prepareProsody(wrapper));

                            sbXML.Append("</speak>");

                            Amazon.Polly.Model.SynthesizeSpeechRequest synthesizeSpeechPresignRequest = new Amazon.Polly.Model.SynthesizeSpeechRequest();
                            synthesizeSpeechPresignRequest.Text = sbXML.ToString();
                            synthesizeSpeechPresignRequest.TextType = Amazon.Polly.TextType.Ssml;
                            synthesizeSpeechPresignRequest.VoiceId = voiceName;
                            synthesizeSpeechPresignRequest.OutputFormat = Amazon.Polly.OutputFormat.Ogg_vorbis;

                            silence = false;

                            onSpeakAudioGenerationStart(wrapper);

                            string outputFile = getOutputFile(wrapper.Uid);
                            bool success = false;
                            bool requestFinished = false;

                            client.SynthesizeSpeechAsync(synthesizeSpeechPresignRequest, (responseObject) =>
                            {
                                if (responseObject.Exception == null)
                                {
                                    using (System.IO.FileStream fileStream = System.IO.File.Create(outputFile))
                                    {
                                        copyTo(responseObject.Response.AudioStream, fileStream);
                                    }

                                    success = true;
                                }
                                else
                                {
                                    string errorMessage = "Could not generate the text: " + wrapper + System.Environment.NewLine + "Error: " + responseObject.Exception;
                                    Debug.LogError(errorMessage);
                                    onErrorInfo(wrapper, errorMessage);
                                }

                                requestFinished = true;
                            });

                            do
                            {
                                yield return null;
                            } while (!requestFinished);

                            if (success)
                            {
                                processAudioFile(wrapper, outputFile);
                            }
                        }
                    }
                }
            }
        }

        public override IEnumerator SpeakNative(Model.Wrapper wrapper)
        {
            yield return speak(wrapper, true);
        }

        public override IEnumerator Speak(Model.Wrapper wrapper)
        {
            yield return speak(wrapper, false);
        }

        #endregion


        #region Private methods

        private void connectToAmazon()
        {
            if (client == null || !lastCredentials.Equals(CognitoCredentials))
            {
                if (string.IsNullOrEmpty(CognitoCredentials))
                {
                    Debug.LogError("Credentials must not be null or empty!");
                }
                else
                {
                    lastCredentials = CognitoCredentials;

                    Amazon.CognitoIdentity.CognitoAWSCredentials credentials = new Amazon.CognitoIdentity.CognitoAWSCredentials(CognitoCredentials, getAWSEndpoint);
                    client = new Amazon.Polly.AmazonPollyClient(credentials, getAWSEndpoint);
                }
            }
        }

        private IEnumerator speak(Model.Wrapper wrapper, bool isNative)
        {
            //Debug.Log("Speak: " + wrapper);
            connectToAmazon();

            if (client == null)
            {
                Debug.LogWarning("'client' is null! Did you add the correct Cognito credentials?");
            }
            else
            {
                if (wrapper == null)
                {
                    Debug.LogWarning("'wrapper' is null!");
                }
                else
                {
                    if (string.IsNullOrEmpty(wrapper.Text))
                    {
                        Debug.LogWarning("'wrapper.Text' is null or empty!");
                    }
                    else
                    {
                        if (!Util.Helper.isInternetAvailable)
                        {
                            string errorMessage = "Internet is not available - can't use AWS Polly right now!";
                            Debug.LogError(errorMessage);
                            onErrorInfo(wrapper, errorMessage);
                        }
                        else
                        {
                            yield return null; //return to the main process (uid)

                            string voiceCulture = getVoiceCulture(wrapper);
                            string voiceName = getVoiceName(wrapper);

                            System.Text.StringBuilder sbXML = new System.Text.StringBuilder();

                            //SSML
                            sbXML.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
                            sbXML.Append("<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\" xml:lang=\"");
                            sbXML.Append(voiceCulture);
                            sbXML.Append("\"");
                            //sbXML.Append (" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"");
                            //sbXML.Append (" xsi:schemaLocation=\"http://www.w3.org/2001/10/synthesis http://www.w3.org/TR/speech-synthesis/synthesis.xsd\"");
                            sbXML.Append(">");

                            sbXML.Append(prepareProsody(wrapper));

                            sbXML.Append("</speak>");

                            Amazon.Polly.Model.SynthesizeSpeechRequest synthesizeSpeechPresignRequest = new Amazon.Polly.Model.SynthesizeSpeechRequest();
                            synthesizeSpeechPresignRequest.Text = sbXML.ToString();
                            synthesizeSpeechPresignRequest.TextType = Amazon.Polly.TextType.Ssml;
                            synthesizeSpeechPresignRequest.VoiceId = voiceName;
                            synthesizeSpeechPresignRequest.OutputFormat = Amazon.Polly.OutputFormat.Ogg_vorbis;

                            silence = false;

                            if (!isNative)
                            {
                                onSpeakAudioGenerationStart(wrapper);
                            }

                            string outputFile = getOutputFile(wrapper.Uid);
                            bool success = false;
                            bool requestFinished = false;

                            client.SynthesizeSpeechAsync(synthesizeSpeechPresignRequest, (responseObject) =>
                            {
                                if (responseObject.Exception == null)
                                {
                                    using (System.IO.FileStream fileStream = System.IO.File.Create(outputFile))
                                    {
                                        copyTo(responseObject.Response.AudioStream, fileStream);
                                    }

                                    success = true;
                                }
                                else
                                {
                                    string errorMessage = "Could not speak the text: " + wrapper + System.Environment.NewLine + "Error: " + responseObject.Exception;
                                    Debug.LogError(errorMessage);
                                    onErrorInfo(wrapper, errorMessage);
                                }

                                requestFinished = true;
                            });

                            do
                            {
                                yield return null;
                            } while (!requestFinished);

                            if (success)
                            {
                                yield return playAudioFile(wrapper, Util.Constants.PREFIX_FILE + outputFile, outputFile, AudioFileType, isNative);
                            }
                        }
                    }
                }
            }
        }

        private string getVoiceCulture(Model.Wrapper wrapper)
        {
            if (wrapper.Voice == null || string.IsNullOrEmpty(wrapper.Voice.Culture))
            {
                if (Util.Config.DEBUG)
                    Debug.LogWarning("'wrapper.Voice' or 'wrapper.Voice.Culture' is null! Using the 'default' English voice.");

                //always use English as fallback
                return "en-US";
            }
            else
            {
                return wrapper.Voice.Culture;
            }
        }

        private static void copyTo(System.IO.Stream input, System.IO.Stream outputAudio)
        {
            byte[] buffer = new byte[1024];
            int bytesRead;

            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                outputAudio.Write(buffer, 0, bytesRead);
            }
        }

        private static string prepareProsody(Model.Wrapper wrapper)
        {
            if (wrapper.Rate != 1f || wrapper.Pitch != 1f)
            {
                System.Text.StringBuilder sbXML = new System.Text.StringBuilder();

                sbXML.Append("<prosody");

                if (wrapper.Rate != 1f)
                {
                    float _rate = wrapper.Rate > 1 ? (wrapper.Rate - 1f) * 0.5f : wrapper.Rate - 1f;

                    sbXML.Append(" rate=\"");
                    if (_rate >= 0f)
                    {
                        sbXML.Append(_rate.ToString("+#0%", Util.Helper.BaseCulture));
                    }
                    else
                    {
                        sbXML.Append(_rate.ToString("#0%", Util.Helper.BaseCulture));
                    }

                    sbXML.Append("\"");
                }

                if (wrapper.Pitch != 1f)
                {
                    float _pitch = wrapper.Pitch - 1f;

                    sbXML.Append(" pitch=\"");
                    if (_pitch >= 0f)
                    {
                        sbXML.Append(_pitch.ToString("+#0%", Util.Helper.BaseCulture));
                    }
                    else
                    {
                        sbXML.Append(_pitch.ToString("#0%", Util.Helper.BaseCulture));
                    }

                    sbXML.Append("\"");
                }

                sbXML.Append(">");

                sbXML.Append(wrapper.Text);

                sbXML.Append("</prosody>");

                return sbXML.ToString();
            }

            return wrapper.Text;
        }

        #endregion


        #region Editor-only methods

#if UNITY_EDITOR
        public override void GenerateInEditor(Model.Wrapper wrapper)
        {
            Debug.LogError("GenerateInEditor is not supported for AWS Polly!");
        }

        public override void SpeakNativeInEditor(Model.Wrapper wrapper)
        {
            Debug.LogError("SpeakNativeInEditor is not supported for AWS Polly!");
        }
#endif

        #endregion
    }
}

// © 2018-2019 crosstales LLC (https://www.crosstales.com)
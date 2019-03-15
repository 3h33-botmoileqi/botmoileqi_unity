using UnityEngine;
using UnityEditor;
using CrazyMinnow.AmplitudeWebGL;

namespace CrazyMinnow.SALSA.AmplitudeWebGL
{
	[CustomEditor(typeof(AmplitudeSALSA))]
	public class AmplitudeSALSAEditor : Editor
	{
		private AmplitudeSALSA instance;
		private Texture inspLogo;

		public void OnEnable()
		{
			instance = target as AmplitudeSALSA;
			if (instance) instance.FindAmplitude();
		}

		public override void OnInspectorGUI()
		{
			GUILayout.Space(5);
			EditorGUILayout.BeginVertical(GUI.skin.box);
			{
				GUILayout.Space(5);
				
				instance.salsaType = (AmplitudeSALSA.SalsaType)EditorGUILayout.EnumPopup(
					new GUIContent("SALSA Type", "Select Salsa2D or Salsa3D"), instance.salsaType);

				instance.SetSalsaType(instance.salsaType);

				if (instance.salsaType == AmplitudeSALSA.SalsaType.Salsa2D)
				{
					instance.salsa2D = (Salsa2D)EditorGUILayout.ObjectField(
						new GUIContent("Salsa2D", "Salsa2D reference"), instance.salsa2D, typeof(Salsa2D), true);
				}
				else
				{
					instance.salsa3D = (Salsa3D)EditorGUILayout.ObjectField(
						new GUIContent("Salsa3D", "Salsa3D reference"), instance.salsa3D, typeof(Salsa3D), true);
				}

				instance.amplitude = (Amplitude)EditorGUILayout.ObjectField(
					new GUIContent("Amplitude", "Link to the Amplitude component for this character."),
					instance.amplitude, typeof(Amplitude), true);

				GUILayout.Space(5);
			}
			EditorGUILayout.EndVertical();
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrazyMinnow.SALSA;
using CrazyMinnow.AmplitudeWebGL;

namespace CrazyMinnow.SALSA.AmplitudeWebGL
{
	[AddComponentMenu("Crazy Minnow Studio/Amplitude/Addons/AmplitudeSALSA")]
	public class AmplitudeSALSA : MonoBehaviour 
	{
		public enum SalsaType { Salsa2D, Salsa3D }
		public SalsaType salsaType = SalsaType.Salsa3D;
		public Salsa2D salsa2D;
		public Salsa3D salsa3D;
		public Amplitude amplitude;

		private WaitForSeconds audioUpdateDelay;
		private bool prevPlayState;

		/// <summary>
		/// Find the Amplitude component on reset
		/// </summary>
		void Reset()
		{
			FindAmplitude();
		}

		/// <summary>
		/// Set the audioUpdateDelay and start the update coroutine
		/// </summary>
		void Start()
		{
			if (salsa2D)
				audioUpdateDelay = new WaitForSeconds(salsa2D.audioUpdateDelay);
			if (salsa3D)
				audioUpdateDelay = new WaitForSeconds(salsa3D.audioUpdateDelay);

			StartCoroutine(UpdateSalsa());
		}

		/// <summary>
		/// Using the SALSA audioUpdateDelay, fetch the amplitude.average, scale it, apply boost, and write it to the SALSA component average.
		/// </summary>
		IEnumerator UpdateSalsa()
		{
			while (true)
			{
				if (amplitude.audioSource.isPlaying)
				{
					prevPlayState = true;
					
					if (salsa2D)
						salsa2D.average = SalsaUtility.ScaleRange(amplitude.average, 0f, 1f, 0f, 0.012f);
					
					if (salsa3D)
						salsa3D.average = SalsaUtility.ScaleRange(amplitude.average, 0f, 1f, 0f, 0.012f);
				}

				if (!amplitude.audioSource.isPlaying && amplitude.audioSource.isPlaying != prevPlayState)
				{
					prevPlayState = amplitude.audioSource.isPlaying;

					if (salsa2D)
						salsa2D.average = 0f;
					
					if (salsa3D)
						salsa3D.average = 0f;
				}

				yield return audioUpdateDelay;
			}
		}

		/// <summary>
		/// Set the SALSA type 2D or 3D, and find the corresponding component
		/// </summary>
		/// <param name="salsaType"></param>
		public void SetSalsaType(SalsaType salsaType)
		{
			switch (salsaType)
			{
				case SalsaType.Salsa2D:
					salsa3D = null;
					if (!salsa2D) salsa2D = GetComponent<Salsa2D>();
					break;
				case SalsaType.Salsa3D:
					salsa2D = null;
					if (!salsa3D) salsa3D = GetComponent<Salsa3D>();
					break;
			}
		}

		/// <summary>
		/// Find the local Amplitude component
		/// </summary>
		public void FindAmplitude()
		{
			if (!amplitude) amplitude = GetComponent<Amplitude>();
		}
	}
}
-----------------
AmplitudeSALSA

Version 1.0.1
-----------------
Designed to bridge the SALSA and Amplitude assets, to provide SALSA lip-sync on the WebGL platform.

Crazy Minnow Studio, LLC
CrazyMinnowStudio.com

https://crazyminnowstudio.com/posts/salsa-lipsync-in-webgl-with-amplitude/


Package Contents
----------------
Crazy Minnow Studio/SALSA with RandomEyes/AmplitudeSALSA
	Editor
		AmplitudeSALSAEditor.cs
			Custom inspector for the AmplitudeSALSA class.
	AmplitudeSALSA.cs
		The component that bridges the SALSA and Amplitude assets and allows SALSA to work in WebGL.
	ReadMe.txt
		This readme file.


Installation Instructions
-------------------------
1. Install SALSA into your project.
	Select [Window] -> [Asset Store]
		Once the Asset Store window opens, select the download icon, and download and import [SALSA with RandomEyes].

2. Install Amplitude into your project.
	Select [Window] -> [Asset Store]
		Once the Asset Store window opens, select the download icon, and download and import [Amplitude].

3. Install the AmplitudeSALSA add-on into your project.
	Select [Assets] -> [Import Package] -> [Custom Package...]
		Browse to the AmplitudeSALSA_X.X.X.unitypackage you downloaded from CrazyMinnowStudio.com.


Usage Instructions
------------------
1. Setup a SALSA character as you normally would.

2. Add the Amplitude component, link your SALSA AudioSource to the Amplitude.audioSource field, and leave the sample size and absolute values checkbox at their default settings.

3. Add the AmplitudeSALSA component to the same GameObject where SALSA was added, and select the appropriate SALSA version (2D or 3D), be sure it auto linked your SALSA and Amplitude components.

4. While in the editor, play the scene, play SALSA, and adjust the AmplitudeSALSA boost to get the desired results. The results should look similar once compiled to WebGL.
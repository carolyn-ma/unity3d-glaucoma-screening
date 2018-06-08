# Specs:

Unity 5.3.4p4 (most updated that is compatible with runtime)
Oculus Utilities 1.3.0 (compatible with runtime)
Oculus Runtime 0.8.0.0(eliminated the need for directtorift.exe)
*FOVE's new unity plugin only supports Unity 5.4 or later.

# Test:

1.	Plug in USB hub, Oculus(headset and tracker), and Mouse
2.	Open Oculus Configuration Utility, Click "Advanced", Click "Disable Health and Safety Warning"
3.	Make sure Octave is closed
4.	Click EarlyGlaucoma_LeftEye.exe or EarlyGlaucoma_RightEye.exe in Dropbox/Oculus Work/CarolynMa/VF Project (don't check "windowed")
5.	Instruct patients to wear the eye patch and focus on the center fixation point throughout the test, also do not move head above and below
6.	Let them know that the fixation point will start with red and change to cyan when the test is 1/4 done, blue when it is 1/2 done, and green when it is finished
7. 	Use "alt"+"tab" to close the test
8. 	Output text file is under /Results folder with test date and time. Feel free to rename with patient names.
	

# Graphic Outputs(Octave):
*On the graphics laptop generating outputs can be a little slow right after starting Octave, but will be much faster after keeping it open for a while.

1.	Open Octave and click on Editor at the bottom middle
2.	Hit F5 for both tabs (heatmap.m and 3D graph.m) to generate graphic outputs
3.	At prompt, choose file to render
4. 	For 3D graph output, can click on autoscale to look into details or click on Edit/Rotate to rotate around
5.	In graph outputs, can click on File/Save as to save as pdf files
	

------------------------------------------------------------------
*To change mode(EarlyGlaucoma/LaterGlaucoma)and eye(left/right):

1. Open Unity
2. Click on hierachies on the left and choose OVECameraRig/TrackingSpace/CenterEyeAnchor/Light Stimulus
3. On the right, choose from "mode" dropdown and "eye" dropdown in manager
4. File -> Build Settings -> Build (as LaterGlaucoma_RightEye.exe)


------------------------------------------------------------------
Fixed QA Issues 5/23/17:

1. Red dot intensity
2. Color transitions
3. Later Glaucoma start at 2
4. Turn off head tracking
5. Save printout as pdf


------------------------------------------------------------------
QA 5/28/17:

1. Saving new printouts
2. Error counter
3. Change color only once (blue-red)
4. Make stimuli appear smaller
*5. One eye rendering (the splash screen, which can only be turned off in professional accounts of unity, is very distracting in the other eye when rendering to only one eye)





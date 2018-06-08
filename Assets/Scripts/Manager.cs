using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

public enum TestMode
{
    EarlyGlaucoma,
    LaterGlaucoma
}

public enum TestingEye
{
    RightEye,
    LeftEye
}

public class Manager : MonoBehaviour
{
    //useful
    public Light fixation;
    public Light stimulus;
    public TestMode mode = TestMode.LaterGlaucoma;
    public TestingEye eye = TestingEye.LeftEye;
    public float startLightInt;
    public float stimuliRadius;
    public List<TestPoint> testPoints;
    public int totalNumberOfPoints;
    public int errorCounter;
   
    private TextAsset asset;        //used to read a file
    private StreamWriter writer;    //used to write to a file

    public float minIntensity; 
    public float maxIntensity;
    public float lightTimer;
    public float minEnableDuration;
    public float maxEnableDuration;
    public float minDisableDuration;
    public float maxDisableDuration;
    public int counter;   //counter that counts total number of points tested
    public int numDone;
    public TestPoint p1, p2, p3, p4, p5;

    //unsure
    //public int blindSpotErrorCheck = 0;
    //public int totalNumberOfBlindSpotPoints;


    // Use this for initialization
    void Start()
    {
        stimulus.enabled = false;
        totalNumberOfPoints = 100;
        if (mode == TestMode.LaterGlaucoma)
        {
            startLightInt = 2.0f;
        }
        if (mode == TestMode.EarlyGlaucoma)
        {
            startLightInt = 1.0f;
        }
        stimuliRadius = 1f;
        minIntensity = 0.0f;  //defaulted to 0
        maxIntensity = 8.0f;
        lightTimer = -0.01f;  // Initialize a wait
        minEnableDuration = 0.8f;
        maxEnableDuration = 1.2f;
        minDisableDuration = 3f;
        maxDisableDuration = 3f;
        counter = 0;
        numDone = 0;
        errorCounter = 0;

        List<float> ints = new List<float>();
        ints.Add(startLightInt);

        //Add regular points
        for (int i = 0; i < totalNumberOfPoints/5-1; i++)
        {
            testPoints.Add(addStimuli());
        } 
        //Add blindspot points
        if (eye == TestingEye.LeftEye)
        {
            p1 = new TestPoint(-15, 0, stimuliRadius, ints);
        }
        if (eye == TestingEye.RightEye)
        {
            p1 = new TestPoint(15, 0, stimuliRadius, ints);
        }
        testPoints.Add(p1);


        for (int i = totalNumberOfPoints / 5; i < 2*totalNumberOfPoints / 5 - 1; i++)
        {
            testPoints.Add(addStimuli());
        }
        if (eye == TestingEye.LeftEye)
        {
            p2 = new TestPoint(-14, -1, stimuliRadius, ints);
        }
        if (eye == TestingEye.RightEye)
        {
            p2 = new TestPoint(14, -1, stimuliRadius, ints);
        }
        testPoints.Add(p2);


        for (int i = 2*totalNumberOfPoints / 5; i < 3*totalNumberOfPoints / 5 - 1; i++)
        {
            testPoints.Add(addStimuli());
        }
        if (eye == TestingEye.LeftEye)
        {
            p3 = new TestPoint(-14, 1, stimuliRadius, ints);
        }
        if (eye == TestingEye.RightEye)
        {
            p3 = new TestPoint(14, 1, stimuliRadius, ints);
        }
        testPoints.Add(p3);


        for (int i = 3 * totalNumberOfPoints / 5; i < 4 * totalNumberOfPoints / 5 - 1; i++)
        {
            testPoints.Add(addStimuli());
        }
        if (eye == TestingEye.LeftEye)
        {
            p4 = new TestPoint(-16, 1, stimuliRadius, ints);
        }
        if (eye == TestingEye.RightEye)
        {
            p4 = new TestPoint(16, 1, stimuliRadius, ints);
        }
        testPoints.Add(p4);


        for (int i = 4 * totalNumberOfPoints / 5; i < totalNumberOfPoints - 1; i++)
        {
            testPoints.Add(addStimuli());
        }
        if (eye == TestingEye.LeftEye)
        {
            p5 = new TestPoint(-16, -1, stimuliRadius, ints);
        }
        if (eye == TestingEye.RightEye)
        {
            p5 = new TestPoint(16, -1, stimuliRadius, ints);
        }
        testPoints.Add(p5);

    }

    TestPoint addStimuli()
    {
        FloatAngle newSti = new FloatAngle(0,0);
        if (mode == TestMode.LaterGlaucoma)
        {
            float x = UnityEngine.Random.Range(-30.0f, 30.0f);
            float y = UnityEngine.Random.Range(-30.0f, 30.0f);

            if (x < 2f && x > 0f)
            {
                x += 2f;
            }
            if (y < 2f && y > 0f)
            {
                y += 2f;
            }
            if (x > -2f && x < 0f)
            {
                x -= 2f;
            }
            if (y > -2f && y < 0f)
            {
                y -= 2f;
            }
            newSti = new FloatAngle(x,y);
        }
        else if (mode == TestMode.EarlyGlaucoma)
        {
            float x = UnityEngine.Random.Range(-15.0f, 15.0f);
            float y = UnityEngine.Random.Range(-15.0f, 15.0f);
            if (x < 2f && x > 0f)
            {
                x += 2f;
            }
            if (y < 2f && y > 0f)
            {
                y += 2f;
            }
            if (x > -2f && x < 0f)
            {
                x -= 2f;
            }
            if (y > -2f && y < 0f)
            {
                y -= 2f;
            }
            if (x > 5f)
            {
                x += 5f;
            }
            if (y > 5f)
            {
                y += 5f;
            }
            if (x < -5f)
            {
                x -= 5f;
            }
            if (y < -5f)
            {
                y -= 5f;
            }
            newSti = new FloatAngle(x,y);
        }
        List<float> ints = new List<float>();
        ints.Add(startLightInt);
        TestPoint newPoint = new TestPoint(newSti.x, newSti.y, stimuliRadius,ints);
        return newPoint;
    }

    void print_results(List<TestPoint> P)
    {
        asset = Resources.Load("/Users/Oculus/Dropbox/Oculus work/CarolynMa/VF Project/Results/ResultsCarolyn_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm") + ".txt") as TextAsset;    //put file in Resources directory
        writer = new StreamWriter("/Users/Oculus/Dropbox/Oculus work/CarolynMa/VF Project/Results/ResultsCarolyn_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm") + ".txt");   //write to file
        string output = "";
        output += "Test mode: " + mode + "\t\n"+"Testing eye: " + eye + "\t\n"+"False positives: " + errorCounter + "/"+totalNumberOfPoints+"\t\n\t\n";
        output += "x\ty\tLight Intensity\t\n";
        for (int i = 0; i < testPoints.Count; i++)
        {
            output += testPoints[i].angles.x + "\t" + testPoints[i].angles.y + "\t" + testPoints[i].lightIntensities[testPoints[i].lightIntensities.Count - 1] + "\n";
        }
        writer.Write(output);
        writer.Close();    //close writer

    }


    // Update is called once per frame
    void Update()
    {

        int tempIndex = counter % totalNumberOfPoints;

        if (numDone == 0)
        {
            fixation.color = Color.blue;
        }

        if (numDone == totalNumberOfPoints)
        {
            print_results(testPoints);
            stimulus.enabled = false;
            fixation.color = Color.red;
        }

        else if (lightTimer > 0f)
        {
            if (lightTimer > UnityEngine.Random.Range(minEnableDuration, maxEnableDuration))
            //Switch to light off logic
            {
                if (testPoints[tempIndex].pointState == TestPointState.DoneTestingCorrect)
                {
                    numDone++;
                }

                if (testPoints[tempIndex].lightIntensities.Count == counter/totalNumberOfPoints + 1) // if not seen light stimulus, higher light intensity next time
                {
                    if (testPoints[tempIndex].pointState == TestPointState.Untested)
                    {
                        testPoints[tempIndex].pointState = TestPointState.NeedsMoreTesting;
                        testPoints[tempIndex].lightIntensities.Add((startLightInt + maxIntensity) / 2);
                    }
                    else if (testPoints[tempIndex].pointState == TestPointState.NeedsMoreTesting && counter / totalNumberOfPoints > 0)
                    {
                        float tempLastInt = testPoints[tempIndex].lightIntensities[counter / totalNumberOfPoints];
                        float tempSecInt = testPoints[tempIndex].lightIntensities[counter / totalNumberOfPoints - 1];
                        testPoints[tempIndex].lightIntensities.Add((tempLastInt + tempSecInt) / 2);
                    }
                }
                //Debug.Log("VfTest turn " + (counter/totalNumberOfPoints+1) + " at index " + tempIndex + " with rotation of " + testPoints[tempIndex].angles.x + " and " + testPoints[tempIndex].angles.y + " at intensity " + stimulus.intensity + " with " + (testPoints[tempIndex].lightIntensities.Count-1) + " intensities tested, " + testPoints[tempIndex].pointState);
                counter++;
                lightTimer = -Time.deltaTime;
            }

            else //Turn spotlight on
            {

                if (testPoints[tempIndex].pointState != TestPointState.DoneTestingCorrect)
                {
                    //Counters head movement, the euler angles' x and y are different from the floatAngles' x and y
                    stimulus.transform.rotation = Quaternion.Euler(fixation.transform.rotation.eulerAngles.x - testPoints[tempIndex].angles.y, fixation.transform.rotation.eulerAngles.y + testPoints[tempIndex].angles.x, fixation.transform.rotation.eulerAngles.z);
                    stimulus.spotAngle = testPoints[tempIndex].radius;
                    stimulus.intensity = testPoints[tempIndex].lightIntensities[counter / totalNumberOfPoints];
                    stimulus.enabled = true;
                }

                
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) // if seen light stimulus, lower light intensity next time
                {
                    if (testPoints[tempIndex].pointState == TestPointState.Untested)
                    {
                        testPoints[tempIndex].pointState = TestPointState.NeedsMoreTesting;
                        testPoints[tempIndex].lightIntensities.Add(0);
                        testPoints[tempIndex].lightIntensities[counter / totalNumberOfPoints+1] = (startLightInt + minIntensity) / 2;
                    }
                    else if (testPoints[tempIndex].pointState == TestPointState.NeedsMoreTesting && counter / totalNumberOfPoints > 0)
                    {
                        float tempLastInt = testPoints[tempIndex].lightIntensities[counter / totalNumberOfPoints];
                        float tempSecInt = testPoints[tempIndex].lightIntensities[counter / totalNumberOfPoints - 1];
                        if (Math.Abs(tempLastInt - tempSecInt) <= 0.3)
                        {
                            testPoints[tempIndex].pointState = TestPointState.DoneTestingCorrect;
                        }
                        else if (tempSecInt > tempLastInt)
                        {
                            testPoints[tempIndex].lightIntensities.Add(0);
                            testPoints[tempIndex].lightIntensities[counter / totalNumberOfPoints+1] = (tempLastInt + minIntensity) / 2;
                        }
                        else if (tempSecInt < tempLastInt)
                        {
                            testPoints[tempIndex].lightIntensities.Add(0);
                            testPoints[tempIndex].lightIntensities[counter / totalNumberOfPoints+1] = (tempLastInt + tempSecInt) / 2;
                        }
                    }
                }
                
                lightTimer += Time.deltaTime;
            }
        }

        else if (lightTimer < 0f)
        {
            if (lightTimer < -UnityEngine.Random.Range(minDisableDuration, maxDisableDuration))
            //Switch to light on logic
            {
                minDisableDuration = 0.2f;
                maxDisableDuration = 0.6f;
                if (testPoints[tempIndex].pointState == TestPointState.DoneTestingCorrect)
                {
                    counter++;
                }
                lightTimer = Time.deltaTime;
                Debug.Log("VfTest turn " + (counter / totalNumberOfPoints + 1) + " at index " + tempIndex + " with rotation of " + testPoints[tempIndex].angles.x + " and " + testPoints[tempIndex].angles.y + " at intensity " + stimulus.intensity + " with " + (testPoints[tempIndex].lightIntensities.Count - 1) + " intensities tested, " + testPoints[tempIndex].pointState);
            }

            else //Turn light off
            {
                stimulus.enabled = false;
                //stimulus.transform.rotation = Quaternion.Euler(fixation.transform.rotation.eulerAngles.x, fixation.transform.rotation.eulerAngles.y, fixation.transform.rotation.eulerAngles.z);

                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                {
                    errorCounter++;
                    Debug.Log("Clicked when there was no point during test.");
                }

                lightTimer -= Time.deltaTime;
            }

        }

        
        
    }
}

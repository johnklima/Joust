using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NeuralNetworkRegression;


//THE BASE NN CLASS
public class NN_Test : MonoBehaviour
{
    private NeuralNetwork nn;
    private float[][] trainX;  //training set (what is our goal output)
    private float[]   trainY;
    private float[][] testX;   //test against 40 actual values
    private float[]   testY;

    public int curEpochs = 0;
    public int maxEpochs = 1000;   // how many training iterations
    public float lrnRate = 0.01f;  // how quickly  - if divide grads by batSize
    public int batSize = 10;       // batch size
    public int epochGroup = 15;     // per frame epochs >= batSize

    
    //config for the NN
    public int numIn  = 2;
    public int numHid = 64;
    public int numOut = 1;

    public bool accTrain = false;
    public bool accTest = false;
    public bool done = false;
    public bool weightsLoaded = false;
    public bool weightsBuilt = false;

    public int[] Xcols = {0, 1 };
    public int[] Ycols = {2};
    public float[] question = {0.1f,0.2f};   //the question, what just played out
    public float answer;                     //what I should do next
    public string fileprefix ;
    public string weightfilename;

    //each NN input (question) is fed by the output of two other NN's
    //based on the bucky topology - I guess I just build these by hand for now.
    public NN_Test[] nodeConnected = new NN_Test[3];
    public int startNodeIndex = 0;
    public bool drawConnections = true;
    public bool visited = false;

    

    // Start is called before the first frame update
    private void Start()
    {
 
        weightfilename += transform.name;


        fileprefix = transform.name;

        string trainFile = fileprefix + "_train.txt";
       
        trainX = Utils.MatLoad(trainFile,
            Xcols, ',', "#");

        trainY = Utils.MatToVec(Utils.MatLoad(trainFile,
            Ycols, ',', "#"));

        
        
        //make new NN
        nn =  new NeuralNetwork(numIn, numHid, numOut, seed: 0);

        
        //test if we have a built weight file
        if (LoadWeights())
            AskQuestion();
       

        // batch training params
        Debug.Log("Start Done " + transform.name);
       
    }

    
    

    float timer = -1;
    
    private void Update()
    {
          
                
        if(!done && ( !weightsLoaded || !weightsBuilt ))
        {
            Debug.Log("do " + epochGroup + " groups - Epochs: " + curEpochs);

            nn.TrainBatch(trainX, trainY, lrnRate,
              batSize, epochGroup);


            curEpochs += epochGroup;  //group process the training

        }

        if (curEpochs >= maxEpochs && !done)
        {
            //accuracy tests?
            /*
            if(accTrain)
            {
                float trainAcc = nn.Accuracy(trainX, trainY, 0.10f);
                Debug.Log("Accuracy on train data = " + trainAcc);
            }

            if (accTest)
            {
                float testAcc = nn.Accuracy(testX, testY, 0.10f);
                Debug.Log("Accuracy on test data  = " + testAcc);
            }
            */
            
            AskQuestion();

            weightsBuilt = true;
            weightsLoaded = true;
            done = true;
            Debug.Log("DONE");


            //// save trained model wts
            ///
            
            Debug.Log("Saving model wts to file " + weightfilename);
            string fn = weightfilename + "_wts.txt";
            nn.SaveWeights(fn);
            

        }

        //Console.WriteLine("\nPredicting income for male" +
        //  " 34 Oklahoma moderate ");
        //float y2 = nn2.ComputeOutput(question);
        //Console.WriteLine("Predicted income = " +
        //  y2.ToString("F5"));


    }
    public void SetQuestion(float a, float b)
    {
        question[0] = a;
        question[1] = b;

    }
    public void AskQuestion()
    {
        

        //input data we want to process
        
        float y = nn.ComputeOutput(question);

        //Debug.Log("first out = " + y);
        answer = y;

    }
    public bool LoadWeights()
    {
        //// load saved wts later 
        Debug.Log("Loading saved wts to new NN " + weightfilename);
        string fn = weightfilename + "_wts.txt";
       
        if(nn.LoadWeights(fn))
        {
            Debug.Log("Has Weights ");
            weightsLoaded = true;
            weightsBuilt = true;
            done = true;
            return true;
        }
        else 
        {
            Debug.Log("Does Not Have Weights ");
            return false;
        }

      
    }
}

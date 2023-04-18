using System;

class GilberElliotModel : Model 
{
    private BinarySymmetricChannel goodState;
    private BinarySymmetricChannel badState;
    private BinarySymmetricChannel currentState;

    private Random randomNumberGenerator = new Random();
    private float goodToBadProbability = .05f;
    private float badToGoodProbability = .9f;

    public GilberElliotModel(
        float goodStateErrorProbability,
        float badStateErrorProbability,
        float goodToBadProbability,
        float badToGoodProbability,
        ref Statistics statistics)
    {
        goodState = new BinarySymmetricChannel(goodStateErrorProbability, ref statistics);
        badState = new BinarySymmetricChannel(badStateErrorProbability, ref statistics);
        this.goodToBadProbability = goodToBadProbability;
        this.badToGoodProbability = badToGoodProbability;

        currentState = goodState;
    }


    public override void Run(int iterations)
    {
        int index = 0;

        while (index < iterations) {

            changeStateIfNecessary();
            currentState.Run(1); // run just one iteration of the BSC per loop iteration
            
            index++;
        }
    }

    private void changeStateIfNecessary()
    {
        float probablity = (float) randomNumberGenerator.NextDouble();

        if (currentState == goodState) {    // the model in the good state
            if (probablity <= goodToBadProbability)
                currentState = badState;
        } else {                            // the model in the bad state
            if (probablity <= badToGoodProbability)
                currentState = goodState;
        }
    }
}
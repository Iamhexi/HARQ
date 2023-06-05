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
        float badToGoodProbability
        )
    {
        goodState = new BinarySymmetricChannel(goodStateErrorProbability);
        badState = new BinarySymmetricChannel(badStateErrorProbability);
        this.goodToBadProbability = goodToBadProbability;
        this.badToGoodProbability = badToGoodProbability;

        currentState = goodState;
    }


    public override void Run(string filename)
    {
        int index = 0;

        // TODO: only required number of times, not constant 100
        int iterations = 100;
        while (index < iterations) {

            changeStateIfNecessary();
            currentState.Run(filename); // run just one iteration of the BSC per loop iteration
            
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
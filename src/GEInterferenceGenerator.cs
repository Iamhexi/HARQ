
using System;
class GEInterferenceGenerator: InterferenceGenerator
{
    private bool inGoodState = true;
    private float goodStateErrorProbability;
    private float badStateErrorProbability;
    private float goodToBadProbability;
    private float badToGoodProbability;

    private float errorProbability;

    private Random randomNumberGenerator = new Random();
    public GEInterferenceGenerator
    (
        float goodStateErrorProbability,
        float badStateErrorProbability,
        float goodToBadProbability,
        float badToGoodProbability
    )
    {
        this.goodStateErrorProbability = goodStateErrorProbability;
        this.badStateErrorProbability = badStateErrorProbability;
        this.goodToBadProbability = goodToBadProbability;
        this.badToGoodProbability = badToGoodProbability;

        this.errorProbability = goodStateErrorProbability;
    }
    public Packet DeformPacket(Packet packet)
    {
        int index = 0;
        changeStateIfNecessary();

        while (index < packet.Length) {
            if (randomNumberGenerator.NextDouble() <= errorProbability)
                packet.Content.InvertBit(index);

            index++;
        }

        return packet;
    }

    private void changeStateIfNecessary()
    {
        float probablity = (float) randomNumberGenerator.NextDouble();

        if (inGoodState) {    // the model in the good state
            if (probablity <= goodToBadProbability) // change to the bad state
                inGoodState = false;
                errorProbability = badStateErrorProbability;
        } else {                            // the model in the bad state
            if (probablity <= badToGoodProbability) // change to the good state
                inGoodState = true;
                errorProbability = goodStateErrorProbability;
        }
    }

}
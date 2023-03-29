class Model
{
    private Sender sender = new Sender();
    private Receiver receiver = new Receiver();
    private ModelType type;

    Model(ModelType type) 
    {
        this.type = type;
    }

    
}
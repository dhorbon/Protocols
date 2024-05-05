using Protocols.Classes;
using System;

public interface IProtocol
{
    //called on creation to initialize requiered data
    public void Initialize(Simulation simReference, Antenna parent, string messageToSend, int[] RecievingAntenna); 
    
    //not recieving a message
    public void Tick();
    
    //recieving a message
    public void Recieve(string content);

    //issue a broadcast
    private void Broadcast(string content, Simulation simReference, Antenna parent)
    {
        simReference.Broadcast(content, parent);
    }

    //save payload portion of a message
    private void Save(string content, Antenna parent)
    {
        parent.Save(content);
    }
}
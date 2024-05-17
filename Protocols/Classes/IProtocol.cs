using Protocols.Classes;
using System;

public interface IProtocol
{
    Simulation simReference { get; set; }
    Antenna parent { get; set; }
    int antennaNumber { get; set; }
    string[] messagesToSend { get; set; }
    int[] RecievingAntennas { get; set; }
    //called on creation to initialize requiered data
    public void Initialize(Simulation simReference, Antenna parent, string[] messagesToSend, int[] RecievingAntennas)
    { 
        this.simReference = simReference;
        this.parent = parent;
        this.messagesToSend = messagesToSend;
        this.RecievingAntennas = RecievingAntennas;
        antennaNumber = parent.antennaNumber;
    }

    //issue a broadcast
    private void Broadcast(string content)
    {
        simReference.Broadcast(content, parent);
    }

    //save payload portion of a message
    private void Save(string content)
    {
        parent.Save(content);
    }

    //use report on every end of protocol
    /* set state to
     * 1 - idle
     * 2 - sending
     * 3 - error
     * 4 - recieved
     * 
     * set packagesToSend to
     * number of messages left to send
     * 
     * set sendingTo to
     * antenna id the next message is being sent to
     */
    private void Report(string state, string packagesToSend, string sendingTo)
    {
        parent.Report(state, packagesToSend, sendingTo);
    }

    

    //not recieving a message
    public void Tick();
    
    //recieving a message
    public void Recieve(string content);
}
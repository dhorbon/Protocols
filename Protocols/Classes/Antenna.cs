namespace Protocols.Classes
{
    public class Antenna
    {
        public float x;
        public float y;

        public string name;

        public string recieved = "";

        public int antennaNumber;

        private Simulation simReference;

        private int curProtocol;
        public List<IProtocol> protocols;

        private List<Message> incomingMessages = new();

        public Antenna(string name, float x, float y, List<IProtocol> protocols, Simulation simulation, int curProtocol, int antennaNumber)
        {
            this.name = name;
            this.x = x;
            this.y = y;
            this.protocols = protocols;
            simReference = simulation;
            this.curProtocol = curProtocol;
            this.antennaNumber = antennaNumber;
            simReference.AddAntenna(this);
        }

        public float DistanceTo(Antenna target)
        {
            return MathF.Sqrt(MathF.Pow(this.x + target.x, 2f) + MathF.Pow(this.y + target.y, 2f));
        }

        public void Tick()
        {
            int numOfSignals = 0;
            int recievedSignal = 0;
            for (int i = 0; i < incomingMessages.Count; i++)
            {
                if (incomingMessages[i].TryRecieve() != null)
                {
                    numOfSignals++;
                    recievedSignal = i;
                }
            }

            if (numOfSignals < 2 && numOfSignals != 0)
            {
                protocols[curProtocol].Recieve(incomingMessages[recievedSignal].TryRecieve());
            }
            else protocols[curProtocol].Tick();
        }
        
        public void Recieve(Message m)
        {
            incomingMessages.Add(m);
        }

        public void Save(string content)
        {
            recieved += content;
        }

        public void Report(string state, string packagesToSend, string sendingTo)
        {
            simReference.Report(state, this, packagesToSend, sendingTo);
        }
    }
}

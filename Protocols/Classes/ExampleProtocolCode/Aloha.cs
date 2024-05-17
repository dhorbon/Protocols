namespace Protocols.Classes.ExampleProtocolCode
{
    public class Aloha : IProtocol
    {
        public Simulation simReference { get; set; }
        public Antenna parent { get; set; }
        public int antennaNumber { get; set; }
        public string[] messagesToSend { get; set; }
        public int[] RecievingAntennas { get; set; }

        private int timeToBroadcast = 0;

        private int currentMessage = 0;
        private int currentPacket = 0;

        public void Tick()
        {
            if (currentMessage >= messagesToSend.Length) return;

            if (timeToBroadcast == 0)
            {
                if (currentPacket >= messagesToSend[currentMessage].Length)
                {
                    currentMessage++;
                    currentPacket = 0;
                    if (currentMessage >= messagesToSend.Length) return;
                }

                string message = RecievingAntennas[currentMessage].ToString() + "/" + messagesToSend[currentMessage][currentPacket];
                Console.WriteLine(message);
                Broadcast(message);
                timeToBroadcast = new Random().Next(0, 5);
            }
            else timeToBroadcast--;
        }

        public void Recieve(string content)
        {  
            if(timeToBroadcast == 0)
            {
                timeToBroadcast = new Random().Next(0, 5);
            }

            if (content == null) return;

            string number = "";
            string payload = "";
            for(int i = 0; i < content.Length; i++)
            {
                if (content[i] == '/')
                {
                    payload = content.Substring(i + 1);
                }
                else
                {
                    number += content[i];
                }
            }

            if(int.Parse(number) == antennaNumber) Save(payload);
        }

        public void Initialize(Simulation simReference, Antenna parent, string[] messagesToSend, int[] RecievingAntennas)
        {
            this.simReference = simReference;
            this.parent = parent;
            this.messagesToSend = messagesToSend;
            this.RecievingAntennas = RecievingAntennas;
        }

        private void Broadcast(string content)
        {
            simReference.Broadcast(content, parent);
        }

        private void Save(string content)
        {
            parent.Save(content);
        }
    }
}

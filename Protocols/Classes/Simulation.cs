using System.Diagnostics;
using System.Reflection;

namespace Protocols.Classes
{
    public class Simulation
    {
        //list of all protocols
        public List<dynamic> protocols = new();

        //currently selected protocol
        public int currentProtocol = 0;

        //list of all antennas
        List<Antenna> antennas = new();

        //stats shown on frontend
        List<List<Dictionary<string, string>>> stats = new();
        
        /* Example
         * [
         *  [
         *   name = "antenna1", name of antenna
         *   status = 1,        status (idle, sending, error, reading)
         *   sending = 12,      packets left to send
         *   to = 1             sending packets to antenna id (-1 if none left)
         *  ],
         *  [
         *   name = "antenna2",
         *   status = 2,
         *   sending = 0,
         *   to = -1
         *  ]
         * ]
         */

        public Simulation()
        {
            var files = Directory.GetFiles("Classes/CustomProtocols/");
            
            for (int i = 0; i < files.Length; i++)
            {
                var file = Directory.GetParent("Classes")?.FullName + @"\" + files[i];
                var types = Assembly.LoadFile(file).GetExportedTypes();

                dynamic obj = null;
                for (int j = 0; j < types.Length; j++)
                {
                    if (types[j].Name != "IProtocol" && types[j].Name != "Antenna" && types[j].Name != "Message" && types[j].Name != "Simulation")
                        obj = Activator.CreateInstance(types[j]);
                }

                protocols.Add(obj);
            }
        }

        public void AddAntenna(Antenna antenna)
        {
            antennas.Add(antenna);
            stats.Add(new());
        }

        public void RemoveAntenna(Antenna antenna)
        {       
            for(int i = 0; i < antennas.Count; i++)
            {
                if (antennas[i] == antenna)
                {
                    antennas.RemoveAt(i);
                    stats.RemoveAt(i);
                    for(int j = i; j < antennas.Count; j++)
                    {
                        antennas[j].antennaNumber--;
                    }
                    break;
                }
            }
        }

        public void Broadcast(string m, Antenna antenna)
        {
            for(int i = 0; i < antennas.Count; i++)
            {
                if (antennas[i] != antenna)
                {
                    antennas[i].Recieve(new Message(m, (int)MathF.Ceiling(antenna.DistanceTo(antennas[i]))));
                }
            }
        }

        public void Report(string state, Antenna antenna, string packagesToSend, string sendingTo)
        {
            for (int i = 0;i < antennas.Count; i++)
            {
                if (antennas[i] == antenna)
                {
                    stats[i].Add(new());
                    stats[i][stats[i].Count - 1].Add("name", antenna.name);
                    stats[i][stats[i].Count - 1].Add("status", state);
                    stats[i][stats[i].Count - 1].Add("sending", packagesToSend);
                    stats[i][stats[i].Count - 1].Add("to", sendingTo);
                    break;
                }
            }
        }
    }
}

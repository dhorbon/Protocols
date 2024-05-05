using System.Diagnostics;
using System.Reflection;

namespace Protocols.Classes
{
    public class Simulation
    {
        public List<IProtocol> protocols = new();
        public List<Type> types = new();

        public int currentProtocol = 0;

        List<Antenna> antennas = new();

        public Simulation()
        {
            var files = Directory.GetFiles("Classes/CustomProtocols/");
            
            for (int i = 0; i < files.Length; i++)
            {
                var file = Directory.GetParent("Classes")?.FullName + @"\" + files[i];
                var dll = Assembly.LoadFile(file);
                types.Add(dll.GetExportedTypes()[i]);
                protocols.Add(null);
            }
        }

        public void Broadcast(string m, Antenna antenna)
        {
            for(int i = 0; i < antennas.Count; i++)
            {
                if (antennas[i] == antenna)
                {
                    antennas[i].Recieve(new Message(m, (int)MathF.Ceiling(antenna.DistanceTo(antennas[i]))));
                }
            }
        }
    }
}

using System.Diagnostics;
using System.Reflection;

namespace Protocols.Classes
{
    public class Simulation
    {
        public List<dynamic> protocols = new();
        public List<Type> types = new();

        public Simulation()
        {
            var files = Directory.GetFiles("Classes/CustomProtocols/");
            
            for (int i = 0; i < files.Length; i++)
            {
                var file = Directory.GetParent("Classes").FullName + @"\" + files[i];
                var dll = Assembly.LoadFile(file);
                protocols.Add(dll.GetExportedTypes()[0]);
                dynamic c = Activator.CreateInstance(dll.GetExportedTypes()[0]);
                c.Tick();
                c.Recieve();
            }
        }

        public void Broadcast(Message m, Antenna antenna)
        {

        }
    }
}

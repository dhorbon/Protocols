

namespace Protocols.Classes
{
    public class Antenna
    {
        public float x;
        public float y;

        public List<IProtocol?> protocols;

        

        private List<Message> incomingMessages = new();

        public Antenna(float x, float y, List<IProtocol?> protocols)
        {
            this.x = x;
            this.y = y;
            this.protocols = protocols;
        }

        public float DistanceTo(Antenna target)
        {
            return MathF.Sqrt(MathF.Pow(this.x + target.x, 2f) + MathF.Pow(this.y + target.y, 2f));
        }
    }
}

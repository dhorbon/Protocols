namespace Protocols.Classes
{
    public class Message
    {
        public string content;
        public int timeToRecieve;
        public Message(string content, int timeToRecieve)
        {
            this.content = content;
            this.timeToRecieve = timeToRecieve;
        }
        
        public string? TryRecieve()
        {
            if(timeToRecieve == 1)
            {
                return content;
            }
            timeToRecieve--;
            return null;
        }
    }
}

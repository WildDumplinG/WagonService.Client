namespace Console.Helper
{
    public class ConsoleController
    {

        public Dictionary<string, object> Behaviors = new Dictionary<string, object>();

        public void AddBehavior(object behavior)
        {
            if (!Behaviors.ContainsKey(behavior.GetType().Name))
            {
                Behaviors.Add(behavior.GetType().Name, behavior);
            }
            else
            {
                Behaviors.Remove(behavior.GetType().Name);
                Behaviors.Add(behavior.GetType().Name, behavior);
            }
        }

        public void Read()
        {
            while (true)
            {
                string[] data = System.Console.ReadLine()!.Split(" ");

                if (Behaviors.ContainsKey(data.First()) && data.Length > 1)
                {
                    var cmd = data.Skip(2).ToArray();
                    Behaviors[data.First()].Execute(data[1], cmd);
                }
                else
                {
                    System.Console.WriteLine($"This class is missing {data.First()}");
                }

            }
        }
    }
}

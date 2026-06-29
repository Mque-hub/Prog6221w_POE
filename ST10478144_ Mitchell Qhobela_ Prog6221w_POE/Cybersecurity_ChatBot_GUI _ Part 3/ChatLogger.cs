using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersecurity_ChatBot_GUI
{
    public class ChatLogger
    {
        private readonly List<ActivityLog> _logs = new List<ActivityLog>();

        public void Record(string action)
        {
            _logs.Add(new ActivityLog(action));
        }

        public void DisplayLog()
        {
            if (_logs.Count == 0)
            {
                Console.WriteLine("No actions have been recorded.");
                return;
            }

            Console.WriteLine("\n=== Chat Log ===");

            foreach (var log in _logs)
            {
                Console.WriteLine(log);
            }

            Console.WriteLine("================\n");
        }
    }
}

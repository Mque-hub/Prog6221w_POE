using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersecurity_ChatBot_GUI
{
    public class ChatLogger
    {
        // Creating the Activity log list<>
        private readonly List<ActivityLog> _logs = new List<ActivityLog>();

        public void Record(string action)
        {
            _logs.Add(new ActivityLog(action));
        }

        // Method for retrieving the logs
        public List<ActivityLog> GetLogs()
        {
            return _logs;
        }

        // Method for displaying the logs
        public string DisplayLog()
        {
            if (_logs.Count == 0)
                return "No activity has been recorded.";

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("========== Activity Log ==========");
            sb.AppendLine();

            foreach (ActivityLog log in _logs)
            {
                sb.AppendLine(log.ToString());
            }

            return sb.ToString();
        }
    }
}

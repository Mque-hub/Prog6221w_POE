using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersecurity_ChatBot_GUI
{
    public class ActivityLog
    {
        //Activity log getters and setters
        public DateTime Timestamp { get; set; }
        public string Action { get; set; }

        // Method to Display the exact timeframe of the events 
        public ActivityLog(string action)
        {
            Timestamp = DateTime.Now;
            Action = action;
        }

        // Method for the time display structure
        public override string ToString()
        {
            return $"[{Timestamp:HH:mm:ss}] {Action}";
        }
    }
}

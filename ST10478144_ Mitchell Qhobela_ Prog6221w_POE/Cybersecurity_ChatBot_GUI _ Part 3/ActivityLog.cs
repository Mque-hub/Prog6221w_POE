using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersecurity_ChatBot_GUI
{
    public class ActivityLog
    {
        public DateTime Timestamp { get; set; }
        public string Action { get; set; }

        public ActivityLog(string action)
        {
            Timestamp = DateTime.Now;
            Action = action;
        }

        public override string ToString()
        {
            return $"[{Timestamp:HH:mm:ss}] {Action}";
        }
    }
}

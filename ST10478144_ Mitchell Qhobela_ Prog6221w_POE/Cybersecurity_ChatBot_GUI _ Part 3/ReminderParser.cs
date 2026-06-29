using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Cybersecurity_ChatBot_GUI
{
    public class ReminderParser
    {
        // Method for Reminder getters and setters
        public class ReminderParseResult
        {
            public bool Success { get; set; }
            public DateTime ReminderDate { get; set; }
            public string DisplayMessage { get; set; }
        }

        // Method of an exception incase of an empty or incomplete task request
        public ReminderParseResult ParseReminder(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return Fail("Reminder text is empty.");
            }

            input = input.Trim().ToLower();

            DateTime baseDate;

            // Regex is used to determine base dates
            if (Regex.IsMatch(input, @"\btomorrow\b"))
            {
                baseDate = DateTime.Today.AddDays(1);
            }
            else if (Regex.IsMatch(input, @"\btoday\b"))
            {
                baseDate = DateTime.Today;
            }
            else if (Regex.IsMatch(input, @"\bnext week\b"))
            {
                baseDate = DateTime.Today.AddDays(7);
            }
            else
            {
                Match dayMatch = Regex.Match(input, @"in\s+(\d+)\s+day[s]?");
                if (dayMatch.Success)
                {
                    int days = int.Parse(dayMatch.Groups[1].Value);

                    if (days < 0)
                        return Fail("Reminder days cannot be negative.");

                    baseDate = DateTime.Today.AddDays(days);
                }
                else
                {
                    return Fail("I couldn't understand the reminder date. Try 'tomorrow', 'today at 18:00', or 'in 3 days'.");
                }
            }

            // The following conditions are to indentify if the user types e.g., 12 pm or 12:00 and return the time in the task display 
            TimeSpan time = new TimeSpan(9, 0, 0);

            Match time24Match = Regex.Match(input, @"at\s+(\d{1,2}):(\d{2})");
            Match time12Match = Regex.Match(input, @"at\s+(\d{1,2})\s*(am|pm)");

            if (time24Match.Success)
            {
                int hour = int.Parse(time24Match.Groups[1].Value);
                int minute = int.Parse(time24Match.Groups[2].Value);

                if (hour < 0 || hour > 23 || minute < 0 || minute > 59)
                    return Fail("Invalid time. Use a valid 24-hour time like 14:30.");

                time = new TimeSpan(hour, minute, 0);
            }
            else if (time12Match.Success)
            {
                int hour = int.Parse(time12Match.Groups[1].Value);
                string ampm = time12Match.Groups[2].Value;

                if (hour < 1 || hour > 12)
                    return Fail("Invalid time. Use a valid 12-hour time like 7pm.");

                if (ampm == "pm" && hour < 12)
                    hour += 12;
                if (ampm == "am" && hour == 12)
                    hour = 0;

                time = new TimeSpan(hour, 0, 0);
            }

            DateTime reminderDate = baseDate.Date + time;
            int daysDifference = (reminderDate.Date - DateTime.Today).Days;

            string display;
            if (daysDifference == 0)
                display = $"Reminder today: {reminderDate:dd MMM yyyy HH:mm}";
            else if (daysDifference == 1)
                display = $"Reminder in 1 day: {reminderDate:dd MMM yyyy HH:mm}";
            else
                display = $"Reminder in {daysDifference} days: {reminderDate:dd MMM yyyy HH:mm}";

            return new ReminderParseResult
            {
                Success = true,
                ReminderDate = reminderDate,
                DisplayMessage = display
            };
        }


        private ReminderParseResult Fail(string message)
        {
            return new ReminderParseResult
            {
                Success = false,
                DisplayMessage = message
            };
        }
    }
}

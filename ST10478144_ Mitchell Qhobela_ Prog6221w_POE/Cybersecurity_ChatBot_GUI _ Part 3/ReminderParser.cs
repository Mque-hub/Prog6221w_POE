using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Cybersecurity_ChatBot_GUI
{
    public class ReminderParser
    {
        public class ReminderParseResult
        {
            public bool Success { get; set; }
            public DateTime ReminderDate { get; set; }
            public string DisplayMessage { get; set; }
        }
    

    public ReminderParseResult ParseReminder(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return new ReminderParseResult
                {
                    Success = false,
                    DisplayMessage = "Reminder text is empty."
                };
            }

            input = input.Trim().ToLower();

            // 1. tomorrow
            if (input.Contains("tomorrow"))
            {
                DateTime reminderDate = DateTime.Today.AddDays(1);

                return new ReminderParseResult
                {
                    Success = true,
                    ReminderDate = reminderDate,
                    DisplayMessage = $"Reminder in 1 day: {reminderDate:dd MMM yyyy}"
                };
            }

            // 2. today
            if (input.Contains("today"))
            {
                DateTime reminderDate = DateTime.Today;

                return new ReminderParseResult
                {
                    Success = true,
                    ReminderDate = reminderDate,
                    DisplayMessage = $"Reminder today: {reminderDate:dd MMM yyyy}"
                };
            }

            // 3. in X days
            Match dayMatch = Regex.Match(input, @"in\s+(\d+)\s+day[s]?");
            if (dayMatch.Success)
            {
                int days = int.Parse(dayMatch.Groups[1].Value);
                DateTime reminderDate = DateTime.Today.AddDays(days);

                return new ReminderParseResult
                {
                    Success = true,
                    ReminderDate = reminderDate,
                    DisplayMessage = $"Reminder in {days} day{(days == 1 ? "" : "s")}: {reminderDate:dd MMM yyyy}"
                };
            }

            // 4. next week
            if (input.Contains("next week"))
            {
                DateTime reminderDate = DateTime.Today.AddDays(7);

                return new ReminderParseResult
                {
                    Success = true,
                    ReminderDate = reminderDate,
                    DisplayMessage = $"Reminder in 7 days: {reminderDate:dd MMM yyyy}"
                };
            }

            return new ReminderParseResult
            {
                Success = false,
                DisplayMessage = "I couldn't understand the reminder date. Try phrases like 'tomorrow', 'in 2 days', or 'next week'."
            };
        }
    }
}

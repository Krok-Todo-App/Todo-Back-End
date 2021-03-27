using System.Collections.Generic;
using taskAPI.Model;
using System;

namespace taskAPI.Helpers
{
    public static class StatusHelper
    {

        public static ToDoTask ValidateStatus(ToDoTask task)
        {
            // do nothng if task is done or there is no due date
            if (task.Status == "done" || task.DueDate == null)
                return task;

            if (IsTaskExipred(task.DueDate.Value))
                task.Status = "expired";

            return task;
        }

        public static List<ToDoTask> ValidateStatus(List<ToDoTask> tasks)
        {

            List<ToDoTask> output = new List<ToDoTask>();

            foreach (var task in tasks)
            {
                // do nothng if task is done or there is no due date
                if (task.Status == "done" || task.DueDate == null)
                {
                    output.Add(task);
                    continue;
                }

                if (IsTaskExipred(task.DueDate.Value))
                    task.Status = "expired";

                output.Add(task);
            }

            return output;
        }

        private static bool IsTaskExipred(DateTime dueDate){
            var now = DateTime.Now;
            return now.Year >= dueDate.Year &&
                   now.Month >= dueDate.Month &&
                   now.Day >= dueDate.Day &&
                   now.Hour >= dueDate.Hour &&
                   now.Minute >= dueDate.Minute;
        }
    }
}
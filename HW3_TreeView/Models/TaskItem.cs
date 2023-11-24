using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3_TreeView.Models
{
    public class TaskItem
    {
        public string TaskName { get; set; }
        public ObservableCollection<TaskItem> SubTasks { get; set; }

        public TaskItem()
        {
            SubTasks = new ObservableCollection<TaskItem>();
        }
    }
}

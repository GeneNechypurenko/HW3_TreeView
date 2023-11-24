using HW3_TreeView.Models;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HW3_TreeView
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<TaskItem> Tasks { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Tasks = new ObservableCollection<TaskItem>();
            treeView.ItemsSource = Tasks;
        }
        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            TaskItem selectedTask = treeView.SelectedItem as TaskItem;

            if (selectedTask != null) { selectedTask.SubTasks.Add(new TaskItem { TaskName = inputTextBox.Text }); }
            else { Tasks.Add(new TaskItem { TaskName = "New Task" }); }
        }
        private void RemoveTask_Click(object sender, RoutedEventArgs e)
        {
            TaskItem selectedTask = treeView.SelectedItem as TaskItem;

            if (selectedTask != null)
            {
                TaskItem parentTask = FindParentTask(Tasks, selectedTask);
                if (parentTask != null) { parentTask.SubTasks.Remove(selectedTask); }
                else { Tasks.Remove(selectedTask); }
            }
        }
        private TaskItem FindParentTask(IEnumerable<TaskItem> tasks, TaskItem childTask)
        {
            foreach (var task in tasks)
            {
                if (task.SubTasks.Contains(childTask)) { return task; }
                var parent = FindParentTask(task.SubTasks, childTask);
                if (parent != null) { return parent; }
            }
            return null;
        }
    }
}
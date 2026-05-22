using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lab6
{
    public partial class Form1 : Form
    {
        // Собственный делегат для фильтрации задач
        delegate bool TaskFilter(TaskItem task);

        // Собственный делегат для уведомлений (multicast)
        delegate void NotifyHandler(string message);

        // Список задач и multicast-делегат уведомлений
        private List<TaskItem> allTasks = new List<TaskItem>();
        private NotifyHandler notify;

        public Form1()
        {
            InitializeComponent();

            // Multicast-делегат: соединяем два метода через +=
            notify = WriteToLog;
            notify += WriteToStatus;

            comboBoxFilter.Items.AddRange(new string[]
            {
                "Все задачи",            // 0
                "Выполненные",           // 1
                "Невыполненные",         // 2
                "Высокий приоритет (4-5)" // 3
            });
            comboBoxFilter.SelectedIndex = 0;

            UpdateStats();
        }

        // ===== Методы-обработчики уведомлений =====
        private void WriteToLog(string message)
        {
            string line = "[" + DateTime.Now.ToString("HH:mm:ss") + "] " + message;
            listBoxLog.Items.Insert(0, line);
        }

        private void WriteToStatus(string message)
        {
            labelStatus.Text = message;
        }

        // ===== Методы фильтрации =====
        private bool FilterAll(TaskItem t) => true;
        private bool FilterDone(TaskItem t) => t.IsDone;
        private bool FilterNotDone(TaskItem t) => !t.IsDone;
        private bool FilterHighPriority(TaskItem t) => t.Priority >= 4;

        private TaskFilter GetCurrentFilter()
        {
            switch (comboBoxFilter.SelectedIndex)
            {
                case 1: return FilterDone;
                case 2: return FilterNotDone;
                case 3: return FilterHighPriority;
                default: return FilterAll;
            }
        }

        // ===== Применение фильтра + сортировка лямбдой =====
        private void ApplyFilter()
        {
            // Сортировка: сначала невыполненные, затем по убыванию приоритета
            allTasks.Sort((a, b) =>
            {
                if (a.IsDone != b.IsDone)
                    return a.IsDone ? 1 : -1;
                return b.Priority.CompareTo(a.Priority);
            });

            TaskFilter filter = GetCurrentFilter();
            listBoxTasks.Items.Clear();

            for (int i = 0; i < allTasks.Count; i++)
            {
                if (filter(allTasks[i]))
                    listBoxTasks.Items.Add(allTasks[i]);
            }
        }

        // ===== Статистика через стандартные делегаты =====
        private void UpdateStats()
        {
            // Func<int> — делегат без параметров, возвращает int
            Func<int> getTotal = () => allTasks.Count;
            Func<int> getDone = () =>
            {
                int count = 0;
                foreach (TaskItem t in allTasks)
                    if (t.IsDone) count++;
                return count;
            };
            Func<int> getLeft = () => getTotal() - getDone();

            // Action<string> — делегат для обновления Label
            Action<string> showStats = text => labelStats.Text = text;

            string result = "Всего: " + getTotal() +
                            " | Выполнено: " + getDone() +
                            " | Осталось: " + getLeft();
            showStats(result);
        }

        // ===== Кнопка "Добавить" =====
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                MessageBox.Show("Введите название задачи!");
                return;
            }

            TaskItem task = new TaskItem(textBoxName.Text, (int)numericPriority.Value);
            allTasks.Add(task);
            textBoxName.Clear();

            notify("Добавлена: " + task.Name + " (приоритет " + task.Priority + ")");
            ApplyFilter();
            UpdateStats();
        }

        // ===== Кнопка "Удалить" =====
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxTasks.SelectedItem == null)
            {
                MessageBox.Show("Выберите задачу для удаления!");
                return;
            }

            TaskItem task = (TaskItem)listBoxTasks.SelectedItem;
            allTasks.Remove(task);

            notify("Удалена: " + task.Name);
            ApplyFilter();
            UpdateStats();
        }

        // ===== Кнопка "Выполнено" =====
        private void buttonDone_Click(object sender, EventArgs e)
        {
            if (listBoxTasks.SelectedItem == null)
            {
                MessageBox.Show("Выберите задачу!");
                return;
            }

            TaskItem task = (TaskItem)listBoxTasks.SelectedItem;
            task.IsDone = !task.IsDone;

            string action = task.IsDone ? "Выполнена: " : "Возвращена: ";
            notify(action + task.Name);
            ApplyFilter();
            UpdateStats();
        }

        // ===== Смена фильтра =====
        private void comboBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            notify("Фильтр: " + comboBoxFilter.SelectedItem.ToString());
            ApplyFilter();
        }
    }

    // ===== Класс задачи =====
    public class TaskItem
    {
        public string Name { get; set; }
        public int Priority { get; set; }
        public bool IsDone { get; set; }
        public DateTime CreatedAt { get; set; }

        public TaskItem(string name, int priority)
        {
            Name = name;
            Priority = priority;
            IsDone = false;
            CreatedAt = DateTime.Now;
        }

        public override string ToString()
        {
            string status = IsDone ? "[V]" : "[ ]";
            return status + " [" + Priority + "] " + Name;
        }
    }
}

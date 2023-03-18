
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Tasks.Data {
    public static class DataSet {
        public static readonly List<TaskItem> TaskItems = new();

        private static readonly string DataPath = Environment.CurrentDirectory + @"\Saves\Save.dat";
        public static bool LoadItems() {
            TaskItems.Clear();
            List<TaskItem> items = new();
            try {
                BinaryFormatter bf = new();
                DirectoryInfo di = new(Environment.CurrentDirectory + @"\Saves");
                if (!di.Exists) di.Create();
                foreach (FileInfo f in di.GetFiles()) {
                    using (FileStream fs = new(f.FullName, FileMode.Open)) {
#pragma warning disable SYSLIB0011 // Type or member is obsolete
                        items.Add((TaskItem)bf.Deserialize(fs));
#pragma warning restore SYSLIB0011 // Type or member is obsolete
                    }
                } 
                TaskItems.AddRange(items.OrderBy(x => x.Date));
                return true;
            } catch (Exception) { }

            return false;
        }

        public static bool SaveItem(TaskItem task) {
            try {
                BinaryFormatter bf = new();
                using (FileStream fs = new(DataPath.Replace("Save.dat", $"Save{task.ID}.dat"), FileMode.Create)) {
#pragma warning disable SYSLIB0011 // Type or member is obsolete
                    bf.Serialize(fs, task);
#pragma warning restore SYSLIB0011 // Type or member is obsolete
                }
                return true;
            } catch (Exception) { }
            return false;
        }

        public static bool DeleteTask(TaskItem task) {
            try {
                TaskItems.Remove(task);
                FileInfo fi = new(Environment.CurrentDirectory + @$"\Saves\Save{task.ID}.dat");
                if (fi.Exists) fi.Delete();
                return true;
            } catch (Exception) { }
            return false;
        }
    }

    [Serializable()]
    public class TaskItem : ISerializable {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public int? RepeatDays { get; set; }
        public int? TimesSkipped { get; set; }
        public bool FirstSave { get; set; }

        public string TextDisplay { get { 
                return TimesSkipped == null ? Name : $"{Name} Skipped: {TimesSkipped}"; } }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public TaskItem(int id) {
            ID = id;
        }

        public TaskItem(SerializationInfo info, StreamingContext context) {
            ID = info.GetInt32("ID");
            Name = info.GetString("Name");
            Date = info.GetDateTime("Date");
            RepeatDays = info.GetInt32("Repeat");
            try { TimesSkipped = info.GetInt32("TimesSkipped"); } catch (Exception) { }
            FirstSave = info.GetBoolean("Saved");
            Start = ((DateTime)Date).Date;
            End = ((DateTime)Date).Date.AddDays(1).AddTicks(-1);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("ID", ID);
            info.AddValue("Name", Name);
            info.AddValue("Date", Date);
            info.AddValue("Repeat", RepeatDays);
            info.AddValue("TimesSkipped", TimesSkipped);
            info.AddValue("Saved", FirstSave);
        }
    }
}

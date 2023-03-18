using System.Timers;
using Timer = System.Timers.Timer;

namespace Tasks.Objects {
    public class ApplicationData {
        public Pages.Index Index { get; set; }

        public Timer RefreshTimer { get; set; }

        public void StartTimer() {
            if (RefreshTimer != null) return;
            RefreshTimer = new Timer(TimeSpan.FromMinutes(10));
            RefreshTimer.Elapsed += Refresh;
            RefreshTimer.Enabled = true;
            RefreshTimer.AutoReset = true;

        }

        public void Refresh(object source, ElapsedEventArgs e) {
            Index.Refresh();
        }
    }
}

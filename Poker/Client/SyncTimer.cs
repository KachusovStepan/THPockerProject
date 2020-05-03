using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Client
{
    class SyncTimer
    {
		public int Interval { get; set; }

		public SyncTimer() {
			Interval = 300;
		}

		public SyncTimer(int interval) {
			if (interval <= 0) {
				throw new InvalidOperationException();
			}
			Interval = interval;
		}

		public event Action<int> Tick;

		public void Start() {
			for (int i = 0; ; i++) {
				if (Tick != null) {
					Tick(i);
				}

				Thread.Sleep(Interval);
			}
		}
	}
}

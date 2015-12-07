﻿using UnityEngine;
using System;
using System.Threading;
using System.Collections.Generic;

public static class Concurrency {

	// Wraps func in function that catches and logs all exceptions.
	// Starts func in a new thread.
	public static void StartThread(Action func, string name, Logger logger) {
		Thread thread = new Thread(() => {
			try {
				func();
			} catch (Exception e) {
				string message = String.Format("Exception from thread \"{0}\":\n{1}",
					name,
					e.ToString());
				logger.Error(message);
				Debug.Log(message);
			}
		});
		thread.Start();
	}

	public static bool Dequeue(Queue<object> queue, out object obj) {
		lock (queue) {
			if (queue.Count > 0) {
				obj = queue.Dequeue();
				return true;
			} else {
				obj = null;
				return false;
			}
		}
	}
}

//  
//  Copyright (C) 2009 Chris Szikszoy
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
// 
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
// 

using System;
using System.Linq;
using System.Collections.Generic;

namespace Docky.Services.Logging
{
	
	public abstract class LogBase
	{
		class LogCall
		{
			public readonly LogLevel Level;
			public readonly string Message;

			public LogCall (LogLevel level, string message)
			{
				Level = level;
				Message = message;
			}
		}

		public static LogLevel DisplayLevel { get; set; }

		static bool Writing { get; set; }
		static ICollection<LogCall> PendingLogCalls { get; set; }

		static LogBase ()
		{
			Writing = false;
			PendingLogCalls = new LinkedList<LogCall> ();
		}

		public static void Write (LogLevel level, string msg, params object[] args)
		{
			if (level < DisplayLevel) return;
			
			msg = string.Format (msg, args);
			if (Writing) {
				// In the process of logging, another log call has been made.
				// We need to avoid the infinite regress this may cause.
				PendingLogCalls.Add (new LogCall (level, msg));
			} else {
				Writing = true;

				if (PendingLogCalls.Any ()) {
					// Flush delayed log calls.
					// First, swap PendingLogCalls with an empty collection so it
					// is not modified while we enumerate.
					IEnumerable<LogCall> calls = PendingLogCalls;
					PendingLogCalls = new LinkedList<LogCall> ();
	
					// Log all pending calls.
					foreach (LogCall call in calls)
							ConsoleLog.Log (call.Level, call.Message);
				}

				// Log message.
				ConsoleLog.Log (level, msg);
				
				Writing = false;
			}
		}
		
		public static void SendNote (string sender, string icon, string msg, params object[] args)
		{			
			string title = sender;
			
			if (string.IsNullOrEmpty (sender))
				title = "Docky";
			
			NotificationService.Notify (title, string.Format (msg, args), icon);
		}
	}
}

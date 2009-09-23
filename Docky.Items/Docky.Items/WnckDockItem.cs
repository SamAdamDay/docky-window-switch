//  
//  Copyright (C) 2009 Jason Smith
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Cairo;
using Gdk;
using Wnck;

using Docky;
using Docky.CairoHelper;
using Docky.Menus;
using Docky.Services;
using Docky.Windowing;

namespace Docky.Items
{


	public abstract class WnckDockItem : IconDockItem
	{
		public event EventHandler WindowsChanged;
		
		int last_raised;
		DateTime last_scroll = new DateTime (0);
		TimeSpan scroll_rate = new TimeSpan (0, 0, 0, 0, 200);
		
		IEnumerable<Wnck.Window> windows;
		public IEnumerable<Wnck.Window> Windows {
			get { return windows; }
			protected set {
				windows = value;
				SetIndicator ();
				SetState ();
				
				if (WindowsChanged != null)
					WindowsChanged (this, EventArgs.Empty);
			}
		}
		
		public IEnumerable<Wnck.Window> ManagedWindows {
			get {
				return Windows.Where (w => !w.IsSkipTasklist);
			}
		}
		
		protected string CloseIcon {
			get {
				return "close.svg@" + System.Reflection.Assembly.GetExecutingAssembly ().FullName;
			}
		}
		
		protected string MaximizeIcon {
			get {
				return "maximize.svg@" + System.Reflection.Assembly.GetExecutingAssembly ().FullName;
			}
		}
		
		protected string MinimizeIcon {
			get {
				return "minimize.svg@" + System.Reflection.Assembly.GetExecutingAssembly ().FullName;
			}
		}
		
		protected string RunIcon {
			get {
				return "run.svg@" + System.Reflection.Assembly.GetExecutingAssembly ().FullName;
			}
		}
		
		public WnckDockItem ()
		{
			Wnck.Screen.Default.ActiveWindowChanged += WnckScreenDefaultActiveWindowChanged;
		}

		void WnckScreenDefaultActiveWindowChanged (object o, ActiveWindowChangedArgs args)
		{
			SetState ();
		}
		
		void SetIndicator ()
		{
			int count = ManagedWindows.Count ();
			if (count > 1) {
				Indicator = ActivityIndicator.SinglePlus;
			} else if (count == 1) {
				Indicator = ActivityIndicator.Single;
			} else {
				Indicator = ActivityIndicator.None;
			}
		}
		
		void SetState ()
		{
			ItemState state = 0;
			
			if (Windows.Contains (Wnck.Screen.Default.ActiveWindow)) {
				state |= ItemState.Active;
			}
			
			State = state;
		}
		
		protected override void OnScrolled (Gdk.ScrollDirection direction, Gdk.ModifierType mod)
		{
			int count = ManagedWindows.Count ();
			
			if (count < 1 || (DateTime.UtcNow - last_scroll) < scroll_rate) return;
			
			last_scroll = DateTime.UtcNow;
			
			// This block will make sure that if we're scrolling on an app that is already active
			// that when we scroll we move on the next window instead of appearing to do nothing
			Wnck.Window focused = ManagedWindows.Where (w => w.IsActive).FirstOrDefault ();
			if (focused != null)
				for (; last_raised < count - 1; last_raised++)
					if (ManagedWindows.ElementAt (last_raised).Pid == focused.Pid)
						break;

			switch (direction) {
			case ScrollDirection.Up:
			case ScrollDirection.Right:
				last_raised++;
				break;
			case ScrollDirection.Down:
			case ScrollDirection.Left:
				last_raised--;
				break;
			}
			
			if (last_raised < 0)
				last_raised = count - 1;
			else if (last_raised >= count)
				last_raised = 0;

			ManagedWindows.ElementAt (last_raised).CenterAndFocusWindow ();
		}
		
		public sealed override void SetScreenRegion (Gdk.Screen screen, Gdk.Rectangle region)
		{
			foreach (Wnck.Window w in ManagedWindows) {
				w.SetIconGeometry (region.X, region.Y, region.Width, region.Height);
			}
		}
		
		protected override ClickAnimation OnClicked (uint button, ModifierType mod, double xPercent, double yPercent)
		{
			if (!ManagedWindows.Any () || button != 1)
				return ClickAnimation.None;
			
			List<Wnck.Window> stack = new List<Wnck.Window> (Wnck.Screen.Default.WindowsStacked);
			IEnumerable<Wnck.Window> windows = ManagedWindows.OrderByDescending (w => stack.IndexOf (w));
			
			bool not_in_viewport = !windows.Any (w => !w.IsSkipTasklist && w.IsInViewport (w.Screen.ActiveWorkspace));
			bool urgent = windows.Any (w => w.NeedsAttention ());
			
			if (not_in_viewport || urgent) {
				foreach (Wnck.Window window in windows) {
					if (urgent && !window.NeedsAttention ())
						continue;
					if (!window.IsSkipTasklist) {
						WindowControl.IntelligentFocusOffViewportWindow (window, windows);
						return ClickAnimation.Darken;
					}
				}
			}
			
			if (windows.Any (w => w.IsMinimized && w.IsInViewport (Wnck.Screen.Default.ActiveWorkspace))) {
				WindowControl.RestoreWindows (windows);
			} else if (windows.Any (w => w.IsActive && w.IsInViewport (Wnck.Screen.Default.ActiveWorkspace))) {
				WindowControl.MinimizeWindows (windows);
			} else {
				WindowControl.FocusWindows (windows);
			}
			
			return ClickAnimation.Darken;
		}
		
		public override IEnumerable<MenuItem> GetMenuItems ()
		{
			if (ManagedWindows.Any ()) {
				yield return new MenuItem ("Maximize", MaximizeIcon, (o, a) => WindowControl.MaximizeWindow (ManagedWindows.First ()));
				if (ManagedWindows.Any (w => w.IsMinimized))
					yield return new MenuItem ("Restore", MinimizeIcon, (o, a) => WindowControl.RestoreWindows (ManagedWindows));
				else
					yield return new MenuItem ("Minimize", MinimizeIcon, (o, a) => WindowControl.MinimizeWindows (ManagedWindows));
				yield return new MenuItem ("Close", CloseIcon, (o, a) => WindowControl.CloseWindows (ManagedWindows));
				
				yield return new SeparatorMenuItem ();
				
				foreach (Wnck.Window window in ManagedWindows) {
					yield return new WindowMenuItem (window, Icon);
				}
			}
		}
	}
}

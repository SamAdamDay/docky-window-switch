//
// AddinView.cs
//
// Author:
//   Aaron Bockover <abockover@novell.com>
//
// Copyright (C) 2008 Novell, Inc.
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Linq;
using System.Collections.Generic;
using Gtk;


namespace NPR
{
	public class StationsView : EventBox
	{
		private List<StationTile> tiles = new List<StationTile> ();
		private VBox box = new VBox ();
		
		private int selected_index = -1;
		
		public StationsView ()
		{
			CanFocus = true;
			VisibleWindow = false;
			
			box.Show ();
			Add (box);
		}
		
		public void Clear ()
		{
			tiles.Clear ();
			foreach (Widget child in box.Children) {
				box.Remove (child);
			}
		}

		private bool changing_styles = false;
		
		protected override void OnStyleSet (Style previous_style)
		{
			if (changing_styles) {
				return;
			}
			
			changing_styles = true;
			base.OnStyleSet (previous_style);
			Parent.ModifyBg (StateType.Normal, Style.Base (StateType.Normal));
			changing_styles = false;
		}
		
		
		public void AppendStation (Station station)
		{
			StationTile tile = new StationTile (station);
			tile.ActiveChanged += OnStationActiveChanged;
			tile.SizeAllocated += OnStationSizeAllocated;
			tile.Show ();
			tiles.Add (tile);
			
			box.PackStart (tile, false, false, 0);
		}

		
		private void OnStationActiveChanged (object o, EventArgs args)
		{
			StationTile tile = o as StationTile;
			List<int> stations = NPR.MyStations.ToList ();
			if (stations.Contains ((int)tile.OwnedObject.ID))
				stations.Remove ((int)tile.OwnedObject.ID);
			else
				stations.Add ((int)tile.OwnedObject.ID);
			
			NPR.MyStations = stations.ToArray ();			
			
			foreach (StationTile stationTile in tiles) {
				stationTile.UpdateState ();
			}
		}
		
		private void OnStationSizeAllocated (object o, SizeAllocatedArgs args)
		{
			ScrolledWindow scroll;
			
			if (Parent == null || (scroll = Parent.Parent as ScrolledWindow) == null) {
				return;
			}
			
			StationTile tile = (StationTile)o;
			
			if (tiles.IndexOf (tile) != selected_index) {
				return;
			}
			
			Gdk.Rectangle ta = ((StationTile)o).Allocation;
			Gdk.Rectangle va = new Gdk.Rectangle (0, (int)scroll.Vadjustment.Value, 
			                                      Allocation.Width, Parent.Allocation.Height);
			
			if (!va.Contains (ta)) {
				double delta = 0.0;
				if (ta.Bottom > va.Bottom) {
					delta = ta.Bottom - va.Bottom;
				} else if (ta.Top < va.Top) {
					delta = ta.Top - va.Top;
				}
				scroll.Vadjustment.Value += delta;
				QueueDraw();
			}
		}
		
		protected override bool OnButtonPressEvent (Gdk.EventButton evnt)
		{
			HasFocus = true;
			
			ClearSelection ();
			
			for (int i = 0; i < tiles.Count; i++) {
				if (tiles[i].Allocation.Contains ((int)evnt.X, (int)evnt.Y)) {
					Select (i);
					break;
				}
			}
			
			QueueDraw ();
			
			return base.OnButtonPressEvent (evnt);
		}
		
		protected override bool OnKeyPressEvent (Gdk.EventKey evnt)
		{
			int index = selected_index;
			
			switch (evnt.Key) {
			case Gdk.Key.Up:
			case Gdk.Key.uparrow:
				index--;
				if (index < 0) {
					index = 0;
				}
				break;
			case Gdk.Key.Down:
			case Gdk.Key.downarrow:
				index++;
				if (index > tiles.Count - 1) {
					index = tiles.Count - 1;
				}
				break;
			}
			
			if (index != selected_index) {
				ClearSelection ();
				Select (index);
				return true;
			}
			
			return base.OnKeyPressEvent (evnt);
		}
		
		private void Select (int index)
		{
			if (index >= 0 && index < tiles.Count) {
				selected_index = index;
				tiles[index].Select (true);
			} else {
				ClearSelection ();
			}
			
			if (Parent != null && Parent.IsRealized) {
				Parent.GdkWindow.InvalidateRect (Parent.Allocation, true);
			}
			
			QueueResize ();
		}
		
		private void ClearSelection ()
		{
			if (selected_index >= 0 && selected_index < tiles.Count) {
				tiles[selected_index].Select (false);
			}
			
			selected_index = -1;
		}
	}
}

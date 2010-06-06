//  
//  Copyright (C) 2009 Chris Szikszoy
//  Copyright (C) 2010 Robert Dyer
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

using GLib;

using Docky.Items;
using Docky.Services;

namespace Mounter
{
	public class MountProvider : AbstractDockItemProvider
	{
		#region AbstractDockItemProvider implementation
		
		public override string Name {
			get { return "Mount"; }
		}
		
		public override bool AutoDisable {
			get { return false; }
		}
		
		public override string Icon {
			get { return "drive-removable-media-usb;;drive-removable-media"; }
		}
		
		#endregion
		
		List<MountItem> Mounts = new List<MountItem> ();
		
		public VolumeMonitor Monitor { get; private set; }
		
		public MountProvider ()
		{
			Monitor = VolumeMonitor.Default;

			foreach (Mount m in Monitor.Mounts) {
				if (IsTrash (m))
					continue;
				Mounts.Add (new MountItem (m));
				Log<MountProvider>.Debug ("Adding {0}.", m.Name);
			}
			
			Monitor.MountAdded += HandleMountAdded;
			Monitor.MountRemoved += HandleMountRemoved;
		
			Items = Mounts.Cast<AbstractDockItem> ();
		}
		
		void HandleMountAdded (object o, MountAddedArgs args)
		{
			// FIXME: due to a bug in GIO#, this will crash when trying to get args.Mount
			Mount m = MountAdapter.GetObject (args.Args[0] as GLib.Object);
			
			if (IsTrash (m))
				return;
			
			Mounts.Add (new MountItem (m));
			Log<MountProvider>.Info ("{0} mounted.", m.Name);
			Items = Mounts.Cast<AbstractDockItem> ();
		}		
		
		void HandleMountRemoved (object o, MountRemovedArgs args)
		{
			// FIXME: due to a bug in GIO#, this will crash when trying to get args.Mount
			Mount m = MountAdapter.GetObject (args.Args[0] as GLib.Object);
			
			if (!IsTrash (m) && Mounts.Any (d => d.Mnt.Handle == m.Handle)) {
				MountItem mntToRemove = Mounts.First (d => d.Mnt.Handle == m.Handle);
				
				Mounts.Remove (mntToRemove);
				Items = Mounts.Cast<AbstractDockItem> ();
				mntToRemove.Dispose ();
				
				Log<MountProvider>.Info ("{0} unmounted.", m.Name);
			}
		}
		
		// determine if the mount should be handled or not
		bool IsTrash (Mount m)
		{			
			return m == null || (m.Volume == null && m.Root != null && m.Root.Path != null && m.Root.Path.Contains ("cdda"));
		}
		
		public override bool RemoveItem (AbstractDockItem item)
		{
			(item as MountItem).UnMount ();
			return base.RemoveItem (item);
		}
		
		public override void Dispose ()
		{
			base.Dispose ();
			
			Monitor.MountAdded -= HandleMountAdded;
			Monitor.MountRemoved -= HandleMountRemoved;
		}
	}
}

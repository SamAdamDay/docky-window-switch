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
using System.Collections.Generic;
using System.Text.RegularExpressions;

using GLib;

using Docky.Items;
using Docky.Menus;
using Docky.Services;

namespace Mounter
{
	
	public class MountItem : FileDockItem
	{
		
		#region IconDockItem implementation
		
		public override string UniqueID ()
		{
			return Mnt.Handle.ToString ();
		}
		
		#endregion
		
		public MountItem (Mount mount) : base (mount.Root.StringUri ())
		{
			Mnt = mount;
			
			SetIconFromGIcon (mount.Icon);
			
			HoverText = Mnt.Name;
			
			Mnt.Changed += (o, a) => SetIconFromGIcon (Mnt.Icon);
		}
		
		public Mount Mnt { get; private set; }
		
		protected override ClickAnimation OnClicked (uint button, Gdk.ModifierType mod, double xPercent, double yPercent)
		{
			if (button == 1) {
				Open ();
				return ClickAnimation.Bounce;
			}
			
			return ClickAnimation.None;
		}
		
		public void UnMount ()
		{
			if (Mnt.CanEject ())
				Mnt.EjectWithOperation (MountUnmountFlags.Force, new Gtk.MountOperation (null), null, (s, result) =>
				{
					try {
						if (!Mnt.EjectWithOperationFinish (result))
							Log<MountItem>.Error ("Failed to eject {0}", Mnt.Name);
					} catch (Exception e) {
						Log<MountItem>.Error ("An error when ejecting {0} was encountered: {1}", Mnt.Name, e.Message);
						Log<MountItem>.Debug (e.StackTrace);
					}
				});
			else
				Mnt.UnmountWithOperation (MountUnmountFlags.Force, new Gtk.MountOperation (null), null, (s, result) =>
				{
					try {
						if (!Mnt.UnmountWithOperationFinish (result))
							Log<MountItem>.Error ("Failed to unmount {0}", Mnt.Name);
					} catch (Exception e) {
						Log<MountItem>.Error ("An error when unmounting {0} was encountered: {1}", Mnt.Name, e.Message);
						Log<MountItem>.Debug (e.StackTrace);
					}
				});
		}
		
		public override MenuList GetMenuItems ()
		{
			MenuList list = new MenuList ();
			list[MenuListContainer.Actions].Add (new MenuItem ("Open", Icon, (o, a) => Open ()));
			if (Mnt.CanEject () || Mnt.CanUnmount) {
				string removeLabel = (Mnt.CanEject ()) ? "Eject" : "Unmount";
				list[MenuListContainer.Actions].Add (new MenuItem (removeLabel, "media-eject", (o, a) => UnMount ()));
			}
			
			return list;
		}
	}
}

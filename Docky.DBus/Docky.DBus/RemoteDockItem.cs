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

namespace Docky.DBus
{


	public class RemoteDockItem : RemoteItem
	{
		public event EventHandler<Gtk.ButtonReleaseEventArgs> ButtonReleased;
		
		public string HoverText {get; private set;}
		public string Icon { get; private set; }
	
		public RemoteDockItem (uint id, string hovertext, string icon) : base (id)
		{
			HoverText = hovertext;
			Icon = icon;
		}
		
		public void OnButtonReleased (Gtk.ButtonReleaseEventArgs args)
		{
			if (ButtonReleased != null)
				ButtonReleased (this, args);
		}
	}
}

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
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Cairo;
using Gdk;
using Gtk;

namespace Docky.Items
{


	public class ApplicationDockItemProvider : IDockItemProvider
	{

		#region IDockItemProvider implementation
		public bool ItemCanBeMoved (AbstractDockItem item)
		{
			throw new System.NotImplementedException();
		}
		
		public bool ItemCanBeRemoved (AbstractDockItem item)
		{
			throw new System.NotImplementedException();
		}
		
		public bool MoveItem (AbstractDockItem item, int position)
		{
			throw new System.NotImplementedException();
		}
		
		public bool RemoveItem (AbstractDockItem item)
		{
			throw new System.NotImplementedException();
		}
		
		public ReadOnlyCollection<AbstractDockItem> Items {
			get {
				throw new System.NotImplementedException();
			}
		}
		#endregion

		public ApplicationDockItemProvider ()
		{
		}
	}
}

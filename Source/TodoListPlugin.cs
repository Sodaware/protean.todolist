/**
 * Copyright (c) 2007, Sodaware
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without modification, 
 * are permitted provided that the following conditions are met:
 * 
 *     * Redistributions of source code must retain the above copyright notice, 
 * 	       this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright notice, 
 * 	       this list of conditions and the following disclaimer in the documentation 
 * 	       and/or other materials provided with the distribution.
 *     * Neither the name of the <ORGANIZATION> nor the names of its contributors 
 * 	       may be used to endorse or promote products derived from this software 
 * 	       without specific prior written permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
 * IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
 * PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
 * POSSIBILITY OF SUCH DAMAGE. 
 */

using System;

namespace Protean.Plugins.TodoList
{

	/// <summary>Main plugin class. Initialises plugin and registers it with Protean.</summary>
	public class TodoListPlugin : Protean.Hub.Plugins.Plugin
	{
		private static DockingPages.frm_TodoList dockingPage;

		/// <summary>Registers the plugin with Protean, and sets up event handlers.</summary>
		public TodoListPlugin()
		{
			// Initialisation & Content Setup
			this.Initialize		+= new Protean.Hub.Plugins.Plugin.InitializeEventHandler(this.Plugin_Initialize);
			this.ContentSetup	+= new Protean.Hub.Plugins.Plugin.ContentSetupEventHandler(this.Plugin_ContentSetup);
			
			// Project loaded event handler
			Protean.Hub.Projects.Globals.Projects.Inserted += new Tarro.Collections.CollectionWithEvents.InsertedEventHandler(this.Projects_Inserted);
		}

		/// <summary>Initialises all of the plugin's data. Called when the plugin is loaded by Protean.</summary>
		internal void Plugin_Initialize(object sender, Protean.Hub.Plugins.Plugin.InitializeArgs e)
		{
			e.Information.Name		= "TO-DO list";
			e.Information.GUID		= "C6CC0646-BDE5-4058-AEE4-28F2BCE9419E";
			e.Information.Priority	= Protean.Hub.Plugins.PriorityTypes.GeneralTools;
		}

		/// <summary>Sets up the content of the plugin..</summary>
		internal void Plugin_ContentSetup(object sender, EventArgs e)
		{
			Protean.Hub.Splash.Actions.Write("Initialising Task list");

			// Register docking pages
			TodoListPlugin.dockingPage = new Protean.Plugins.TodoList.DockingPages.frm_TodoList();
			TodoListPlugin.dockingPage.Register();

			// Register auto-update sites
			// Protean.Hub.PluginUpdater.UpdateManager.Register(this, "http://127.0.0.1/test.xml");
		}

		/// <summary>STUB. Sets the current project to the newly added project.</summary>
		private void Projects_Inserted(object sender, int index, object value)
		{
			Protean.Hub.Projects.Project insertedProject = (Protean.Hub.Projects.Project)value;
		}

		/// <summary>
		/// Add an item to the docking page control. Horrible.
		/// </summary>
		/// <param name="itemType"></param>
		/// <param name="lineNumber"></param>
		/// <param name="description"></param>
		/// <param name="sourceFile"></param>
		internal static void AddItem(DockingPages.frm_TodoList.ItemType itemType, int lineNumber, string description, Protean.Hub.Files.File sourceFile)
		{
			TodoListPlugin.dockingPage.AddItem(itemType, lineNumber, description, sourceFile);
		}

	}

}
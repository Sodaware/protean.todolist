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
using Protean.Plugins.TodoList.DockingPages;

namespace Protean.Plugins.TodoList
{
	/// <summary>Manages the parsing of source code for todo tags.</summary>
	public class SourceParser
	{
		/// <summary>Fired when parsing of the project has finished.</summary>
		public static event ParsingFinishedEventHandler ParsingFinished;
		public delegate void ParsingFinishedEventHandler(Object sender, System.EventArgs e);

		/// <summary>Parse all of the code loaded into Protean and populate the todo list.</summary>
		public static void Parse()
		{
			// Iterate through each document loaded into Protean - Parse them if they're valid
			foreach(Protean.Hub.Documents.Document tDocument in Protean.Hub.Documents.Globals.Documents)
			{
				if(tDocument != null && tDocument.Document != null && tDocument.Document.Lines.Length > 0) 
				{
					SourceParser.ParseDocument(tDocument);
				}
			}

			// Fire event when finished
			SourceParser.ParsingFinished(null, null);
		}

		/// <summary>Parse a single Protean document.</summary>
		/// <param name="doc"></param>
		private static void ParseDocument(Protean.Hub.Documents.Document doc)
		{
			int lineNumber = 1;
			foreach(string tLine in doc.Document.Lines) 
			{
				string description	= "";
				bool itemFound		= false;
				frm_TodoList.ItemType itemType = frm_TodoList.ItemType.Temp;

				// Refactor this to get the first keyword, then do a "switch" on it

				if (tLine != null && tLine.Length > 0 ) 
				{
					// TODO:					
					if (SourceParser.findKeyword("todo", tLine) != "") 
					{
						description	= SourceParser.findKeyword("todo", tLine);
						itemType	= frm_TodoList.ItemType.Todo;
						itemFound	= true;
					}

						// TEMP:					
					else if (SourceParser.findKeyword("temp", tLine) != "") 
					{
						description	= SourceParser.findKeyword("temp", tLine);
						itemType	= frm_TodoList.ItemType.Temp;
						itemFound	= true;
					}

						// HACK:					
					else if (SourceParser.findKeyword("hack", tLine) != "") 
					{
						description	= SourceParser.findKeyword("hack", tLine);
						itemType	= frm_TodoList.ItemType.Hack;
						itemFound	= true;
					}

						// KLUDGE:					
					else if (SourceParser.findKeyword("kludge", tLine) != "") 
					{
						description	= SourceParser.findKeyword("kludge", tLine);
						itemType	= frm_TodoList.ItemType.Kludge;
						itemFound	= true;
					}

						// NOTE:					
					else if (SourceParser.findKeyword("note", tLine) != "") 
					{
						description	= SourceParser.findKeyword("note", tLine);
						itemType	= frm_TodoList.ItemType.Note;
						itemFound	= true;
					}
					// If we found something, add it
					if(itemFound) 
					{
						TodoListPlugin.AddItem(itemType, lineNumber, description, doc.File); 
					}
					
				}

				lineNumber++;
			}
		}

		private static string findKeyword(string keyword, string sourceLine)
		{
			string rString		= "";
			string lowerLine	= sourceLine.ToLower();
			
			if (lowerLine.IndexOf("; " + keyword + ":") > -1) {
				rString	= sourceLine.Substring(lowerLine.IndexOf("; " + keyword.ToLower() + ":") + 4 + keyword.Length);
			}

			if (lowerLine.IndexOf(";" + keyword + ":") > -1) {
				rString	= sourceLine.Substring(lowerLine.IndexOf(";" + keyword.ToLower() + ":") + 3 + keyword.Length);
			}

			return rString;
		}
	}
}

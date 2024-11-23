// See https://aka.ms/new-console-template for more information
using System.Net.WebSockets;
using HALLToDoList.ConsoleApp;
using HALLToDoList.Database.Models;

Console.WriteLine("Hello, World!");

EFCore efCore = new EFCore();

 efCore.GetLists(); 
// Get all the todo lists

efCore.GetList();
// request (int id) and get the one list with specific id

efCore.CreateList();
// request (string title) and create new to do list, automatically set the status to "idle"

efCore.SetComplete();
// request (int id) and set the specific list by that id to "complete"

efCore.SetPending();
// request (int id) and set the specific list by that id to "pending"

efCore.SetDue();
// request (int id) and set the specific list by that id to "due"

efCore.SetIdle();
// request (int id) and set the specific list by that id to "idle"

efCore.GetCompletedLists();
// get all the list which status are "complete"

efCore.GetIdleLists();
// get all the list which status are "complete"

efCore.GetDueLists();
// get all the list which status are "complete"

efCore.GetPendingLists();
// get all the list which status are "complete"
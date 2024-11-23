using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HALLToDoList.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace HALLToDoList.ConsoleApp
{
    public class EFCore
    {
        private readonly AppDbContext _db;
        public EFCore()
        {
            _db = new AppDbContext();
        }
        public void GetLists()
        {
            var lst = _db.TblLists.AsNoTracking().ToList();
            if(lst.Count == 0)
            {
                Console.WriteLine("No data found."); return;
            }
            foreach(var item in lst)
            {
                Print(item);
            }
        }
        public void GetList()
        {
            int? id = ReadId();
            if (id is null)
            {
                Console.WriteLine("Id is required."); return;
            }
            var item = _db.TblLists.AsNoTracking().FirstOrDefault(x => x.TaskId == id);
            if (item is null)
            {
                Console.WriteLine($"No data is found by provided id {id}");
            }
            else
            {
                Print(item);
            }
        }
        public void GetCompletedLists()
        {
            var lst = _db.TblLists.AsNoTracking().Where(x => x.Status == "complete").ToList();
            if(lst.Count == 0)
            {
                Console.WriteLine("No data found"); return;
            }
            foreach (var item in lst)
            {
                Print(item);
            }
        }
        public void GetPendingLists()
        {
            var lst = _db.TblLists.AsNoTracking().Where(x => x.Status == "pending").ToList();
            if (lst.Count == 0)
            {
                Console.WriteLine("No data found"); return;
            }
            foreach (var item in lst)
            {
                Print(item);
            }
        }
        public void GetIdleLists()
        {
            var lst = _db.TblLists.AsNoTracking().Where(x => x.Status == "idle").ToList();
            if (lst.Count == 0)
            {
                Console.WriteLine("No data found"); return;
            }
            foreach (var item in lst)
            {
                Print(item);
            }
        }
        public void GetDueLists()
        {
            var lst = _db.TblLists.AsNoTracking().Where(x => x.Status == "due").ToList();
            if (lst.Count == 0)
            {
                Console.WriteLine("No data found"); return;
            }
            foreach (var item in lst)
            {
                Print(item);
            }
        }
        public void CreateList()
        {
            string title = ReadTitle();
            if(String.IsNullOrEmpty(title))
            {
                Console.WriteLine("List title is required.");
            }
            TblList list = new TblList { TaskTitle = title, Status = "idle" };
            _db.TblLists.Add(list);
            int result = _db.SaveChanges();
            Console.WriteLine(result == 0 ? "Created failed." : "Successfully Created.");
        }
        public void UpdateTitle()
        {
            int?  id = ReadId();
            if(id is null)
            {
                Console.WriteLine("Id is required."); return;
            }
            var item = _db.TblLists.AsNoTracking().FirstOrDefault(x => x.TaskId == id);
            if(item is null)
            {
                Console.WriteLine($"No data is found by provided id {id}");
            } else
            {
                string title = ReadTitle();
                if(title is null)
                {
                    Console.WriteLine("List title is required."); return;
                }
                item.TaskTitle = title;
                _db.Entry(item).State = EntityState.Modified;
                int result = _db.SaveChanges();
                Console.WriteLine(result == 0 ? "Created failed." : "Created Successfully.") ;
            }
        }
        public void SetPending()
        {
            int? id = ReadId();
            if (id is null)
            {
                Console.WriteLine("Id is required."); return;
            }
            var item = _db.TblLists.AsNoTracking().FirstOrDefault(x => x.TaskId == id);
            if(item is null)
            {
                Console.WriteLine($"No data is found by provided id {id}");
            } else
            {
                item.Status = "pending";
                _db.Entry(item).State = EntityState.Modified;
                int result = _db.SaveChanges();
                Console.WriteLine(result == 0 ? "Saved as pending." : "failed to save as pending.");
            }
        }
        public void SetIdle()
        {
            int? id = ReadId();
            if (id is null)
            {
                Console.WriteLine("Id is required."); return;
            }
            var item = _db.TblLists.AsNoTracking().FirstOrDefault(x => x.TaskId == id);
            if (item is null)
            {
                Console.WriteLine($"No data is found by provided id {id}");
            }
            else
            {
                item.Status = "idle";
                _db.Entry(item).State = EntityState.Modified;
                int result = _db.SaveChanges();
                Console.WriteLine(result == 0 ? "Saved as idle." : "failed to save as idle.");
            }
        }
        public void SetComplete()
        {
            int? id = ReadId();
            if (id is null)
            {
                Console.WriteLine("Id is required."); return;
            }
            var item = _db.TblLists.AsNoTracking().FirstOrDefault(x => x.TaskId == id);
            if (item is null)
            {
                Console.WriteLine($"No data is found by provided id {id}");
            }
            else
            {
                item.Status = "complete";
                _db.Entry(item).State = EntityState.Modified;
                int result = _db.SaveChanges();
                Console.WriteLine(result == 0 ? "Saved as complete." : "failed to save as compelete.");
            }
        }
        public void SetDue()
        {
            int? id = ReadId();
            if (id is null)
            {
                Console.WriteLine("Id is required."); return;
            }

            var item = _db.TblLists.AsNoTracking().FirstOrDefault(x => x.TaskId == id);
            if (item is null)
            {
                Console.WriteLine($"No data is found by provided id {id}");
            }
            else
            {
                item.Status = "due";
                _db.Entry(item).State = EntityState.Modified;
                int result = _db.SaveChanges();
                Console.WriteLine(result == 0 ? "Saved as due." : "failed to save as due.");
            }
        }
        private void Print(TblList item)
        {
            Console.WriteLine($"{item.TaskId} {item.TaskTitle} {item.Status}");
        }

        private int? ReadId()
        {
            Console.Write("Enter the list id = ");
            string id = Console.ReadLine();
            return id is null ? null : Convert.ToInt32(id);
        }

        private string ReadTitle()
        {
            Console.Write("Enter the list title = ");
            string title = Console.ReadLine();
            return title;
        }

    }
}

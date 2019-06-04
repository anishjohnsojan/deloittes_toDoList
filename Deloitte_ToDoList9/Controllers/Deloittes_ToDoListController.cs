using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Deloitte_ToDoList9.Models;
using Microsoft.AspNet.Identity;

namespace Deloitte_ToDoList9.Controllers
{
    public class Deloittes_ToDoListController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Deloittes_ToDoList
        public ActionResult Index()
        {
            return View();
        }

        public ApplicationUser Get_CurrentUser()
        {
            string get_Curr_UserId = User.Identity.GetUserId();
            ApplicationUser curr_User = db.Users.FirstOrDefault(
                user => user.Id == get_Curr_UserId);
            return curr_User;
        }

        public IEnumerable<Deloittes_ToDoList> GetUsersToDoList()
        {
            return (db.ToDoLists.ToList().Where(user => user.ToDoList_User == Get_CurrentUser()));
        }
        public ActionResult Get_ToDo_List()
        {
            return PartialView("_ToDoList", GetUsersToDoList());
        }

        // GET: Deloittes_ToDoList/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deloittes_ToDoList deloittes_ToDoList = db.ToDoLists.Find(id);
            if (deloittes_ToDoList == null)
            {
                return HttpNotFound();
            }
            return View(deloittes_ToDoList);
        }

        // GET: Deloittes_ToDoList/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Task_Id,Task,Task_Description")] Deloittes_ToDoList deloittes_ToDoList)
        {
            if (ModelState.IsValid)
            {
               
                deloittes_ToDoList.ToDoList_User = Get_CurrentUser();
                deloittes_ToDoList.Task_IsChecked = false;
                deloittes_ToDoList.Task_Date = DateTime.Now;
                deloittes_ToDoList.Task_LastUpdated_Date = DateTime.Now;
                db.ToDoLists.Add(deloittes_ToDoList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deloittes_ToDoList);
        }

        // POST: Deloittes_ToDoList/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ajax_CreateNewtoDo([Bind(Include = "Task_Id,Task,Task_Description")] Deloittes_ToDoList deloittes_ToDoList)
        {
            if (ModelState.IsValid)
            {

                deloittes_ToDoList.ToDoList_User = Get_CurrentUser();
                deloittes_ToDoList.Task_IsChecked = false;
                deloittes_ToDoList.Task_Date = DateTime.Now;
                deloittes_ToDoList.Task_LastUpdated_Date = DateTime.Now;
                db.ToDoLists.Add(deloittes_ToDoList);
                db.SaveChanges();
                return PartialView("_ToDoList", GetUsersToDoList());
                //return RedirectToAction("Index");
            }
            return PartialView("_ToDoList", GetUsersToDoList());

        }

        // GET: Deloittes_ToDoList/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deloittes_ToDoList deloittes_ToDoList = db.ToDoLists.Find(id);
            if (deloittes_ToDoList == null)
            {
                return HttpNotFound();
            }
            ApplicationUser curr_user = Get_CurrentUser();
            if(deloittes_ToDoList.ToDoList_User!=curr_user)
                { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            return View(deloittes_ToDoList);
        }

        // POST: Deloittes_ToDoList/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Task_Id,Task,Task_Description,Task_Date,Task_IsChecked,Task_LastUpdated_Date,ToDoList_User")] Deloittes_ToDoList deloittes_ToDoList)
        {
            if (ModelState.IsValid)
            {
                Deloittes_ToDoList deloittes_ToDoList_temp = db.ToDoLists.Find(deloittes_ToDoList.Task_Id);
                deloittes_ToDoList_temp.Task_IsChecked = deloittes_ToDoList.Task_IsChecked;
                deloittes_ToDoList_temp.Task = deloittes_ToDoList.Task;
                deloittes_ToDoList_temp.Task_Description = deloittes_ToDoList.Task_Description;
                deloittes_ToDoList_temp.Task_LastUpdated_Date = DateTime.Now;
                deloittes_ToDoList = deloittes_ToDoList_temp;
                db.Entry(deloittes_ToDoList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deloittes_ToDoList);
        }


        [HttpPost]
        public ActionResult Ajax_EditToDoTask(int? id, bool val)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deloittes_ToDoList deloittes_ToDoList = db.ToDoLists.Find(id);
            if (deloittes_ToDoList == null)
            {
                return HttpNotFound();
            }
            else { deloittes_ToDoList.Task_IsChecked = val;
                db.Entry(deloittes_ToDoList).State = EntityState.Modified;
                db.SaveChanges();
            }

            return PartialView("_ToDoList", GetUsersToDoList());
        }

        // GET: Deloittes_ToDoList/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deloittes_ToDoList deloittes_ToDoList = db.ToDoLists.Find(id);
            if (deloittes_ToDoList == null)
            {
                return HttpNotFound();
            }
            return View(deloittes_ToDoList);
        }

        // POST: Deloittes_ToDoList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Deloittes_ToDoList deloittes_ToDoList = db.ToDoLists.Find(id);
            db.ToDoLists.Remove(deloittes_ToDoList);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

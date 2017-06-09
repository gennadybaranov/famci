using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using STACK.Models;
using Microsoft.AspNet.Identity;
using STACK.ViewModels;

namespace STACK.Controllers
{
    public class QuestionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Questions
        public ActionResult Index()
        {
            return View(db.Questions.ToList());
        }

        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            var qa = new QuestionAnswerViewModel
            {
                Question = question,
                NewAnswer = new Answer()
            };
            qa.NewAnswer.QuestionId = qa.Question.Id;
            return View(qa);
        }

        // GET: Questions/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Questions/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Question question)
        {
            if (ModelState.IsValid)
            {

                question.ApplicationUserId = User.Identity.GetUserId();
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = question.Id });
            }

            return View(question);
        }

        // GET: Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Question question = db.Questions.Find(id);
                if (question == null)
                {
                    return HttpNotFound();
                }
                return View(question);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }           
        }

        // POST: Questions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Question question)
        {
            if (ModelState.IsValid)
            {
                question.ApplicationUserId = User.Identity.GetUserId();
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(question);
        }

        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
        {

            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Question question = db.Questions.Find(id);
                if (question == null)
                {
                    return HttpNotFound();
                }
                return View(question);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }            
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //GET : Questions/Answer/5
        public ActionResult Answer(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                Answer answer = new Answer();
                answer.QuestionId = id;
                return View(answer);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Answer(Answer answer)
        {
            if (ModelState.IsValid)
            {
                answer.ApplicationUserId = User.Identity.GetUserId();
                db.Answers.Add(answer);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = answer.QuestionId });
            }

            return View(answer);
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

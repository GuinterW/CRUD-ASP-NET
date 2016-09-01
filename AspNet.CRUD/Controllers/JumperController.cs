using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using AspNet.CRUD.Models;
using AspNet.CRUD;

namespace ProjetoCRUD.Controllers
{
    public class JumperController : Controller
    {
        private DBComponentsEntities db = new DBComponentsEntities();

        public ActionResult Index()
        {
            Jumpers[] jumpers = null;

            using (var db = new DBComponentsEntities())
            {
                jumpers = db.Jumpers.ToArray();
            }

            return View("Index", model: jumpers);
        }

        public ActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Adicionar(Jumpers jumps)
        {
            if (ModelState.IsValid)
            {
                Jumpers jumper = new Jumpers()
                {
                    QntdJumpers = jumps.QntdJumpers,
                    TypeJumpers = jumps.TypeJumpers
                };
                db.Jumpers.Add(jumper);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jumps);
        }

        public ActionResult Editar(int id)
        {
            Jumpers jumper = db.Jumpers.FirstOrDefault(x => x.IDJumpers == id);
            if (jumper == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
            return View(jumper);
        }

        [HttpPost]
        public ActionResult Editar(Jumpers jumper)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jumper).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jumper);
        }

        public ActionResult Visualizar(long id)
        {
            Jumpers jumper = db.Jumpers.FirstOrDefault(x => x.IDJumpers == id);
            if (jumper == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
            return View(jumper);
        }

        public string Excluir(long id)
        {
            try
            {
                Jumpers jumper = db.Jumpers.Find(id);
                db.Jumpers.Remove(jumper);
                db.SaveChanges();
                // return Boolean.TrueString;
                return RedirectToAction("Index").ToString();
            }
            catch
            {
                return Boolean.FalseString;
            }
        }
    }
}

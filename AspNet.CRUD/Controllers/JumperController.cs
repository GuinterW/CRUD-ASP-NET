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

        [HttpGet]
        public ActionResult Index()
        {
            return View("Index");
        }

        //Paginação 10 itens por página

        [HttpPost]
        public ActionResult Index(string search, int pageIndex = 0)
        {
            Jumpers[] jumpers = null;

            using (var db = new DBComponentsEntities())
            {
                var query = db.Jumpers.Where(x => (search != null && x.TypeJumpers.Contains(search)) || search == null);

                var count = query.Count();
                var items = query.OrderByDescending(x => x.IDJumpers).Skip(0).Take(10);

                jumpers = items.ToArray();

                ViewBag.Count = 10;
            }

            return PartialView("_Listagem", jumpers);
        }

        [HttpGet]
        public ActionResult Adicionar()
        {
            return PartialView("Adicionar");
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
                return Json(new { Success = true });
            }

            return Json(new { Success = false });
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

        [HttpGet]
        public ActionResult ExcluirConfirmacao(long id)
        {
            return PartialView("_Excluir", id);
        }

        [HttpPost]
        public ActionResult Excluir(long id)
        {
            Jumpers jumper = db.Jumpers.Find(id);
            db.Jumpers.Remove(jumper);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

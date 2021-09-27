using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAppFront.Infra;
using TodoAppFront.Models;

namespace TodoAppFront.Controllers
{
    public class TarefasController : Controller
    {
        private readonly TodoRestClient restClient;

        public TarefasController()
        {
            this.restClient = new TodoRestClient();
        }
        // GET: AdicionarTarefa
        public ActionResult Index()
        {
            var model = this.restClient.GetAll();
            return View(model);
        }

        // GET: AdicionarTarefa/Details/5
        public ActionResult Details(Guid id)
        {
            var model = this.restClient.GetTaskById(id);
            return View(model);
        }

        // GET: AdicionarTarefa/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdicionarTarefa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TodoModel model)
        {
            try
            {
                this.restClient.SaveTask(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdicionarTarefa/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = this.restClient.GetTaskById(id);
            return View(model);
        }

        // POST: AdicionarTarefa/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, TodoModel model)
        {
            try
            {
                this.restClient.UpdateTask(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdicionarTarefa/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = this.restClient.GetTaskById(id);
            return View(model);
        }

        // POST: AdicionarTarefa/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, TodoModel model)
        {
            try
            {
                this.restClient.DeleteTask(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

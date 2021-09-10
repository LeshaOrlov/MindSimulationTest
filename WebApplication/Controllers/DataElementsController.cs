using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WebApplication.Helpers;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class DataElementsController : Controller
    {
        private Data _data;
        private readonly DataManager dataManager;

        public DataElementsController(DataManager manager)
        {
            dataManager = manager;
            _data = manager.data;
        }

        // GET: DataElementsList
        public IActionResult Index()
        {
            return View( _data.ElementsList);
        }

        // GET: DataElementsList/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataElement =  _data.ElementsList
                .FirstOrDefault(m => m.Element == id);
            if (dataElement == null)
            {
                return NotFound();
            }

            return View(dataElement);
        }

        // GET: DataElementsList/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DataElementsList/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Element,ElementInfo,IsElement")] DataElement dataElement)
        {
            if (ModelState.IsValid)
            {
                _data.ElementsList.Add(dataElement);
                return RedirectToAction(nameof(Index));
            }
            return View(dataElement);
        }

        // GET: DataElementsList/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataElement =  _data.ElementsList.FirstOrDefault(m => m.Element == id);
            if (dataElement == null)
            {
                return NotFound();
            }
            return View(dataElement);
        }

        // POST: DataElementsList/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("Element,ElementInfo,IsElement")] DataElement dataElement)
        {
            if (id != dataElement.Element)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var index = _data.ElementsList.FindIndex(m => m.Element == id);

                    _data.ElementsList[index] = dataElement;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DataElementExists(dataElement.Element))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dataElement);
        }

        // GET: DataElementsList/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataElement =  _data.ElementsList
                .FirstOrDefault(m => m.Element == id);
            if (dataElement == null)
            {
                return NotFound();
            }

            return View(dataElement);
        }

        // POST: DataElementsList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var dataElement =  _data.ElementsList.FirstOrDefault(m => m.Element == id);
            _data.ElementsList.Remove(dataElement);
            return RedirectToAction(nameof(Index));
        }

        private bool DataElementExists(string id)
        {
            return _data.ElementsList.Any(e => e.Element == id);
        }


        //функции для работы с файлом json
        public IActionResult SaveNewJsonFile()
        {
            string filepath = "Files\\NewFile.json";
            dataManager.Save(_data, filepath);
            FileStream fs = new FileStream(filepath, FileMode.Open);
            return File(fs, "application/json", filepath);
        }

        public IActionResult ResaveJsonFile()
        {
            dataManager.Save(_data, "Files\\testJson.json");
            return Redirect("Index");
        }

        //функции для работы с базой данных
        public IActionResult SaveNewDbFile()
        {
            string filepath = "Files\\NewDb.db";
            dataManager.SaveToDB(_data, "Files\\NewDb.db");
            FileStream fs = new FileStream(filepath, FileMode.Open);
            return File(fs, "application/x-binary", filepath);
        }
    }
}

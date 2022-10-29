using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mvc1.Models;
using mvc1.Services;

namespace mvc1.Controllers
{
    public class RookiesController : Controller
    {
        private readonly ILogger<RookiesController> _logger;
        private readonly IPersonService _service;

        public RookiesController(ILogger<RookiesController> logger, IPersonService service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            List<PersonModel> people = _service.GetAll();
            return View(people);
        }

        public IActionResult GetAll()
        {
            List<PersonModel> people = _service.GetAll();
            return View(people);
        }

        public IActionResult Details(int index)
        {
            var person = _service.GetOne(index);
            if (person != null)
            {
                var model = new PersonDetailsModel
                {
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Gender = person.Gender,
                    DateOfBirth = person.DateOfBirth,
                    PhoneNumber = person.PhoneNumber,
                    BirthPlace = person.BirthPlace,
                };
                ViewData["Index"] = index;
                return View(model);
            }
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(PersonCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var person = new PersonModel
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                    DateOfBirth = model.DateOfBirth,
                    BirthPlace = model.BirthPlace,
                    PhoneNumber = model.PhoneNumber,
                    IsGraduated = false
                };
                _service.Create(person);
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return RedirectToAction("Index");
        }

        public IActionResult Update(int index)
        {
            var person = _service.GetOne(index);
            if (person != null)
            {
                return View(person);
            }

            return NotFound();
        }
        [HttpPost]
        public IActionResult Update(int index, PersonUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                var person = _service.GetOne(index);
                if (person != null)
                {
                    person.FirstName = model.FirstName;
                    person.LastName = model.LastName;
                    person.PhoneNumber = model.PhoneNumber;
                    person.BirthPlace = model.BirthPlace;
                    person.IsGraduated = model.IsGraduated;

                    _service.Update(index, person);
                }

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Delete(int index)
        {
            var result = _service.Delete(index);
            if (result == null)
            {
                return NotFound();
            }

            HttpContext.Session.SetString("DeletedNames", result.FullName);
            return View(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
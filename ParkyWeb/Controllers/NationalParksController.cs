﻿using Microsoft.AspNetCore.Mvc;
using ParkyWeb.Models;
using ParkyWeb.Repository.IRepository;
using System.IO;
using System.Threading.Tasks;

namespace ParkyWeb.Controllers
{
    public class NationalParksController : Controller
    {
        private readonly INationalParkRepository _npRepo;

        public NationalParksController(INationalParkRepository npRepo)
        {
            _npRepo = npRepo;
        }
        public IActionResult Index()
        {
            return View(new NationalPark() { });
        }
        public async Task<IActionResult> Upsert(int? id)
        {
            NationalPark obj = new NationalPark();
            if (id == null)
            {
                //this will be true for insert/create
                return View(obj);//returns the view i.e what would be displayed to the client.
            }
            //flow will come here for update
            obj = await _npRepo.GetAsync(SD.NationalParkAPIPath, id.GetValueOrDefault());//had to use GetValueOrDefault because of the nullable parameter.
            if(obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(NationalPark obj)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }
                    obj.Picture = p1;
                }
                else
                {
                    var objFromDb = await _npRepo.GetAsync(SD.NationalParkAPIPath, obj.Id);
                    obj.Picture = objFromDb.Picture;
                }
                if (obj.Id == 0)
                {
                    await _npRepo.CreateAsync(SD.NationalParkAPIPath, obj);
                }
                else
                {
                    await _npRepo.UpdateAsync(SD.NationalParkAPIPath + obj.Id, obj);
                }
                return RedirectToAction(nameof(Index)); // or you can use "Index" instead of nameof(Inde) 
            }
            else
            {
                return View(obj);
            }
        }

        public async Task<IActionResult> GetAllNationalPark()
        {
            return Json(new { data = await _npRepo.GetAllAsync(SD.NationalParkAPIPath) });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id) // this would be called from our js delete method
        {
            var status = await _npRepo.DeleteAsync(SD.NationalParkAPIPath, id);
            if (status)
            {
                return Json(new { success = true, message = "Delete Successful" }); 
            }
            return Json(new { success = false, message = "Delete not Successful" });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicineX.DBContext;
using MedicineX.DbModel;

namespace MedicineX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public MedicinesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Medicines
        [HttpGet]
        public async Task<ActionResult> GetMedicines()
        {
            if (_context.Medicines == null)
            {
                return NotFound();
            }
            return Ok(await _context.Medicines.ToListAsync());
        }

        // GET: api/Medicines/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetMedicine(int id)
        {
            if (_context.Medicines == null)
            {
                return NotFound();
            }
            var medicine = await _context.Medicines.FindAsync(id);

            if (medicine == null)
            {
                return NotFound();
            }

            return Ok(medicine);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicine(int id, MedicineDto medicine)
        {
            var exmedi = await _context.Medicines.FindAsync(id);
            if (exmedi == null)
            {
                return NotFound("Id Not Exist");
            }
            exmedi.ExpDate = DateTime.ParseExact(medicine.ExpDate, "yyyy-MM-dd", null);
            exmedi.Price=medicine.Price;
            exmedi.Quantity=medicine.Quantity;
            await _context.SaveChangesAsync();
            return Ok("Updated Successfully");
        }

        [HttpPost]
        public async Task<ActionResult> PostMedicine(MedicineDto medicine)
        {
            if (_context.Medicines == null)
            {
                return Problem("Entity set 'DatabaseContext.Medicines'  is null.");
            }
            Medicine exmedi= new Medicine();

            exmedi.ExpDate = DateTime.ParseExact(medicine.ExpDate, "yyyy-MM-dd", null);
            exmedi.Price = medicine.Price;
            exmedi.Quantity = medicine.Quantity;
            exmedi.Name = medicine.Name;
            exmedi.ImageUrl = medicine.ImageUrl;
            _context.Medicines.Add(exmedi);
            await _context.SaveChangesAsync();

            return Ok("Add Successfully");
        }

        // DELETE: api/Medicines/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMedicine(int id)
        //{
        //    if (_context.Medicines == null)
        //    {
        //        return NotFound();
        //    }
        //    var medicine = await _context.Medicines.FindAsync(id);
        //    if (medicine == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Medicines.Remove(medicine);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool MedicineExists(int id)
        //{
        //    return (_context.Medicines?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}

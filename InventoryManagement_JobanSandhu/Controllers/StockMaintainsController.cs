﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InventoryManagement_JobanSandhu.Data;
using InventoryManagement_JobanSandhu.Models;
using Microsoft.AspNetCore.Authorization;

namespace InventoryManagement_JobanSandhu.Controllers
{
    [Authorize]
    public class StockMaintainsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StockMaintainsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StockMaintains
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.StockMaintains.Include(s => s.Category).Include(s => s.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: StockMaintains/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockMaintain = await _context.StockMaintains
                .Include(s => s.Category)
                .Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (stockMaintain == null)
            {
                return NotFound();
            }

            return View(stockMaintain);
        }

        // GET: StockMaintains/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "CategoryName");
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ProductName");
            return View();
        }

        // POST: StockMaintains/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CategoryID,ProductID,Quantity,Price,SoldItems,LeftItems")] StockMaintain stockMaintain)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockMaintain);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "CategoryName", stockMaintain.CategoryID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ProductName", stockMaintain.ProductID);
            return View(stockMaintain);
        }

        // GET: StockMaintains/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockMaintain = await _context.StockMaintains.FindAsync(id);
            if (stockMaintain == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "CategoryName", stockMaintain.CategoryID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ProductName", stockMaintain.ProductID);
            return View(stockMaintain);
        }

        // POST: StockMaintains/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CategoryID,ProductID,Quantity,Price,SoldItems,LeftItems")] StockMaintain stockMaintain)
        {
            if (id != stockMaintain.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockMaintain);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockMaintainExists(stockMaintain.ID))
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
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "CategoryName", stockMaintain.CategoryID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ProductName", stockMaintain.ProductID);
            return View(stockMaintain);
        }

        // GET: StockMaintains/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockMaintain = await _context.StockMaintains
                .Include(s => s.Category)
                .Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (stockMaintain == null)
            {
                return NotFound();
            }

            return View(stockMaintain);
        }

        // POST: StockMaintains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stockMaintain = await _context.StockMaintains.FindAsync(id);
            _context.StockMaintains.Remove(stockMaintain);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockMaintainExists(int id)
        {
            return _context.StockMaintains.Any(e => e.ID == id);
        }
    }
}

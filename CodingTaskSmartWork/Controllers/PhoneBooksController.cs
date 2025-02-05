﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodingTaskSmartWork.IService;
using CodingTaskSmartWork.Model;
using Microsoft.AspNetCore.Mvc;

namespace CodingTaskSmartWork.Controllers
{
    [Route("api/PhoneBook")]
    public class PhoneBooksController : ControllerBase
    {
       // private readonly IRepository<PhoneBook> _phoneBookRepository;
        private readonly IPhoneBookService _phoneBookService;
        public PhoneBooksController(IPhoneBookService phoneBookService)
        {
            //_phoneBookRepository = repo;
            _phoneBookService = phoneBookService;
        }
        /// <summary>
        /// Retrieve all PhoneBooks.
        /// </summary>
        /// <returns>A list of PhoneBooks </returns>
        // GET: api/getAllPhoneBooks
        [HttpGet("~/api/getAllPhoneBooks")]
        public List<PhoneBook> getAllPhoneBooks()
        {
            //return guideDbContext.Guides;
            return _phoneBookService.GetAll();
        }
        /// <summary>
        /// Retrieve all PhoneBooks ordered by FirstName.
        /// </summary>
        /// <returns>A list of PhoneBooks </returns>
        //[HttpGet("~/api/OrderByName")]
        //public List<PhoneBook> OrderByName()
        //{
        //    //return guideDbContext.Guides;
        //    return _phoneBookRepository.getAllRecords().OrderBy(p => p.FirstName).ToList();
        //}
        ///// <summary>
        ///// Retrieve all PhoneBooks ordered by LastName.
        ///// </summary>
        ///// <returns>A list of PhoneBooks </returns>
        //[HttpGet("~/api/OrderBylastName")]
        //public List<PhoneBook> OrderBylastName()
        //{
        //    //return guideDbContext.Guides;
        //    return _phoneBookRepository.getAllRecords().OrderBy(p => p.FirstName).ToList();
        //}
        ///// <summary>
        ///// Retrieve all PhoneBooks ,whoose name match with search value,
        ///// </summary>
        ///// <returns>A list of PhoneBooks </returns>
        //[HttpGet("~/api/searchByName")]
        //public List<PhoneBook> searchByName(string searchvalue)
        //{
        //    //return guideDbContext.Guides;
        //    return _phoneBookRepository.getAllRecords().Where(p => p.FirstName.Contains("searchvalue")).ToList();
        //}

        // <summary>
        // Adds a PhoneBook Record to jsonFile.
        // </summary>
        // <returns>The created Record </returns>
        [HttpPost("~/api/AddPhoneBook")]
        public async Task<IActionResult> AddPhoneBook([FromBody] PhoneBook _phoneBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _phoneBookService.addPhoneBook(_phoneBook);
            return CreatedAtAction("GetCreatedRecord", new { id = _phoneBook.id }, _phoneBook);

        }
        /// <summary>
        /// Retrieve the PhoneBook by their ID.
        /// </summary>
        /// <param name="id">The ID of the desired PhoneBook</param>
        /// <returns>A PhoneBook Record</returns>
        [HttpGet("~/api/FindById")]
        public ActionResult<PhoneBook> FindById(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var foundPhoneBook = _phoneBookService.findById(id);
            if (foundPhoneBook == null)
            {
                return NotFound();

            }
            else
            {
                return Ok(foundPhoneBook);
            }
        }
        /// <summary>
        /// Delete a PhoneBook Record to jsonFile whoose ID match with an egzisting record.
        /// </summary>
        /// <param name="_phoneBook">The Phone Book record which needs to be removed </param>
        /// <returns>The Deleted Record </returns>
        [HttpDelete("~/api/Delete")]
        public async Task<IActionResult> Delete([FromBody] PhoneBook _phoneBook)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _phoneBookService.deletePhoneBook(_phoneBook);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

            ///// <summary>
            ///// Update a PhoneBook Record to jsonFile whoose ID match with an egzisting record.
            ///// </summary>
            ///// <param name="_phoneBook">The Phone Book record which needs to be updated </param>

            ///// <returns>The Deleted Record </returns>
            //[HttpPut("~/api/Update")]
            //public async Task<ActionResult> Update([FromBody] PhoneBook _phoneBook)
            //{
            //    if (!ModelState.IsValid)
            //    {
            //        return BadRequest(ModelState);
            //    }
            //   var result =  await _phoneBookRepository.Update(_phoneBook);
            //    if(result == null)
            //    {
            //        return NotFound();
            //    }
            //    return Ok(result);
            //}

        }
}

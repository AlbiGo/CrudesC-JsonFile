using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingTaskSmartWork.Interface;
using CodingTaskSmartWork.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CodingTaskSmartWork.Controllers
{
    [Route("api/PhoneBook")]
    [ApiController]
    public class PhoneBooksController : ControllerBase
    {
        private readonly IRepository<PhoneBook> _phoneBookRepository;
        public PhoneBooksController( IRepository<PhoneBook> repo)
        {
            _phoneBookRepository = repo;
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
            return _phoneBookRepository.getAllRecords();
        }
        /// <summary>
        /// Retrieve all PhoneBooks ordered by FirstName.
        /// </summary>
        /// <returns>A list of PhoneBooks </returns>
        [HttpGet("~/api/OrderByName")]
        public List<PhoneBook> OrderByName()
        {
            //return guideDbContext.Guides;
            return _phoneBookRepository.getAllRecords().OrderBy(p => p.FirstName).ToList();
        }
        /// <summary>
        /// Retrieve all PhoneBooks ordered by LastName.
        /// </summary>
        /// <returns>A list of PhoneBooks </returns>
        [HttpGet("~/api/OrderBylastName")]
        public List<PhoneBook> OrderBylastName()
        {
            //return guideDbContext.Guides;
            return _phoneBookRepository.getAllRecords().OrderBy(p => p.FirstName).ToList();
        }
        /// <summary>
        /// Retrieve all PhoneBooks ,whoose name match with search value,
        /// </summary>
        /// <returns>A list of PhoneBooks </returns>
        [HttpGet("~/api/searchByName")]
        public List<PhoneBook> searchByName(string searchvalue)
        {
            //return guideDbContext.Guides;
            return _phoneBookRepository.getAllRecords().Where(p => p.FirstName.Contains("searchvalue")).ToList();
        }
        /// <summary>
        /// Adds a PhoneBook Record to jsonFile.
        /// </summary>
        /// <returns>The created Record </returns>
        [HttpPost("~/api/AddPhoneBook")]
        public async Task<ActionResult> AddPhoneBook([FromBody] PhoneBook _phoneBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _phoneBookRepository.Add(_phoneBook);
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
            var foundPhoneBook = _phoneBookRepository.FindById(id);
            if(foundPhoneBook == null)
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
        public async Task<ActionResult> Delete([FromBody] PhoneBook _phoneBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           var result =  await _phoneBookRepository.Delete(_phoneBook);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(_phoneBook);

             
        }
        /// <summary>
        /// Update a PhoneBook Record to jsonFile whoose ID match with an egzisting record.
        /// </summary>
        /// <param name="_phoneBook">The Phone Book record which needs to be updated </param>

        /// <returns>The Deleted Record </returns>
        [HttpPut("~/api/Update")]
        public async Task<ActionResult> Update([FromBody] PhoneBook _phoneBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           var result =  await _phoneBookRepository.Update(_phoneBook);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

    }
}

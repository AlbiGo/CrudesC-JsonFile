using CodingTaskSmartWork.Interface;
using CodingTaskSmartWork.IService;
using CodingTaskSmartWork.Model;
using CodingTaskSmartWork.Repository;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CodingTaskSmartWork.Services
{
    public class PhoneBookService : IPhoneBookService
    {
        private readonly IRepository<PhoneBook> _phoneBookRepository;
        private readonly IRepository<PhoneType> _phoneTypeRepository;

        private string jsonFile = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "JsonFile", "jsonFile.json");
        private string jsonFileType = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "JsonFile", "jsonFilePhoneType.json");
        JArray jObject = new JArray();
        string jsonString = "";
        JArray jObjectPhoneType = new JArray();
        string jsonStringPhoneType = "";
        public PhoneBookService(IRepository<PhoneBook> phoneBookRepository , IRepository<PhoneType> phoneTypeRepository)
        {
            _phoneBookRepository = phoneBookRepository;
            _phoneTypeRepository = phoneTypeRepository;
            if (File.Exists(jsonFile) && File.Exists(jsonFileType))
            {
                jsonString = File.ReadAllText(jsonFile);
                jObject = JArray.Parse(jsonString);
                jsonStringPhoneType = File.ReadAllText(jsonFileType);
                jObjectPhoneType = JArray.Parse(jsonStringPhoneType);

            }
            else
            {
                throw new Exception("File doesnt exist");
            }
        }

        public List<PhoneBook> GetAll()
        {
            var phoneBooks = this._phoneBookRepository.getAllRecords(jsonString);
            foreach (var element in phoneBooks)
            {
                var obje = _phoneTypeRepository.FindById(element.TypeID.ToString() , jsonFileType);
                if(obje != null)
                {
                    var phoneType = new PhoneType()
                    {
                        id = obje.id,
                        Type = obje.Type

                    };
                    element.PhoneType = phoneType;
                }
                
            }
            return phoneBooks;

        }
        public async Task addPhoneBook(PhoneBook entity)
        {
            try
            {
                var phoneTypeId = entity.TypeID;
                await this._phoneBookRepository.Add(entity,jsonFile);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task deletePhoneBook(PhoneBook entity)
        {
            try
            {
                await this._phoneBookRepository.Delete(entity ,jsonFile);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public PhoneBook findById(string id)
        {
            try
            {
                return this._phoneBookRepository.FindById(id , jsonFile);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}

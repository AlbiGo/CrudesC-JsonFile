using CodingTaskSmartWork.Interface;
using CodingTaskSmartWork.Model;
using CodingTaskSmartWork.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTaskUnitTesting
{
    public class UnitTestService<T> : IRepository<T> where T : class
    {
        string jsonFile = @"C:\Users\Administrator\source\repos\CodingTaskSmartWork\CodingTaskUnitTesting\JsonFileMockData\jsonFileMockData.json";
        JArray jObject = new JArray();
        string jsonString = "";
        public PhoneType  getPhoneType(int id)
        {
            try
            {
                string jsonFilePhoneType = @"C:\Users\Administrator\source\repos\CodingTaskSmartWork\CodingTaskUnitTesting\JsonFileMockData\jsonFilePhoneTypeMockData.json";
                JArray jo = new JArray();
                var JsonStringPhoneType = File.ReadAllText(jsonFilePhoneType);
                jo = JArray.Parse(JsonStringPhoneType);

                var PhoneBook = jo.FirstOrDefault(obj => (int)obj["PhoneTypeID"] == id);
                PhoneType _PhoneBook = Newtonsoft.Json.JsonConvert.DeserializeObject<PhoneType>(PhoneBook.ToString());

                return _PhoneBook;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public UnitTestService()
        {
            if (File.Exists(jsonFile))
            {
                jsonString = File.ReadAllText(jsonFile);
                jObject = JArray.Parse(jsonString);
            }
            else
            {
                throw new Exception("File doesnt exist");
            }


        }
        public List<T> getAllRecords()
        {
            try
            {
                var PhoneNumberlist = JsonConvert.DeserializeObject<List<PhoneBook>>(jsonString);
                foreach (var element in PhoneNumberlist)
                {
                    var phoneType = this.getPhoneType((int)element.TypeID);
                    PhoneType pht = new PhoneType()
                    {
                        PhoneTypeID = phoneType.PhoneTypeID,
                        Type = phoneType.Type
                    };
                    element.PhoneType = pht;
                }

                return PhoneNumberlist as List<T>;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Add(T entity)
        {
                try
                {
                    var ph = entity as PhoneBook;
                    Guid PhoneBookID = Guid.NewGuid();//PhoneBookCreation
                    ph.id = PhoneBookID;
                    var phoneType = this.getPhoneType((int)ph.TypeID);
                    PhoneType pht = new PhoneType()
                    {
                        PhoneTypeID = phoneType.PhoneTypeID,
                        Type = phoneType.Type
                    };
                    ph.PhoneType = pht;
                    string json = JsonConvert.SerializeObject(ph, Formatting.Indented);
                    var Object = JObject.Parse(json);
                    jObject.Add(Object);
                   //await this.saveAsync(jObject, jsonFile);
            }


            catch (Exception)
                {
                    throw;
                }
        }
        public async Task<T> Update(T entity)
        {
            try
            {
              
                    var ph = entity as PhoneBook;
                    var PhoneBook = jObject.FirstOrDefault(obj => (Guid)obj["id"] == ph.id);
                    if (PhoneBook == null)
                    {
                        return null;
                    }
                    else
                    {
                        string json = JsonConvert.SerializeObject(ph, Formatting.Indented);
                        var newObject = JObject.Parse(json);
                        PhoneBook["FirstName"] = newObject["FirstName"];
                        PhoneBook["LastName"] = newObject["LastName"];
                        PhoneBook["TypeID"] = newObject["TypeID"];
                        PhoneBook["PhoneNumber"] = newObject["PhoneNumber"];
                        return ph as T;
                       
                    }
                

            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<T> Delete(T entity)
        {
            try
            {
                var ph = entity as PhoneBook;
                var foundRecordToBeDeleted = this.FindById(ph.id.ToString());
                if (foundRecordToBeDeleted == null)
                {
                    return null;
                }
                else
                {
                    string json = JsonConvert.SerializeObject(foundRecordToBeDeleted, Formatting.Indented);
                    var Object = JObject.Parse(json);
                    jObject.Remove(Object);
                    return foundRecordToBeDeleted as T;
                }


            }
            catch (Exception)
            {
                throw;
            }
        }

        public T FindById(string id)
        {
            try
            {
                var CheckGuid = Guid.TryParse(id, out Guid phoneBookID);
                if (CheckGuid)
                {
                    var _PhoneBook = new PhoneBook();
                    var PhoneBook = jObject.FirstOrDefault(obj => (Guid)obj["id"] == phoneBookID);
                    if (PhoneBook == null)
                    {
                        return null;
                    }
                    else
                    {
                        _PhoneBook = Newtonsoft.Json.JsonConvert.DeserializeObject<PhoneBook>(PhoneBook.ToString());
                        var phoneType = this.getPhoneType((int)_PhoneBook.TypeID);
                        if (phoneType != null)
                        {
                            _PhoneBook.PhoneType = phoneType;
                        }
                       
                        return _PhoneBook as T;
                    }
                }
                else
                {
                    throw new Exception("Id is not valid");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    
    }
}

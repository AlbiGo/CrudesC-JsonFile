using CodingTaskSmartWork.Interface;
using CodingTaskSmartWork.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace CodingTaskSmartWork.Repository
{
    
    public class PhoneBookRepository<T> : IRepository<T> where T : class
    {
        phoneTypeRepository _phoneTypeRepository = new phoneTypeRepository();  
        string jsonFile = System.IO.Path.Combine(Directory.GetCurrentDirectory() , "JsonFile", "jsonFile.json");
        JArray jObject = new JArray();
        string jsonString = "";
        private static readonly Object lockObject = new Object();


        public PhoneBookRepository()
        {
            if(File.Exists(jsonFile))
            {
                jsonString = File.ReadAllText(jsonFile);
                jObject = JArray.Parse(jsonString);
            }
            else
            {
                throw new Exception("File doesnt exist");
            }


        }
        public async Task saveAsync(JArray _JArray, string jsonFilePath)
        {

            string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(_JArray, Newtonsoft.Json.Formatting.Indented);
            await File.WriteAllTextAsync(jsonFilePath, newJsonResult);
        }
        public async Task Add(T entity)
        {
            try
            {   
                var ph = entity as PhoneBook;
                Guid PhoneBookID = Guid.NewGuid();//PhoneBookCreation
                ph.id = PhoneBookID;
                string json = JsonConvert.SerializeObject(ph, Formatting.Indented);
                var Object = JObject.Parse(json);
                jObject.Add(Object);
                await this.saveAsync(jObject, jsonFile);
            }
           
             
            catch(Exception)
            {
                throw ;
            }

        }
        public List<T> getAllRecords()
        {
            try
            {
                var PhoneNumberlist = JsonConvert.DeserializeObject<List<PhoneBook>>(jsonString);
                foreach(var element in PhoneNumberlist)
                {
                    var phoneType = _phoneTypeRepository.getPhoneType((int)element.TypeID);
                    PhoneType pht = new PhoneType()
                    {
                        PhoneTypeID = phoneType.PhoneTypeID,
                        Type = phoneType.Type
                    };
                    element.PhoneType = pht;
                }

                return PhoneNumberlist as List<T>;
            }
            catch(Exception)
            {
                throw;
            }
          

        }
        public List<PhoneBook> OrderByName()
        {
            try
            {
                var PhoneNumberlist = (this.getAllRecords() as List<PhoneBook>).OrderBy(p => p.FirstName).ToList();

                return PhoneNumberlist;
            }
            catch (Exception)
            {
                throw;
            }


        }
        public List<PhoneBook> OrderByLastName()
        {
            try
            {
                var PhoneNumberlist = (this.getAllRecords() as List<PhoneBook>).OrderBy(p => p.LastName).ToList();

                return PhoneNumberlist;
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
                    if(PhoneBook == null)
                    {
                        return null;
                    }
                    else
                    {
                        _PhoneBook = Newtonsoft.Json.JsonConvert.DeserializeObject<PhoneBook>(PhoneBook.ToString());

                        var phoneType = _phoneTypeRepository.getPhoneType((int)_PhoneBook.TypeID);
                        if(phoneType != null)
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

 

        public async Task<T> Delete(T entity)
        {
            try
            {
           
                    var ph = entity as PhoneBook;
                    var foundRecordToBeDeleted = this.FindById(ph.id.ToString());
                    if(foundRecordToBeDeleted == null)
                    {
                        return null;
                    }
                    else
                    {
                        string json = JsonConvert.SerializeObject(foundRecordToBeDeleted, Formatting.Indented);
                        var Object = JObject.Parse(json);
                        jObject.Remove(Object);
                        await this.saveAsync(jObject, jsonFile);
                        return foundRecordToBeDeleted as T;
                    }

             
               
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
                    var PhoneBookNeedToUpdate = jObject.FirstOrDefault(obj => (Guid)obj["id"] == ph.id );
                    if (PhoneBookNeedToUpdate == null)
                    {
                        return null;
                    }
                    else
                    {
             
                        PhoneBookNeedToUpdate["FirstName"] =ph.FirstName;
                        PhoneBookNeedToUpdate["LastName"] = ph.LastName;
                        PhoneBookNeedToUpdate["TypeID"] = ph.TypeID;
                        PhoneBookNeedToUpdate["PhoneNumber"] = ph.PhoneNumber;
                        await this.saveAsync(jObject, jsonFile);
                        var upDatedBook =  this.FindById(ph.id.ToString());
                        return upDatedBook as T;
                    }
                
               
            }
            catch (Exception)
            {
                throw;
            }
        
          
        }



    }
}

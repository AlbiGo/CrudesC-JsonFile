using CodingTaskSmartWork.Interface;
using CodingTaskSmartWork.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;


namespace CodingTaskSmartWork.Repository
{
    
    public class PhoneBookRepository<T> : IRepository<T> where T : class
    {
        private static readonly Object lockObject = new Object();

        public async Task Add(T entity ,string jsonFile)
        {
            try
            {   
                dynamic ph = entity;
                Guid PhoneBookID = Guid.NewGuid();//PhoneBookCreation
                ph.id = PhoneBookID;
                string jsonString = File.ReadAllText(jsonFile);
                var jObject = JArray.Parse(jsonString);
                string json = JsonConvert.SerializeObject(ph, Formatting.Indented);
                var Object = JObject.Parse(json);
                jObject.Add(Object);
                await Utilities.Utility.saveAsync(jObject, jsonFile);
            }
            catch(Exception ex)
            {
                throw ex ;
            }

        }
        public List<T> getAllRecords(string jsonString)
        {
            try
            {
                var PhoneNumberlist = JsonConvert.DeserializeObject<List<T>>(jsonString);
                return PhoneNumberlist;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public T FindById(string id , string jsonFile)
        {
            try
            {
                var jsonString = File.ReadAllText(jsonFile);
                var jObject = JArray.Parse(jsonString);
                var CheckGuid = Guid.TryParse(id, out Guid entityId);
                if (CheckGuid)
                {
                    //dynamic _PhoneBook ;
                    var PhoneBook = jObject.FirstOrDefault(obj => (Guid)obj["id"] == entityId);
                    if(PhoneBook == null)
                    {
                        return null;
                    }
                    else
                    {
                       var _PhoneBook = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(PhoneBook.ToString());
                       return _PhoneBook;
                    }
                   
                }
                else
                {
                    throw new Exception("Id is not valid");
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task Delete(T entity , string jsonFile)
        {
            try
            {
                dynamic record = entity;
                string jsonString = File.ReadAllText(jsonFile);
                JArray jObject = JArray.Parse(jsonString);
                var recordToBeDeleted = jObject.Where(p => p["id"].ToString() == record.id.ToString()).FirstOrDefault();
                if(recordToBeDeleted == null)
                {
                    throw new Exception("Not Found");
                }
                jObject.Remove(recordToBeDeleted);
                await Utilities.Utility.saveAsync(jObject, jsonFile);
                }
            catch (Exception)
            {
                throw;
            }
           
        }

        public async Task<T> Update(T entity , string jsonFile)
        {
            try
            {
                    var ph = entity as PhoneBook;
                    string jsonString = File.ReadAllText(jsonFile);
                    var jObject = JArray.Parse(jsonString);
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
                        await Utilities.Utility.saveAsync(jObject, jsonFile);
                        var upDatedBook =  this.FindById(ph.id.ToString() , jsonFile);
                        return PhoneBookNeedToUpdate as T;
                    }
                
               
            }
            catch (Exception)
            {
                throw;
            }
        
          
        }

    }
}

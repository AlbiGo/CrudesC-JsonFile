using CodingTaskSmartWork.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;


namespace CodingTaskSmartWork.Repository
{
    public class phoneTypeRepository 
    {
        public dynamic getPhoneType(int id)
        {
            
            try
            {
                string jsonFilePhoneType = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "JsonFile", "jsonFilePhoneType.json");
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
    }
}

using CodingTaskSmartWork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTaskSmartWork.IService
{
    public interface IPhoneBookService
    {
        public List<PhoneBook> GetAll();
        public Task addPhoneBook(PhoneBook entity);
        public Task deletePhoneBook(PhoneBook entity);
        public PhoneBook findById(string id);




    }
}

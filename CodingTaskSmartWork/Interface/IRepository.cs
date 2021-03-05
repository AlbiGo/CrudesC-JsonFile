using CodingTaskSmartWork.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTaskSmartWork.Interface
{
    public interface IRepository<T> where T : class
    {
        Task Add(T entity , string s);
        Task<T> Update(T entity , string s);
        Task Delete(T entity , string s);

        T FindById(string id , string jsonFile);
        List<T> getAllRecords(string s);


    }
}

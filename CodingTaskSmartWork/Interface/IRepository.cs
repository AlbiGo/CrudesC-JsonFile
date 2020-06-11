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
        Task Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(T entity);

        T FindById(string id);
        List<T> getAllRecords();


    }
}

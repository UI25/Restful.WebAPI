using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIModels.Models.V1;

namespace WebAPIRepository.Interfaces.V1
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployeeAll();
        Task<Employee> GetEmployeeById(Guid Id);
        Task<Employee> InsertEmployee(Employee objEmployee);
        Task<Employee> UpdateEmployee(Employee objEmployee);
        Task<Employee> DeleteEmployee(Guid Id);

    }
}

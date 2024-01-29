using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIModels.Mapper.V1;
using WebAPIModels.Data;
using WebAPIRepository.Interfaces.V1;
using System.CodeDom;
using WebAPIModels.Models.V1;

namespace WebAPIRepository.Managers.V1
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly WebAPIDbContext _context;
        private readonly IMapper _mapper;
        public EmployeeRepository(WebAPIDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Employee>> GetEmployeeAll()
        {
            try
            {
                return await _context.Employees.ToArrayAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(nameof(ex));
            }
            
        }
        public async Task<Employee> GetEmployeeById(Guid Id)
        {
            var _result = await _context.Employees.FindAsync(Id);
            if(_result != null) 
            {
                return _result;
            }
            else
            {
                throw new InvalidCastException();
            }
        }
        public async Task<Employee> InsertEmployee(Employee objEmployee)
        {
            _context.Employees.Add(objEmployee);
            await _context.SaveChangesAsync();
            return objEmployee;
        }
        public async Task<Employee> UpdateEmployee(Employee objEmployee)
        {
            _context.Entry(objEmployee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return objEmployee;
        }
        public async Task<Employee> DeleteEmployee(Guid Id)
        {
            var _result = await _context.Employees.FindAsync(Id);
            if (_result is null)
            {
                throw new ArgumentNullException();
            }
         
            _context.Employees.Remove(_result);
            await _context.SaveChangesAsync();
            return _result;
            
            
            
        }
    }
}

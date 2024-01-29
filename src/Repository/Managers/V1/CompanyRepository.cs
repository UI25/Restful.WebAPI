using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIModels.Models.V1;
using WebAPIModels.Data;
using WebAPIRepository.Interfaces.V1;
using System.Net;
using Azure.Messaging;

namespace WebAPIRepository.Managers.V1
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly WebAPIDbContext _context;

        public CompanyRepository(WebAPIDbContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<Company>> GetCompanyAll()
        {
            try
            {
                return await _context.Companies.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(nameof(ex));
            }
                
        }
        public async Task<Company> GetCompanyById(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            else
            {
                var _result = await _context.Companies.FindAsync(Id);

                if (_result != null)
                {
                    return _result;
                }
                else
                {
                    throw new InvalidCastException();
                }
            }
        }
        public async Task<Company> InsertCompany(Company objCompany)
        {
            _context.Companies.Add(objCompany);
            await _context.SaveChangesAsync();
            return objCompany;
        }
        public async Task<Company> UpdateCompany(Company objCompany)
        {
            _context.Entry(objCompany).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return objCompany;
        }
        public async Task<Company> DeleteCompany(Guid Id)
        {

            var CompanyItem = await _context.Companies.FindAsync(Id);
            if(CompanyItem == null)
            {
                throw new Exception("Company Not Found");
            }
            _context.Companies.Remove(CompanyItem);
            await _context.SaveChangesAsync();
            return CompanyItem;    
        }
    }
}

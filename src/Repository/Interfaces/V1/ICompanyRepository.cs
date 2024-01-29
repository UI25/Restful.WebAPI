using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIModels.Models.V1;

namespace WebAPIRepository.Interfaces.V1
{
    public interface ICompanyRepository
    {
        Task <IEnumerable<Company>> GetCompanyAll();
        Task<Company> GetCompanyById(Guid Id);
        Task<Company> InsertCompany(Company objCompany);
        Task<Company> UpdateCompany(Company objCompany);
        Task<Company> DeleteCompany(Guid Id);

    }
}

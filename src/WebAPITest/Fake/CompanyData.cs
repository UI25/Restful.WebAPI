using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIModels.Models.V1;

namespace WebAPITest.Fake
{
    public class CompanyData
    {
        public IEnumerable<Company> GetCompanyTestData()
        {
            return new List<Company>
            {
                new Company
                {
                    Id = Guid.Parse("417738A1-A5ED-4118-8525-46710D017F08"),
                    Name = "IT_Solutions Ltd",
                    Address = "584 Wall Dr. Gwynn Oak, MD 21207",
                    Country = "USA"
                },
                new Company
                {
                    Id = Guid.Parse("3D490A70-94CE-4D15-9494-5248280C2CE3"),
                    Name = "Admin_Solutions Ltd",
                    Address = "312 Forest Avenue, BF 923",
                    Country = "USA"
                }
            };
        }
    }
}

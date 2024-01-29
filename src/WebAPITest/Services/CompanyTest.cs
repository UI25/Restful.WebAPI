using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.V1;
using WebAPIModels.Models.V1;
using WebAPIRepository.Interfaces.V1;
using WebAPITest.Fake;
using Xunit.Abstractions;

namespace WebAPITest.Services
{
    public class CompanyTest
    {
        private readonly Mock<ICompanyRepository> mockRepo;

        public CompanyTest() 
        {
            mockRepo = new Mock<ICompanyRepository>();
        }
        [Fact]
        public void Get_Returns_ActionResults()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetCompanyAll())
                    .Returns(new CompanyData().GetCompanyTestData); 
            var controller = new CompanyController(mockRepo.Object, null, null);
            //Act
            var result = controller.GetAll(); 
            //Assert
            Assert.NotNull(result);
        }
    }
}

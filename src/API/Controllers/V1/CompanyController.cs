using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIModels.Models.V1;
using WebAPIModels.Mapper.V1;
using WebAPIRepository.Interfaces.V1;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Options;

namespace WebAPI.V1
{
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/[Controller]")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName ="v1")]
    [EnableCors("MyAllowSpecificOrigins")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ICompanyRepository companyRepository, ILogger<CompanyController> logger, IMapper mapper)  
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _logger = logger;
        }
      
        [MapToApiVersion("1.0")]
        [HttpGet]
        [Route("GetCompanyAll")]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation($"GetCompanyAll event");
                var company = await _companyRepository.GetCompanyAll();
                if (company == null) 
                {
                    return NotFound();
                }
                else
                {
                    var companyDto = _mapper.Map<IEnumerable<CompanyDto>>(company);
                }
                
                return Ok(company);
            }
            catch (Exception ex)
            {
                 _logger.LogError(ex.Message, $"Exception occurred with a message:{ex.Message}");
                 return StatusCode(500, ex.Message);
                
            }
               
        }
        
        [MapToApiVersion("1.0")]
        [HttpGet]
        [Route("GetCompanyById/{Id}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    _logger.LogInformation($"GetCompanyById({Id}) event");
                    var company = await _companyRepository.GetCompanyById(Id);
                    if (company == null)
                    {
                        return NotFound();
                    }
                    var companyDto = _mapper.Map<CompanyDto>(company);
                    return StatusCode(200,company);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Exception occurred with a message:{ex.Message}");
                    return StatusCode(500, ex.Message);
                }
            }
        }
        [MapToApiVersion("1.0")]
        [HttpPost]
        [Route("InsertCompany")]
        public async Task<IActionResult> AddCompany([FromBody] CompanyForCreationDto objcompany)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var company = new Company()
                    {
                        Id = Guid.NewGuid(),
                        Name = objcompany.Name,
                        Address = objcompany.Address,
                        Country = objcompany.Country,
                    };
                    await _companyRepository.InsertCompany(company);
                    return StatusCode(201, company);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Exception occurred with a message:{ex.Message}");
                    return StatusCode(500, ex.Message);
                }

            }
            else
            {
                return StatusCode(400, ModelState.Values);
            }

                              
        }
        [MapToApiVersion("1.0")]
        [HttpPut]
        [Route("UpdateCompany/{Id}")]
        public async Task <IActionResult> UpdateCompany(Guid Id, [FromBody] CompanyForCreationDto objcompany)
        {
                try
                {
                    _logger.LogInformation($"UpdateCompany event");
                    var company = new Company()
                    {
                        Id = Id,
                        Name = objcompany.Name,
                        Address = objcompany.Address,
                        Country = objcompany.Country,
                    };
                    await _companyRepository.UpdateCompany(company);
                    return Ok(company);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Exception occurred with a message:{ex.Message}");
                    return StatusCode(500, ex.Message);
                }
                       
        }    
        [MapToApiVersion("1.0")]
        [HttpDelete]
        [Route("DeleteCompany/{Id}")]
        public async Task <IActionResult> DeleteCompany(Guid Id)
        {
                try
                {
                    await _companyRepository.DeleteCompany(Id);
                    return StatusCode(200, "Company deleted");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Exception occurred with a message:{ex.Message}");
                    return StatusCode(500, ex.Message);
                }          
          
        }
        
    }
}

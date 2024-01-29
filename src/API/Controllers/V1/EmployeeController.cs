using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIRepository.Interfaces.V1;
using WebAPIModels.Models.V1;
using AutoMapper;

namespace WebAPI.Controllers.V1
{
    [ApiController]
    [Route("api/[Controller]")]
    [ApiVersion("1.0")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(IEmployeeRepository employeeRepository, ILogger<EmployeeController> logger, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into EmployeeController");
        }
        [MapToApiVersion("1.0")]
        [HttpGet]
        [Route("GetEmployeeAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _logger.LogInformation($"GetEmployeeAll event");
                var employee = await _employeeRepository.GetEmployeeAll();
                var employeeDto = _mapper.Map<IEnumerable<EmployeeDto>>(employee);
                return Ok(employeeDto);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Exception occurred with a message:{ex.Message}");
                 return StatusCode(500, ex.Message);
            }  
        }
        [MapToApiVersion("1.0")]
        [HttpGet]
        [Route("GetEmployeeById/{Id}")]
        public async Task<IActionResult> GetById(Guid Id)
        {           
            try
            {
                _logger.LogInformation($"GetEmployeeAll event");
                var employee = await _employeeRepository.GetEmployeeById(Id);
                var employeeDto = _mapper.Map<EmployeeDto>(employee);
                return Ok(employeeDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred with a message:{ex.Message}");
                return StatusCode(500, ex.Message);
            }  
        }
        [MapToApiVersion("1.0")]
        [HttpPost]
        [Route("InsertEmployee")]
        public async Task<IActionResult> AddEmployee([FromForm] EmployeeForCreationDto objemployee)
        {
            if(objemployee is null)
            {
                _logger.LogError($"object sent from client is null.");
                 return BadRequest("EmployeeForUpdateDto object is null");
            }
            
            if(!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForCreationDto object");
                return UnprocessableEntity(ModelState);     
            }

            try
            {
                var employee = new Employee()
                {
                    Id = Guid.NewGuid(),
                    Name = objemployee.Name,
                    Age = objemployee.Age,
                    Position = objemployee.Position,
                    CompanyId = objemployee.CompanyId
                };
                _logger.LogInformation("GetEmployeeAll event");
                await _employeeRepository.InsertEmployee(employee);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred with a message:{ex.Message}");
                return StatusCode(500, ex.Message);
            }  
        }
        [MapToApiVersion("1.0")]
        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task <IActionResult> UpdateEmployee(Guid Id, [FromBody] EmployeeForCreationDto objemployee)
        {
             if(objemployee is null || Id == Guid.Empty)
            {
                _logger.LogError($"object sent from client is null.");
                 return BadRequest("EmployeeForUpdateDto object is null");
            }
            
            if(!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForUpdateDto object");
                return UnprocessableEntity(ModelState);     
            }
         
            try
            {
                var employee = new Employee()
                {
                    Id = Guid.NewGuid(),
                    Name = objemployee.Name,
                    Age = objemployee.Age,
                    Position = objemployee.Position,
                    CompanyId = objemployee.CompanyId
                };
                _logger.LogInformation("GetEmployeeAll event");
                await _employeeRepository.UpdateEmployee(employee);
                return Ok(employee);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Exception occurred with a message:{ex.Message}");
                 return StatusCode(500, ex.Message);
            }  
                
        }
        [MapToApiVersion("1.0")]
        [HttpDelete]
        [Route("DeleteEmployee")]
        public async Task <IActionResult> DeleteEmployee(Guid Id)
        {
                   if(Id == Guid.Empty)
            {
                 _logger.LogError($"object sent from client is null.");
                 return BadRequest("EmployeeForUpdateDto object is null");
            }
            if(!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForUpdateDto object");
                return UnprocessableEntity(ModelState);     
            }
            try
            {
                await _employeeRepository.DeleteEmployee(Id);
                return Ok(); 
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Exception occurred with a message:{ex.Message}");
                 return StatusCode(500, ex.Message);
            }  
                    
        }
    }
}

using ClinicManagement.API.Controllers.Base;
using ClinicManagement.Application.Contracts.Services;
using ClinicManagement.Application.Dtos.Department;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController(IDepartmentService departmentService) : APIBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllDepartments([FromQuery] DepartmentQueryParams queryParams, CancellationToken ct = default)
        {
            var result = await departmentService.GetAllDepartmentsAsync(queryParams, ct);
            return ToActionResult(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id, CancellationToken ct = default)
        {
            var result = await departmentService.GetDepartmentByIdAsync(id, ct);
            return ToActionResult(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentDto newDepartmentDto, CancellationToken ct = default)
        {
            var result = await departmentService.CreateDepartmentAsync(newDepartmentDto, ct);
            if (result.IsFailure) return ToProblem(result);

            return CreatedAtAction(nameof(GetDepartmentById), new { id = result.Data!.Id }, result.Data);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] UpdateDepartmentDto updateDepartmentDto, CancellationToken ct = default)
        {
            var result = await departmentService.UpdateDepartmentAsync(id, updateDepartmentDto, ct);


            return ToActionResult(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteDepartment(int id, CancellationToken ct = default)
        {
            var result = await departmentService.DeleteDepartmentAsync(id, ct);


            return ToActionResult(result);
        }


    }
}

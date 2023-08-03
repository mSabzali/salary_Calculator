﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalaryCalculator.WebApi.Adapter;

namespace SalaryCalculator.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SalaryEmployeeController : ControllerBase
{
    private readonly IMediator _mediator;

    public SalaryEmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("{datatype}/[controller]")]
    public async Task<IActionResult> HttpPost(string datatype, RegisterSalaryEmployeeRequest employeeRequest)
    {
        ISalaryEmployeeAdapter employeeAdapter = datatype switch
        {
            "json" => new JsonAdapter(employeeRequest.Data),
            "xml" => new XmlAdapter(employeeRequest.Data),
            "cs" => new CsAdapter(employeeRequest.Data),
            "custom" => new CustomAdapter(employeeRequest.Data),
            _ => throw new NotImplementedException()
        };
        var command = employeeAdapter.AdapteToCreateCommand();
        command.OverTimeCalculator = employeeRequest.OverTimeCalculator;
        _mediator.Send(command);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetSalaryEmployeeQueries queries)
    {
        return Ok(_mediator.Send(queries));
    }

    [HttpGet("Range")]
    public IActionResult Get([FromQuery] GetRangeSalaryEmployeeQueries queries)
    {
        return Ok(_mediator.Send(queries));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateSalaryEmployeeCommand command)
    {
        return Ok(_mediator.Send(command));
    }

    [HttpDelete("{salaryId}")]
    public async Task<IActionResult> Delete(int salaryId)
    {
        DeleteSalaryCommand command = new() { SalaryId = salaryId };
        return Ok(_mediator.Send(command));
    }
}
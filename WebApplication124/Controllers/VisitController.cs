using Microsoft.AspNetCore.Mvc;
using WebApplication124.Services;

namespace WebApplication124.Controllers;


[ApiController]
[Route("api/[controller]")]
public class VisitController : ControllerBase
{
    private readonly string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=KOL_PRZYKLAD_2;Integrated Security=True;";
    private readonly IVisitService _service;
    
    
    public VisitController(IVisitService service)
    {
        _service = service;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> getInfoForVisitById(int id)
    {
        return await _service.getInfoForVisitById(id);
    }
}
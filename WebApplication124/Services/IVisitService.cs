using Microsoft.AspNetCore.Mvc;

namespace WebApplication124.Services;

public interface IVisitService
{
    public Task<IActionResult> getInfoForVisitById(int visitId);
}
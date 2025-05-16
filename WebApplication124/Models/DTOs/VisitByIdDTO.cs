namespace WebApplication124.Models.DTOs;

public class VisitByIdDTO
{
    public DateTime Date { get; set; }
    public PatientsInVisitIdDTO Patient { get; set; }
    public DoctorInVisitIdDTO Doctor { get; set; }
    public List<ApointmentServiceInVisitIdDTO> Appointments { get; set; }
}

public class PatientsInVisitIdDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}

public class DoctorInVisitIdDTO
{
    public int DoctorId { get; set; }
    public string Pwz {get; set;}
}

public class ApointmentServiceInVisitIdDTO
{
    public string ApointmentName { get; set; }
    public decimal ServiceFee { get; set; }
}
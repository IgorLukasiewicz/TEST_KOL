using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplication124.Models.DTOs;

namespace WebApplication124.Services;

public class VisitService : IVisitService
{
    private readonly string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=KOL_PRZYKLAD_2;Integrated Security=True;";

    public async Task<IActionResult> getInfoForVisitById(int visitId)
    {
        var toBeReturned = new Dictionary<int, VisitByIdDTO>();

        string sql =
            @"SELECT A.appoitment_id, A.date, P.first_name, P.last_name, P.date_of_birth, D.doctor_id, D.PWZ, S.name, S.base_fee
                      From appointment A
                      JOIN PATIENT P ON A.PATIENT_ID = P.PATIENT_ID
                      JOIN DOCTOR D ON A.DOCTOR_ID = D.DOCTOR_ID
                      JOIN APPOINTMENT_SERVICE APS ON APS.appoitment_id = A.appoitment_id
                      JOIN SERVICE S ON s.service_id = APS.service_id
                      WHERE P.patient_id = @patientId";
        
        
        
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@patientId", visitId);
        
        await conn.OpenAsync();
        using SqlDataReader reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            int appoitment_id = reader.GetInt32(reader.GetOrdinal("appoitment_id"));

            if (!toBeReturned.ContainsKey(appoitment_id))
            {
                toBeReturned[appoitment_id] = new VisitByIdDTO
                {
                    Date = reader.GetDateTime(reader.GetOrdinal("date")),

                    Patient = new PatientsInVisitIdDTO()
                    {
                        FirstName = reader.GetString(reader.GetOrdinal("first_name")),
                        LastName = reader.GetString(reader.GetOrdinal("last_name")),
                        DateOfBirth = reader.GetDateTime(reader.GetOrdinal("date_of_birth")),
                    },
                    Doctor = new DoctorInVisitIdDTO()
                    {
                        DoctorId = reader.GetInt32(reader.GetOrdinal("doctor_id")),
                        Pwz = reader.GetString(reader.GetOrdinal("PWZ")),
                    },

                    Appointments = new List<ApointmentServiceInVisitIdDTO>()
                };
            }

            var newApointment = new ApointmentServiceInVisitIdDTO
            {
                ApointmentName = reader.GetString(reader.GetOrdinal("name")),
                ServiceFee = reader.GetDecimal(reader.GetOrdinal("base_fee")),
            };
            
            
            var current = toBeReturned[appoitment_id];
            if (!current.Appointments.Any(a =>
                    a.ApointmentName == newApointment.ApointmentName
                    && a.ServiceFee  == newApointment.ServiceFee))
            {
                current.Appointments.Add(newApointment);
            }
       

    }
        return new OkObjectResult(toBeReturned.Values.ToList());
    }
    
}
using ASPNETCoreApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;


namespace ASPNETCoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public EmployeesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("GetAllEmployees")]
        public string GetEmployees()
        {
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("Employee"));
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Employee\r\n", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Employee> employeeList = new List<Employee>();
            Response response = new Response();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Employee employee = new Employee();

                    employee.EmpId = Convert.ToInt32(dt.Rows[i]["EmpId"]);
                    employee.EmpName = Convert.ToString(dt.Rows[i]["EmpName"]);
                    employee.Password = Convert.ToString(dt.Rows[i]["Password"]);

                    employeeList.Add(employee);

                }

            }
            if (employeeList.Count > 0)
            {
                return JsonConvert.SerializeObject(employeeList);
            }
            else
            {
                response.StatusCode = "Error";
                response.ErrorMessage = "No Data Found";
                return JsonConvert.SerializeObject(response);


            }
        }
    }
}

using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class SampleDataAccess
    {
        private readonly IMemoryCache _memoryCache;

        public SampleDataAccess(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        //============SYNCHRONOUS CALL================
        public List<EmployeeModel> GetEmployees()
        {
            List<EmployeeModel> output = new();

            output.Add(new() { FirstName = "Tim", LastName = "Corey" });
            output.Add(new() { FirstName = "Sue", LastName = "Storm" });
            output.Add(new() { FirstName = "Jane", LastName = "Jones" });

            Thread.Sleep(3000); //THIS WILL DELAY THE OUTPUT RETURNING FOR 3 SECOND.

            return output;
        }

        //============ASYNCHRONOUS CALL================
        public async Task<List<EmployeeModel>> GetEmployeesAsync()
        {
            List<EmployeeModel> output = new();

            output.Add(new() { FirstName = "Tim", LastName = "Corey" });
            output.Add(new() { FirstName = "Sue", LastName = "Storm" });
            output.Add(new() { FirstName = "Jane", LastName = "Jones" });

            await Task.Delay(3000); //THIS IS SIMILAR TO ABOVE BUT IT IS ASYNCHRONOUS 

            return output;
        }

        //============ASYNCHRONOUS CALL WITH CACHE================
        public async Task<List<EmployeeModel>> GetEmployeesCache()
        {
            List<EmployeeModel> output;

            output = _memoryCache.Get<List<EmployeeModel>>("employees");
            //employees  IS THE KEY OF HASH WHERE THE VALUE IS THE LIST.

            if (output is null)
            {
                output = new();

                output.Add(new() { FirstName = "Tim", LastName = "Corey" });
                output.Add(new() { FirstName = "Sue", LastName = "Storm" });
                output.Add(new() { FirstName = "Jane", LastName = "Jones" });

                await Task.Delay(3000);

                _memoryCache.Set("employees", output, TimeSpan.FromMinutes(1)); //THIS WILL CREATE ABSOLUTE CACHE THAT WILL STAY FOR ONE MINUTE.After one minute it call server for data.
            }

            return output;
        }
    }
}

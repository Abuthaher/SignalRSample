using SampleNotifiaction.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SampleNotifiaction.Repository
{
    public class Repository
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=ProductDB;Integrated Security=True");
        string SqlCon = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=ProductDB;Integrated Security=True";
        public List<Employee> LoadData()
        {
            var messages = new List<Employee>();

            //SqlDependency.Start(@"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=ProductDB;Integrated Security=True");
            using (var cmd = new SqlCommand(@"SELECT [empId],[empName],[Salary],[DeptName],[Designation] FROM [dbo].[Employee]", con))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //dependency = new SqlDependency(cmd);
                //dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                DataSet ds = new DataSet();
                da.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    messages.Add(item: new Employee
                    {
                        empId = int.Parse(ds.Tables[0].Rows[i][0].ToString()),
                        empName = ds.Tables[0].Rows[i][1].ToString(),
                        Salary = Convert.ToInt32(ds.Tables[0].Rows[i][2]),
                        DeptName = ds.Tables[0].Rows[i][3].ToString(),
                        Designation = ds.Tables[0].Rows[i][4].ToString(),
                    });
                }
            }
            return messages;
        }
    }
}

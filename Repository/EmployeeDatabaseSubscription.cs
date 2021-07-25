using Microsoft.AspNetCore.SignalR;
using SampleNotifiaction.Hubs;
using SampleNotifiaction.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;

namespace SampleNotifiaction.Repository
{
    public class EmployeeDatabaseSubscription : IDatabaseSubscription, IDisposable
    {
        private bool disposedValue = false;
        
        private readonly IHubContext<MyHub> _hubContext;
        private SqlTableDependency<Employee> _tableDependency;
        private SqlDependency _dependency;
        private string Connection;

        public EmployeeDatabaseSubscription(IHubContext<MyHub> hubContext)
        {            
            _hubContext = hubContext;
        }

        public void Configure(string connectionString)
        {
            Connection = connectionString;
            _tableDependency = new SqlTableDependency<Employee>(connectionString, null, null, null, null, null, DmlTriggerType.All);
            _tableDependency.OnChanged += Changed;
            //_tableDependency.OnError += TableDependency_OnError;
            _tableDependency.Start();

            //SqlDependency.Start(connectionString);
            //_dependency = new SqlDependency(new SqlCommand(@"SELECT [empId],[empName],[Salary],[DeptName],[Designation] FROM [dbo].[Employee]", new SqlConnection(connectionString)));
            //_dependency.OnChange += dependency_OnChange;
        }

        private void Changed(object sender, RecordChangedEventArgs<Employee> e)
        {
            if (e.ChangeType != ChangeType.None)
            {                
                var changedEntity = e.Entity;
                _hubContext.Clients.All.SendAsync("ReceiveMessage");

            }
        }

        //private void dependency_OnChange(object sender, SqlNotificationEventArgs e) //this will be called when any changes occur in db table.  
        //{
        //    if (e.Type == SqlNotificationType.Change)
        //    {
        //        _hubContext.Clients.All.SendAsync("ReceiveMessage");                
        //    }

        //}

        ~EmployeeDatabaseSubscription()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _tableDependency.OnChanged -= Changed;
                    _tableDependency.Stop();
                    _tableDependency = null;
                    //SqlDependency.Stop(Connection);
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
    }
}

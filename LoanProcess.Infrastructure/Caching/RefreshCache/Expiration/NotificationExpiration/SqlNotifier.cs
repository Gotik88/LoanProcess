using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanProcess.Infrastructure.Caching.RefreshCache.Expiration.NotificationExpiration
{
    public class SqlNotifier : IDisposable
    {
        public SqlCommand CurrentCommand { get; set; }
        private SqlConnection connection;
        public SqlConnection CurrentConnection
        {
            get
            {
                this.connection = this.connection ??
            new SqlConnection(this.ConnectionString);
                return this.connection;
            }
        }
        public string ConnectionString
        {
            get
            {
                return @"Data Source=VALUE-699460DF8\SQLEXPRESS;
                		Initial Catalog=Northwind;Integrated Security=True";
            }
        }

        public SqlNotifier()
        {
            SqlDependency.Start(this.ConnectionString);
        }
        private event EventHandler<SqlNotificationEventArgs> _newMessage;

        public event EventHandler<SqlNotificationEventArgs> NewMessage
        {
            add
            {
                this._newMessage += value;
            }
            remove
            {
                this._newMessage -= value;
            }
        }

        public virtual void OnNewMessage(SqlNotificationEventArgs notification)
        {
            if (this._newMessage != null)
                this._newMessage(this, notification);
        }
        public DataTable RegisterDependency()
        {

            this.CurrentCommand = new SqlCommand("Select [MID],[MsgString],[MsgDesc] from dbo.Message", this.CurrentConnection)
            {
                Notification = null
            };

            var dependency = new SqlDependency(this.CurrentCommand);
            dependency.OnChange += this.dependency_OnChange;

            if (this.CurrentConnection.State == ConnectionState.Closed)
                this.CurrentConnection.Open();
            try
            {

                var dt = new DataTable();
                dt.Load(this.CurrentCommand.ExecuteReader(CommandBehavior.CloseConnection));

                return dt;
            }
            catch { return null; }
        }

        void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            var dependency = sender as SqlDependency;
            dependency.OnChange -= new OnChangeEventHandler(dependency_OnChange);

            this.OnNewMessage(e);
        }
        public void Insert(string msgTitle, string description)
        {
            using (var con = new SqlConnection(this.ConnectionString))
            {
                using (var cmd = new SqlCommand("usp_CreateMessage", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@title", msgTitle);
                    cmd.Parameters.AddWithValue("@description", description);

                    con.Open();

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            SqlDependency.Stop(this.ConnectionString);
        }

        #endregion
    }
}

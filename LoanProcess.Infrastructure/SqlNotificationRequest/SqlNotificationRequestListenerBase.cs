// ============================================================================
// <copyright file="SqlDependencyCacheWatcher.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.Watchers
{
    using System;
    using System.Configuration;
    using System.Data.SqlClient;

    public abstract class SqlNotificationRequestListenerBase : IListiner
    {
        private readonly int _objectID = System.Threading.Interlocked.Increment(ref _objectTypeCount);
        private static int _objectTypeCount; // Bid counter
        internal int ObjectID
        {
            get
            {
                return _objectID;
            }
        }

        public delegate void AsyncListenerDelegate();
        private static bool _isAlreadyListening = false;

        // it will be wrapped inside configuration manager
        protected const string ServiceName = "Service=ContactChangeNotifications";
        protected const string tableName = "Contacts";
        private string Queue;
        private string localDb;
        private string listinerSql;
        private string connectionString;
        private int NotificationTimeOut;


        protected SqlNotificationRequestListenerBase()
        {
            NotificationTimeOut = 100;
            Queue = "ContactChangeMessages";
            localDb = "TestDB";

            connectionString = String.Format("Data Source=(local);Integrated Security=true;" +
                                             "Initial Catalog={0};Pooling=False;Asynchronous Processing=true;", localDb);

            listinerSql = "SELECT ContactID, FirstName, LastName, " +
        "EmailAddress, EmailPromotion " +
        "FROM Person.Contact " +
        "WHERE EmailPromotion IS NOT NULL;";

            listinerSql = string.Format("WAITFOR (RECEIVE * FROM {0});", Queue);

            ////SqlDependencyPerAppDomainDispatcher.SingletonInstance.AddDependencyEntry(this);
        }

        /*internal static SqlDependencyProcessDispatcher ProcessDispatcher
        {
            get
            {
                return _processDispatcher;
            }
        }*/

        protected abstract void DoListen();

        public void StartListen()
        {
            if (_isAlreadyListening) return;

            var asyncListener = new AsyncListenerDelegate(Listen);
            asyncListener.BeginInvoke(StopListening, null);
            _isAlreadyListening = true;
        }



        private void Listen()
        {
            while (true)
            {
                //create the command that will listen to the queue 
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BlogExampleDBConnectionString"].ConnectionString))
                {
                    connection.Open();

                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "WAITFOR (RECEIVE * FROM PersonChangeMessages);";
                    command.CommandTimeout = 60 * 5; //listen in 5 minute increments 

                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                        }
                    }
                    catch (SqlException)
                    {
                        //if the query times out, that means that no messages have 
                        //been sent to the queue yet, so we should keep listening. 
                    }
                }
            }
        }

        public void StopListening()
        {

        }

        protected void StopListening(IAsyncResult result)
        {
            //no information needed here. 
        }

        /*private void OnReaderComplete(IAsyncResult asynResult)
        {
            // You may not interact with the form and its contents
            // from a different thread, and this callback procedure
            // is all but guaranteed to be running from a different thread
            // than the form. Therefore you cannot simply call code that 
            // updates the UI.
            // Instead, you must call the procedure from the form's thread.
            // This code will use recursion to switch from the thread pool
            // to the UI thread.
            if (this.InvokeRequired == true)
            {
                AsyncCallback switchThreads = new AsyncCallback(this.OnReaderComplete);
                object[] args = { asynResult };
                this.BeginInvoke(switchThreads, args);
                return;
            }
            // At this point, this code will run on the UI thread.
            try
            {
                waitInProgress = false;
                SqlDataReader reader = ((SqlCommand)asynResult.AsyncState)
                    .EndExecuteReader(asynResult);
                while (reader.Read())
                // Empty queue of messages.
                // Application logic could partse
                // the queue data to determine why things.
                {
                    for (int i = 0; i <= reader.FieldCount - 1; i++)
                        Debug.WriteLine(reader[i].ToString());
                }

                reader.Close();
                changeCount += 1;

            }
            catch (Exception ex)
            {
            }
        }*/

        // Code requires directives to
        // System.Security.Permissions and
        // System.Data.SqlClient

        /*private bool CanRequestNotifications()
        {
            var permission = new SqlClientPermission(PermissionState.Unrestricted);
            try
            {
                permission.Demand();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }*/
    }
}

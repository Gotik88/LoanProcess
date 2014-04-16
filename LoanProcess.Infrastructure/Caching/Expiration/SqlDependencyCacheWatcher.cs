// ============================================================================
// <copyright file="SqlDependencyCacheWatcher.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.Caching.Expiration
{
    using System;
    using System.Data.SqlClient;

    public class SqlDependencyCacheWatcher : CacheDependencyListenerBase, IWather
    {
        private readonly string connectionString;
        private string sqlQueue;
        private readonly string listenerQuery;
        private SqlDependency dependency;

        public SqlDependencyCacheWatcher(string tableName)
        {
            this.connectionString = connectionString;
            this.sqlQueue = sqlQueue;
            this.listenerQuery = listenerQuery;
            this.dependency = null;
        }

        public void Start()
        {
            SqlDependency.Start(connectionString);
            ListenForChanges();
        }

        public void Stop()
        {
            SqlDependency.Stop(this.connectionString);
        }

        private void ListenForChanges()
        {
            //Remove existing dependency, if necessary
            UnSubscribe();

            var connection = new SqlConnection(connectionString);
            connection.Open();

            var command = new SqlCommand(listenerQuery, connection);

            dependency = new SqlDependency(command);
            dependency.OnChange += new OnChangeEventHandler(OnDependencyChange);

            SqlDependency.Start(connectionString);
            command.ExecuteReader();
            connection.Close();
        }

        private void OnDependencyChange(Object o, SqlNotificationEventArgs args)
        {
            if ((args.Source.ToString() == "Data") || (args.Source.ToString() == "Timeout"))
            {
                Console.WriteLine(Environment.NewLine + "Refreshing data due to {0}", args.Source);
                Notify(CacheDependencyChangeTypes.Changed);
                ListenForChanges();
            }
            else
            {
                Console.WriteLine(Environment.NewLine + "Data not refreshed due to unexpected SqlNotificationEventArgs: Source={0}, Info={1}, Type={2}", args.Source, args.Info, args.Type.ToString());
            }
        }

        private void UnSubscribe()
        {
            if (dependency != null)
            {
                dependency.OnChange -= OnDependencyChange;
                dependency = null;
            }
        }
    }
}

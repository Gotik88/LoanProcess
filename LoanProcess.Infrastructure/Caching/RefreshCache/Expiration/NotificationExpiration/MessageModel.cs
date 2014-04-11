using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanProcess.Infrastructure.Caching.RefreshCache.Expiration.NotificationExpiration
{
    public class MessageModel
    {
        public Dispatcher UIDispatcher { get; set; }

        public SqlNotifier Notifier { get; set; }

        public MessageModel(Dispatcher uidispatcher)
        {
            this.UIDispatcher = uidispatcher;
            this.Notifier = new SqlNotifier();

            this.Notifier.NewMessage += new EventHandler<SqlNotificationEventArgs>
                (notifier_NewMessage);
            DataTable dt = this.Notifier.RegisterDependency();

            this.LoadMessage(dt);
        }

        private void notifier_NewMessage(object sender, SqlNotificationEventArgs e)
        {
            this.LoadMessage(this.Notifier.RegisterDependency());
        }

        private void LoadMessage(DataTable dt)
        {
            this.UIDispatcher.BeginInvoke((Action)delegate()
            {
                if (dt != null)
                {
                    this.Messages.Clear();

                    foreach (DataRow drow in dt.Rows)
                    {
                        Message msg = new Message
                        {
                            Id = Convert.ToString(drow["MID"]),
                            Title = drow["MsgString"] as string,
                            Description = drow["MsgDesc"] as string
                        };
                        this.Messages.Add(msg);
                    }
                }
            });
        }
    }
}

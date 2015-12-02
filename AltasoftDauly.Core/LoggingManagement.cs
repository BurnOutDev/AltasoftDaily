using AltasoftDaily.Domain.POCO;
using AltasoftDaily.Domain.POCO.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Core
{
    public static class LoggingManagement
    {
        public static bool LogException(Exception ex, User user)
        {
            if (ex == null)
                return false;

            using (var db = new AltasoftDailyContext())
            {
                db.ExceptionLogs.Add(CreateExceptionLog(ex, user != null ? db.Users.FirstOrDefault(x => x.UserID == user.UserID) : null));
                db.SaveChanges();
            }

            return true;
        }

        private static ExceptionLog CreateExceptionLog(Exception ex, User user, bool isInner = false)
        {
            var exLog = new ExceptionLog();

            exLog.Date = DateTime.Now;
            exLog.User = user;

            exLog.IsInner = isInner;

            exLog.Message = ex.Message;
            exLog.Source = ex.Source;
            exLog.StackTrace = ex.StackTrace;

            if (ex.InnerException != null)
                exLog.InnerException = CreateExceptionLog(ex.InnerException, user, true);

            return exLog;
        }

        public static bool LogComment(DailyPayment payment, User user)
        {
            using (var db = new AltasoftDailyContext())
            {
                var comment = db.CommentLogs.Create();

                comment.User = db.Users.FirstOrDefault(x => user.UserID == x.UserID);
                comment.CommentValue = payment.Comment;
                comment.Date = DateTime.Now;
                comment.LocalPayment = db.DailyPayments.FirstOrDefault(x => x.DailyPaymentID == payment.DailyPaymentID);

                db.CommentLogs.Add(comment);
                db.SaveChanges();
            }

            return true;
        }

        public static bool LogOrder(DailyPayment payment, User user)
        {
            if (!payment.OrderID.HasValue)
                return false;

            using (var db = new AltasoftDailyContext())
            {
                var order = db.OrderLogs.Create();

                order.Amount = payment.Payment;
                order.Date = DateTime.Now;
                order.LocalPayment = db.DailyPayments.FirstOrDefault(x => x.DailyPaymentID == payment.DailyPaymentID);
                order.OrderID = payment.OrderID.Value;
                order.User = db.Users.FirstOrDefault(x => x.UserID == user.UserID);

                db.OrderLogs.Add(order);
                db.SaveChanges();
            }

            return true;
        }

        public static bool LogSign(SignType signType, User user)
        {
            using (var db = new AltasoftDailyContext())
            {
                var sign = db.SignLogs.Create();

                sign.User = user != null ? db.Users.FirstOrDefault(x => x.UserID == user.UserID) : null;
                sign.Date = DateTime.Now;
                sign.InternalUsername = Environment.UserName;
                sign.SignType = signType;

                db.SignLogs.Add(sign);
                db.SaveChanges();
            }

            return true;
        }
    }
}

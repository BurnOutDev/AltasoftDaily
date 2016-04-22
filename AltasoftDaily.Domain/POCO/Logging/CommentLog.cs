using AltasoftDaily.Domain.POCO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Domain.POCO.Logging
{
    public class CommentLog
    {
        [Key]
        public int CommentLogID { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }

        public CommentType Type { get; set; }
        public string CommentValue { get; set; }

        public DailyPayment LocalPayment { get; set; }
    }

    public enum CommentType
    {
        [Description("მოგვიანებით დავუკავშირდები")]
        CallLater,

        [Description("მიუწვდომელია")]
        CannotBeReached,

        [Description("თანხა არ აქვს")]
        NoMoney,

        [Description("ინფორმაციის უზუსტობა")]
        InaccurateInformation,

        [Description("სასამართლო")]
        Court,

        [Description("ინკასატორი არ მივიდა")]
        CashierNotVisited,

        [Description("გადაიხდის")]
        WillPay,

        [Description("გადაიხადა")]
        Paid,

        [Description("სხვა")]
        Other    
    }
}

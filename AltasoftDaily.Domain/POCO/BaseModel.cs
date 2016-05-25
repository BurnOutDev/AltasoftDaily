using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Domain.POCO
{
    public class BaseModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;


    }
}

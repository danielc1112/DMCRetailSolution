using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DataAccess.Help;

namespace DataAccess.Entity
{
    public class PropertyError : NotifyUIBase
    {
        
        private string propertyName;
        [Key]
        public string PropertyName
        {
            get
            {
                return propertyName;
            }
            set
            {
                propertyName = value;
                RaisePropertyChanged();
            }
        }
        private string error;
        public string Error
        {
            get
            {
                return error;
            }
            set
            {
                error = value;
                RaisePropertyChanged();
            }
        }
        private bool added;
        public bool Added
        {
            get
            {
                return added;
            }
            set
            {
                added = value;
                RaisePropertyChanged();
            }
        }
        
        
        
    }
}

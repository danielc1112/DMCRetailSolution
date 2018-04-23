using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Help;
using GalaSoft.MvvmLight.Command;
using System.Diagnostics;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;

namespace DataAccess.Entity
{
    public class KeyedEntity : BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}

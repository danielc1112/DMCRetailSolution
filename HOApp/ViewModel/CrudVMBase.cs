using DataAccess.Help;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Media;
using HOApp.Messages;
using HOApp.Help;

namespace HOApp.ViewModel
{
    public class CrudVMBase : DataVMBase
    {
        protected object selectedEntity;
        protected object editEntity;
        public CommandVM SaveCmd { get; set; }
        public CommandVM QuitCmd { get; set; }
        public ObservableCollection<CommandVM> Commands { get; set; }
        protected void HandleCommand(CommandMessage action)
        {
            switch (action.Command)
            {
                case CommandType.Insert:
                    InsertNew();
                    break;
                case CommandType.Edit:
                    if (GotSomethingSelected())
                    {
                        EditCurrent();
                    }
                    break;
                case CommandType.Delete:
                    if (GotSomethingSelected())
                    {
                        DeleteCurrent();
                    }
                    break;
                case CommandType.Commit:
                    CommitUpdates();
                    break;
                case CommandType.Refresh:
                    RefreshData();
                    editEntity = null;
                    selectedEntity = null;
                    break;
                case CommandType.Quit:
                    Quit();
                    break;
                case CommandType.None:
                    break;
                default:
                    break;
            }
        }
        public bool GotSomethingSelected()
        {
            bool OK = true;
            if (selectedEntity == null)
            {
                OK = false;
                ShowUserMessage("You must choose a record to work on");
            }
            return OK;
        }
        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                errorMessage = value;
                RaisePropertyChanged();
            }
        }
        private string textSearch;
        public string TextSearch
        {
            get { return textSearch; }
            set
            {
                textSearch = value;
                GetData(textSearch);
                RaisePropertyChanged();
            }
        }
        protected void ShowUserMessage(string message)
        {
            UserMessage msg = new UserMessage { Message = message };
            Messenger.Default.Send<UserMessage>(msg);
        }

        protected CrudVMBase() : base()
        {
            ObservableCollection<CommandVM> commands = new ObservableCollection<CommandVM>
            {
                new CommandVM{ CommandDisplay="Insert", IconGeometry=Application.Current.Resources["InsertIcon"] as Geometry , Message=new CommandMessage{ Command =CommandType.Insert}},
                new CommandVM{ CommandDisplay="Edit", IconGeometry=Application.Current.Resources["EditIcon"] as Geometry , Message=new CommandMessage{ Command = CommandType.Edit}},
                new CommandVM{ CommandDisplay="Delete", IconGeometry=Application.Current.Resources["DeleteIcon"] as Geometry , Message=new CommandMessage{ Command = CommandType.Delete}},
                new CommandVM{ CommandDisplay="Refresh", IconGeometry=Application.Current.Resources["RefreshIcon"] as Geometry , Message=new CommandMessage{ Command = CommandType.Refresh}}

            };
            Commands = commands;
            RaisePropertyChanged("Commands");
            Messenger.Default.Register<CommandMessage>(this, (action) => HandleCommand(action));
            SaveCmd = new CommandVM
            {
                CommandDisplay = "Commit",
                IconGeometry = Application.Current.Resources["SaveIcon"] as Geometry,
                Message = new CommandMessage { Command = CommandType.Commit }
            };
            QuitCmd = new CommandVM
            {
                CommandDisplay = "Quit",
                IconGeometry = Application.Current.Resources["QuitIcon"] as Geometry,
                Message = new CommandMessage { Command = CommandType.Quit }
            };
        }
        protected virtual void Quit()
        {
        }
        protected virtual void InsertNew()
        {
        }
        protected virtual void EditCurrent()
        {
        }
        protected virtual void CommitUpdates()
        {
        }
        protected virtual void DeleteCurrent()
        {
        }

        protected virtual void RefreshData(string filter = "")
        {
            GetData(filter);
            Messenger.Default.Send<UserMessage>(new UserMessage { Message = "Data Refreshed" });
        }

        // Used to control showing a pop up to edit an entity in
        private bool isInEditMode = false;        
        public bool IsInEditMode
        {
            get
            {
                return isInEditMode;
            }
            set
            {
                isInEditMode = value;
                RaisePropertyChanged();
            }
        }
    }
}

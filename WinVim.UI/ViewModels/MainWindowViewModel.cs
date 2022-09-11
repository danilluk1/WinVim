using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinVim.BL;
using WinVim.BL.Windows;

namespace WinVim.UI.ViewModels {
    internal class MainWindowViewModel {
        private MessageHandler messageHandler;
        private MessageBroker messageBroker;

        internal MainWindowViewModel() {
            messageBroker = new MessageBroker();
            messageHandler = new MessageHandler(Settings.GetInstance());
        }
    }
}

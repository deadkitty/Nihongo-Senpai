using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace NihongoSenpaiTsu.Common
{
    public static class MessageBox
    {
        public enum EMessageBoxResult
        {
            ok,
            cancel,
            count,
            undefined = -1,
        }

        public enum EMessageBoxButtons
        {
            Ok,
            OkCancel,
            count,
            undefined = -1,
        }

        public delegate void ResultHandler(EMessageBoxResult result);

        private static ResultHandler handler;

        public static async void Show(String text, String caption = null, EMessageBoxButtons buttons = EMessageBoxButtons.Ok, ResultHandler handler = null)
        {
            MessageBox.handler = handler;

            MessageDialog dialog;

            if (caption != null)
            {
                dialog = new MessageDialog(text, caption);
            }
            else
            {
                dialog = new MessageDialog(text);
            }

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            dialog.Commands.Add(new UICommand("ok", new UICommandInvokedHandler(CommandInvokedHandler)));

            if(buttons == EMessageBoxButtons.OkCancel)
            {
                dialog.Commands.Add(new UICommand("cancel", new UICommandInvokedHandler(CommandInvokedHandler)));
            }

            // Show the message dialog
            await dialog.ShowAsync();
        }

        private static void CommandInvokedHandler(IUICommand command)
        {
            if(handler != null)
            {
                switch (command.Label)
                {
                    case "ok"    : handler(EMessageBoxResult.ok); break;
                    case "cancel": handler(EMessageBoxResult.cancel); break;
                    default      : handler(EMessageBoxResult.undefined); break;
                }
            }

            handler = null;
        }
    }
}

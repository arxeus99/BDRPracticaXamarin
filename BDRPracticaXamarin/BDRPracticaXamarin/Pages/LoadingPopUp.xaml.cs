using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BDRPracticaXamarin.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingPopUp : Popup
    {
        public LoadingPopUp(string Message)
        {
            InitializeComponent();

            messageText.Text = Message;
        }


        public void CloseThis()
        {
            Dismiss(null);
        }
    }
}
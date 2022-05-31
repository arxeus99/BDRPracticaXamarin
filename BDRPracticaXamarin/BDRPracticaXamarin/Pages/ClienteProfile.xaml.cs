using BDRPracticaXamarin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BDRPracticaXamarin.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClienteProfile : ContentPage
    {

        private bool _nuevo;
        private Cliente _cliente;
        private List<Pais> paises;
        private APIConnectionHelper apiConnection;

        public ClienteProfile(Cliente cliente, bool nuevo)
        {
            InitializeComponent();

            this._nuevo = nuevo;
            this._cliente = cliente;

        }

        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                apiConnection = new APIConnectionHelper();

                paises = await apiConnection.GetPaises();

                PaisPicker.ItemsSource = paises;

                if (_nuevo)
                    IdLayout.IsVisible = false;
                else
                {
                    IdLayout.IsVisible = true;
                    IdEntry.Text = _cliente.Id.ToString();
                    NombreEntry.Text = _cliente.Nombre;
                    DireccionEntry.Text = _cliente.Direccion;
                    PoblacionEntry.Text = _cliente.Poblacion;
                    var pais = (from p in paises where p.Id == _cliente.IdPais select p).FirstOrDefault();
                    PaisPicker.SelectedIndex = PaisPicker.ItemsSource.IndexOf(
                       pais
                    );
                    TelefonoEntry.Text = _cliente.Telefono;
                    EmailEntry.Text = _cliente.Email;

                }
            }
            catch (Exception ex) {
                Console.WriteLine("Error " + ex.ToString());
                await DisplayAlert("Error", ex.Message, "Ok"); 
            }
        }

        private async void CancelarButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.ToString());

                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        private async void GuardarButton_Clicked(object sender, EventArgs e)
        {
            try
            {

                _cliente.Nombre = NombreEntry.Text;
                _cliente.Direccion = DireccionEntry.Text;
                _cliente.Poblacion = PoblacionEntry.Text;
                _cliente.IdPais = ((Pais)PaisPicker.SelectedItem).Id;
                _cliente.Telefono = TelefonoEntry.Text;
                _cliente.Email = EmailEntry.Text;

                string result = "";

                apiConnection = new APIConnectionHelper();

                LoadingPopUp popUp = new LoadingPopUp("Guardando cliente...");

                Application.Current.MainPage.Navigation.ShowPopup(popUp);

                if (_nuevo)
                    result = await apiConnection.InsertCliente(_cliente);
                else
                    result = await apiConnection.UpdateCliente(_cliente);

                popUp.CloseThis();

                await DisplayAlert("Resultado", result, "Ok");

                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.ToString());
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }
    }
}
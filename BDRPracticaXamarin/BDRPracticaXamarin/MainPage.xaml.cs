using BDRPracticaXamarin.Models;
using BDRPracticaXamarin.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace BDRPracticaXamarin
{
    public partial class MainPage : ContentPage
    {

        public ObservableCollection<Cliente> Clientes { get; } = new ObservableCollection<Cliente>();

        APIConnectionHelper connectionHelper;

        List<SwipeView> OpenedSwipes;

        public MainPage()
        {
            InitializeComponent();

            BindingContext = this;

            OpenedSwipes = new List<SwipeView>();
            connectionHelper = new APIConnectionHelper();
        }

        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                
                var clientes = await connectionHelper.GetClientes();

                Clientes.Clear();

                foreach (Cliente c in clientes)
                    Clientes.Add(c);
                
            }catch(Exception ex)
            {
                Console.WriteLine("Error " + ex.ToString());
                await DisplayAlert("Error", ex.Message, "Ok");
            }
            
        }
        private async void SwipeView_SwipeStarted(object sender, SwipeStartedEventArgs e)
        {
            try
            {
                if (OpenedSwipes.Count == 1)
                {
                    OpenedSwipes[0].Close();
                    OpenedSwipes.Remove(OpenedSwipes[0]);
                }
            }catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.ToString());
                await DisplayAlert("Error", ex.Message, "Ok");
            }

        }

        private async void SwipeView_SwipeEnded(object sender, SwipeEndedEventArgs e)
        {
            try
            {
                SwipeView swipe = sender as SwipeView;

                OpenedSwipes.Add(swipe);
            }catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.ToString());
                await DisplayAlert("Error", ex.Message, "Ok");
            }

        }

        private async void NuevoClienteButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                Cliente cliente = new Cliente();

                await Application.Current.MainPage.Navigation.PushAsync(new ClienteProfile(cliente, true));

 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.ToString());
                await DisplayAlert("Error", ex.Message, "Ok");
            }

        }

        private async void DeletSwipe_Invoked(object sender, EventArgs e)
        {
            try
            {
                var cliente = ((SwipeItem)sender).BindingContext as Cliente;

                if (cliente == null)
                    return;

                bool confirmar = await DisplayAlert("Confirmar", $"¿Quiere borrar el cliente {cliente.Nombre}?", "Sí", "No");


                if (!confirmar)
                    return;


                string result = await connectionHelper.DeleteCliente(cliente.Id);

                await DisplayAlert("Resultado", result, "Ok");

                await ActualizarLista();

          
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.ToString());
                await DisplayAlert("Error", ex.Message, "Ok");
            }
            
        }

        private async void EditSwipe_Invoked(object sender, EventArgs e)
        {
            try
            {
                var cliente = ((SwipeItem)sender).BindingContext as Cliente;

                if (cliente == null)
                    return;

                await Application.Current.MainPage.Navigation.PushAsync(new ClienteProfile(cliente, false));

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.ToString());
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        private async void ClientesListView_Refreshing(object sender, EventArgs e)
        {
            await ActualizarLista();
        }

        private async Task ActualizarLista()
        {
            ClientesListView.IsRefreshing = true;

            connectionHelper = new APIConnectionHelper();

            var clientes = await connectionHelper.GetClientes();

            Clientes.Clear();

            foreach (Cliente c in clientes)
                Clientes.Add(c);

            ClientesListView.ItemsSource = null;
            ClientesListView.ItemsSource = Clientes;

            ClientesListView.IsRefreshing = false;
        }
    }
}

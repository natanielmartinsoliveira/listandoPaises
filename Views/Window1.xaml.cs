using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net;
using System.IO;
using ListandoPaises.ViewModels;
using System.Net.Http;
using Newtonsoft.Json;



using ListandoPaises.Model;namespace ListandoPaises.Views
{
    /// <summary>
    /// Author: Nataniel Martins
    /// </summary>
    public partial class Window1 : Window
    {
        List<string> nameList = new List<string>();
        List<Country> pais = null;
        Country item = null;
        public Window1()
        {
            InitializeComponent();

            //Cria viewmodel e chama função carregar imagem padrão.
            var model = new Windows1ViewModel();
            DataContext = model;
            model.SetImageData(@"");

            //Cria inicia Cliente http
            var client = new HttpClient();
            //acessa o link via async para carregar json 
            var task = client.GetAsync("https://restcountries.eu/rest/v2/all?fields=name;capital;altSpellings;region;population;nativeName;flag")
              .ContinueWith((taskwithresponse) =>
              {
                  //recebe os dados da conexão
                  var response = taskwithresponse.Result;
                  var jsonString = response.Content.ReadAsStringAsync();
                  jsonString.Wait();
                  //Serializa os dados do json
                  pais = JsonConvert.DeserializeObject<List<Country>>(jsonString.Result);

              });
            //espera finalizar a conexão async
            task.Wait();

            //preencher a lista de nomes para a busca
            foreach (var country in pais)
            {
                nameList.Add(country.name.ToString()); 
            }


        }

        private void txtname_TextChanged(object sender, TextChangedEventArgs e)
        {
            //seta dados digitados e a cria lista em branco
            string typedString = textBox1.Text;
            List<string> autoList = new List<string>();
            autoList.Clear();

            //percorre a lista preenchida com nomes e compara com o que foi digitado
            foreach (string item in nameList)
            {
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    string str = item.ToLower();
                    if (str.Contains(typedString.ToLower()))
                    {
                        autoList.Add(item);
                    }
                }
            }
            //exibe a lista se quantidade de lista encontrada for maior q zero
            if (autoList.Count > 0)
            {
                listBox1.ItemsSource = autoList;
                listBox1.Visibility = Visibility.Visible;
            }
            //fecha se o textboox estiver branco
            else if (textBox1.Text.Equals(""))
            {
                listBox1.Visibility = Visibility.Collapsed;
                listBox1.ItemsSource = null;
            }
            //ou deixara fechado
            else
            {
                listBox1.Visibility = Visibility.Collapsed;
                listBox1.ItemsSource = null;
            }
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //se lista não estiver vazia
            if (listBox1.ItemsSource != null)
            {
                //fecha lista e adcina valor ao texto
                listBox1.Visibility = Visibility.Collapsed;
                if (listBox1.SelectedIndex != -1)
                {
                    textBox1.Text = listBox1.SelectedItem.ToString();
                }
            }
            listBox1.Visibility = System.Windows.Visibility.Collapsed;

            //se campos para preenchimento de dados dos paises estiver vazio
            if (item == null)
            {
                //procura na lista o nome selecionado 
                item = pais.Find(test => test.name == listBox1.SelectedItem.ToString());
                //mostra dados
                textBlock1.Text = item.name;
                textBlock2.Text = item.capital;
                textBlock3.Text = item.population;
                textBlock4.Text = item.region;
                //carrega imagem da bandeira
                var model = new Windows1ViewModel();
                DataContext = model;
                model.SetImageData(@"http://criativit.com.br/imagemagick.php?link=" + item.flag);
            }
            else {
                item = null;
                if (listBox1.SelectedIndex != -1)
                {
                    //procura na lista o nome selecionado 
                    item = pais.Find(test => test.name == listBox1.SelectedItem.ToString());
                    //mostra dados
                    textBlock1.Text = item.name;
                    textBlock2.Text = item.capital;
                    textBlock3.Text = item.population;
                    textBlock4.Text = item.region;
                    //carrega imagem da bandeira
                    var model = new Windows1ViewModel();
                    DataContext = model;
                    model.SetImageData(@"http://criativit.com.br/imagemagick.php?link=" + item.flag);
                }
            }
        }



    }
}

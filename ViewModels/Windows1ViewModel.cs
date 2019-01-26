using System;
using System.Windows.Input;
using ListandoPaises.Helper;
using ListandoPaises.Model;
using System.Windows.Media.Imaging;
using System.Net;
using System.IO;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Net;
using System.IO;
using ListandoPaises.ViewModels;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ListandoPaises.ViewModels
{
    public class Windows1ViewModel : BaseViewModel
    {
        //Setar objetos
        private string _textBox;
        ImageSource imageSource;
       
        public String TextBox
        {
            get { return _textBox; }
            set
            {
                _textBox = value;
                OnPropertyChanged(() => TextBox);
            }

        }

        
        public void SetImageData(string data)
        {
            //objeto imagem em branco
            BitmapImage bitmap2 = new BitmapImage();
            //inicia construçao da imagem pela buffer do URL
            bitmap2.BeginInit();
            bitmap2.UriSource = new Uri(Path.GetFullPath("Images/semimagem.png"));
            bitmap2.EndInit();
            //seta source da imagem
            ImageSource = bitmap2;

            if (data != "")
            {
                var image = new Image();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(data, UriKind.Absolute);
                bitmap.EndInit();
                ImageSource = bitmap;
            }
           
        }

        //objeto imagem source
        public ImageSource ImageSource
        {
            get { return imageSource; }
            set
            {
                imageSource = value;
                OnPropertyChanged(() => ImageSource);
            }
        }




    }
       



}
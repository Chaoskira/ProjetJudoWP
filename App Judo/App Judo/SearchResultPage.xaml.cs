﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour en savoir plus sur le modèle d’élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkID=390556

namespace App_Judo
{



    public class SousItem
    {
        public string UniqueId { get; set; }
        public string Title { get; set; }
        public string Traduction { get; set; }
        public string Subtitle { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
    }

    public class Item
    {
        public string UniqueId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public List<SousItem> SousItems { get; set; }
    }

    public class Group
    {
        public string UniqueId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string ImagePath { get; set; }
        public string Desc { get; set; }
        public string Description { get; set; }
        public List<Item> Items { get; set; }
    }

    public class RootObject
    {
        public List<Group> Groups { get; set; }
    }
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    /// 



    public sealed partial class SearchResultPage : Page
    {

  


        public SearchResultPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoqué lorsque cette page est sur le point d'être affichée dans un frame.
        /// </summary>
        /// <param name="e">Données d'événement décrivant la manière dont l'utilisateur a accédé à cette page.
        /// Ce paramètre est généralement utilisé pour configurer la page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {


                String data = e.Parameter as String;
            

                // Change property of destination page
                search.Text = data;

                search.Text = "\"" + data + "\"";
            

        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}

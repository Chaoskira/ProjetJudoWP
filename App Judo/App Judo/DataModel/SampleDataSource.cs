﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

// Le modèle de données défini par ce fichier sert d'exemple représentatif d'un modèle fortement typé
// modèle.  Les noms de propriétés choisis correspondent aux liaisons de données dans les modèles d'élément standard.
//
// Les applications peuvent utiliser ce modèle comme point de départ et le modifier à leur convenance, ou le supprimer complètement et
// le remplacer par un autre correspondant à leurs besoins. L'utilisation de ce modèle peut vous permettre d'améliorer votre application 
// réactivité en lançant la tâche de chargement des données dans le code associé à App.xaml lorsque l'application 
// est démarrée pour la première fois.

namespace App_Judo.Data
{
    /// <summary>
    /// Modèle de données d'élément générique.
    /// </summary>
    public class SampleDataItems
    {
        public SampleDataItems(String uniqueId, String title, String subtitle, String imagePath, String description, String content)
        {
            this.UniqueId = uniqueId;
            this.Title = title;
            this.Subtitle = subtitle;
            this.Description = description;
            this.ImagePath = imagePath;
            this.Content = content;
            this.Item = new ObservableCollection<SampleDataTech>();
        }

        public string UniqueId { get; private set; }
        public string Title { get; private set; }
        public string Subtitle { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public string Content { get; private set; }
        public ObservableCollection<SampleDataTech> Item { get; set; }

        public override string ToString()
        {
            return this.Title;
        }
    }

    public class SampleDataTech
    {
        public SampleDataTech(String uniqueId, String title, String traduction, String subtitle, String imagePath, String description)
        {
            this.UniqueId = uniqueId;
            this.Title = title;
            this.Traduction = traduction;
            this.Subtitle = subtitle;
            this.Description = description;
            this.ImagePath = imagePath;
        }

        public string UniqueId { get; private set; }
        public string Title { get; private set; }
        public string Traduction { get; private set; }
        public string Subtitle { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public string Content { get; private set; }

        public override string ToString()
        {
            return this.Title;
        }
    }

    /// <summary>
    /// Modèle de données de groupe générique.
    /// </summary>
    public class SampleDataGroup
    {
        public SampleDataGroup(String uniqueId, String title, String subtitle, String imagePath, String description)
        {
            this.UniqueId = uniqueId;
            this.Title = title;
            this.Subtitle = subtitle;
            this.Description = description;
            this.ImagePath = imagePath;
            this.Items = new ObservableCollection<SampleDataItems>();
        }

        public string UniqueId { get; private set; }
        public string Title { get; private set; }
        public string Subtitle { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public ObservableCollection<SampleDataItems> Items { get; set; }

        public override string ToString()
        {
            return this.Title;
        }
    }

    /// <summary>
    /// Crée une collection de groupes et d'éléments dont le contenu est lu à partir d'un fichier json statique.
    /// 
    /// SampleDataSource initialise avec les données lues à partir d'un fichier json statique dans 
    /// projet.  Elle fournit des exemples de données à la fois au moment de la conception et de l'exécution.
    /// </summary>
    public sealed class SampleDataSource
    {
        private static SampleDataSource _sampleDataSource = new SampleDataSource();

        private ObservableCollection<SampleDataGroup> _groups = new ObservableCollection<SampleDataGroup>();
        public ObservableCollection<SampleDataGroup> Groups
        {
            get { return this._groups; }
        }

        public static async Task<IEnumerable<SampleDataGroup>> GetGroupsAsync()
        {
            await _sampleDataSource.GetSampleDataAsync();

            return _sampleDataSource.Groups;
        }

        public static async Task<SampleDataGroup> GetGroupAsync(string uniqueId)
        {
            await _sampleDataSource.GetSampleDataAsync();
            // Une simple recherche linéaire est acceptable pour les petits groupes de données
            var matches = _sampleDataSource.Groups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static async Task<SampleDataItems> GetItemAsync(string uniqueId)
        {
            await _sampleDataSource.GetSampleDataAsync();
            // Une simple recherche linéaire est acceptable pour les petits groupes de données
            var matches = _sampleDataSource.Groups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }


        public static async Task<SampleDataTech> GetTechAsync(string uniqueId)
        {
            await _sampleDataSource.GetSampleDataAsync();
            // Une simple recherche linéaire est acceptable pour les petits groupes de données
            var matches = _sampleDataSource.Groups.SelectMany(group => group.Items.SelectMany(item => item.Item)).Where((itemt) => itemt.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }


        private async Task GetSampleDataAsync()
        {
            if (this._groups.Count != 0)
                return;

            Uri dataUri = new Uri("ms-appx:///DataModel/SampleData.json");

            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
            string jsonText = await FileIO.ReadTextAsync(file);
            JsonObject jsonObject = JsonObject.Parse(jsonText);
            JsonArray jsonArray = jsonObject["Groups"].GetArray();


            foreach (JsonValue groupValue in jsonArray)
            {
                JsonObject groupObject = groupValue.GetObject();
                SampleDataGroup group = new SampleDataGroup(groupObject["UniqueId"].GetString(),
                                                            groupObject["Title"].GetString(),
                                                            groupObject["Subtitle"].GetString(),
                                                            groupObject["ImagePath"].GetString(),
                                                            groupObject["Description"].GetString());

                foreach (JsonValue itemsValue in groupObject["Items"].GetArray())
                {
                    
                    JsonObject itemsObject = itemsValue.GetObject();
                    group.Items.Add(new SampleDataItems(itemsObject["UniqueId"].GetString(),
                                                       itemsObject["Title"].GetString(),
                                                       itemsObject["Subtitle"].GetString(),
                                                       itemsObject["ImagePath"].GetString(),
                                                       itemsObject["Description"].GetString(),
                                                       itemsObject["Content"].GetString()));

                    foreach (JsonValue SousItemValue in itemsObject["SousItems"].GetArray())
                    {
                        JsonObject SouItemObject = SousItemValue.GetObject();
                        group.Items[group.Items.Count - 1].Item.Add(new SampleDataTech(SouItemObject["UniqueId"].GetString(),
                                                           SouItemObject["Title"].GetString(),
                                                           SouItemObject["Traduction"].GetString(),
                                                           SouItemObject["Subtitle"].GetString(),
                                                           SouItemObject["ImagePath"].GetString(),
                                                           SouItemObject["Description"].GetString()));
                        //group.Items[group.Items.Count - 1].Item.Add(new SampleDataTech("a", "b", "c", "Assets/LightGray.png", "e"));
                    }
                }
                this.Groups.Add(group);
            }
        }
    }
}
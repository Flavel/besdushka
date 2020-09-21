using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Besdushka.CreateNewRoom
{
    public partial class SpecificationsView : ContentView
    {

        public ObservableCollection<string> CharacterAttributes = new ObservableCollection<string>();
        public ObservableCollection<string> CharacterCharacteristics = new ObservableCollection<string>();
        public SpecificationsView(ObservableCollection<string> Attributes, ObservableCollection<string> Characteristics)
        {
            InitializeComponent();
            CharacterAttributes = Attributes;
            CharacterCharacteristics = Characteristics;
            attributes.ItemsSource = CharacterAttributes;
            characteristics.ItemsSource = CharacterCharacteristics;
        }

        void AddAttribute_Clicked(System.Object sender, System.EventArgs e)
        {
            if (CharacterAttributes.Contains(""))
                return;
            CharacterAttributes.Add("");
            attributes.ScrollTo(CharacterAttributes.Count - 1);

        }
        void AddCharacteristic_Clicked(System.Object sender, System.EventArgs e)
        {
            if (CharacterCharacteristics.Contains(""))
                return;
            CharacterCharacteristics.Add("");
            characteristics.ScrollTo(CharacterCharacteristics.Count - 1);
        }

        void AttributesEntry_Completed(object sender, System.EventArgs e)
        {
            if(((Entry)sender).Text == "")
            {
                CharacterAttributes.Remove(AttributeFocused);
                return;
            }
            if (CharacterAttributes.Contains(((Entry)sender).Text) && ((Entry)sender).Text != AttributeFocused)
            {
                ((Page)this.Parent.Parent).DisplayAlert("Ой ой!", "Такой аттрибут уже используется", "Блин, точняк!");
                ((Entry)sender).Text = AttributeFocused;
                return;
            }
            CharacterAttributes[CharacterAttributes.IndexOf(AttributeFocused)] = ((Entry)sender).Text;
        }
        void CharacteristicsEntry_Completed(object sender, System.EventArgs e)
        {
            if (((Entry)sender).Text == "")
            {
                CharacterCharacteristics.Remove(CharacteristicFocused);
                return;
            }
            if (CharacterCharacteristics.Contains(((Entry)sender).Text) && ((Entry)sender).Text != CharacteristicFocused)
            {
                ((Page)this.Parent.Parent).DisplayAlert("Ой ой!", "Такая характеристика уже используется", "Блин, точняк!");
                ((Entry)sender).Text = CharacteristicFocused;
                return;
            }
            CharacterCharacteristics[CharacterCharacteristics.IndexOf(CharacteristicFocused)] = ((Entry)sender).Text;
        }
        string AttributeFocused { get; set; }
        string CharacteristicFocused { get; set; }
        void AttributesEntry_Focused(object sender, EventArgs e)
        {
            if (((Entry)sender).Text == null)
            {
                AttributeFocused = "";
                return;
            }
            AttributeFocused = ((Entry)sender).Text;
        }
        void CharacteristicsEntry_Focused(object sender, EventArgs e)
        {
            if (((Entry)sender).Text == null)
            {
                CharacteristicFocused = "";
                return;
            }
            CharacteristicFocused = ((Entry)sender).Text;
        }

    }
}

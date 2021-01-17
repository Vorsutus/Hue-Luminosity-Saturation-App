using Xamarin.Forms;
using HLSView.Model;

namespace HLSView.ViewModel
{
    public class HLSColorViewModel : ViewModelBase
    {
        Color color;
        string name;

        public string HueValue { get; set; }
        public double Hue
        {
            set
            {
                //zero decimals
                HueValue = $"{value * 360:F0}";
                OnPropertyChanged("HueValue");

                if (color.Hue != value)
                {
                    MyColor = Color.FromHsla(value, color.Saturation, color.Luminosity);
                }
            }
            get => color.Hue;
        }

        public string SaturationValue { get; set; }
        public double Saturation
        {
            set
            {
                SaturationValue = $"{value * 100:F0}";
                OnPropertyChanged("SaturationValue");
                if (color.Saturation != value)
                {
                    MyColor = Color.FromHsla(color.Hue, value, color.Luminosity);
                }
            }
            get => color.Saturation;
        }

        public string LuminosityValue { get; set; }
        public double Luminosity
        {
            set
            {
                LuminosityValue = $"{value * 100:F0}";
                OnPropertyChanged("LuminosityValue");
                if (color.Luminosity != value)
                {
                    MyColor = Color.FromHsla(color.Hue, color.Saturation, value);
                }
            }
            get => color.Luminosity;
        }

        public Color MyColor
        {
            set
            {
                if (color != value)
                {
                    color = value;
                    OnPropertyChanged("Hue");
                    OnPropertyChanged("Saturation");
                    OnPropertyChanged("Luminosity");
                    OnPropertyChanged("MyColor");

                    Name = NamedColor.GetNearestColorName(color);
                }
            }
            get => color;
        }

        public string Name
        {
            private set{
                name = value;
                OnPropertyChanged();
            }
            get => name;
        }
    }
}

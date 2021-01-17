using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
//special class that gives you methods to rip through classes.
//at run time this will run s if the colors change it won't matter
using System.Reflection; 

namespace HLSView.Model
{
    public class NamedColor : IComparable<NamedColor>
    {
        public string Name { private set; get; }
        public string FriendlyName { private set; get; }
        public Color Color { private set; get; }
        public string RgbDisplay { private set; get; }
        private NamedColor() //default constructer
        {

        }

        public int CompareTo(NamedColor other)
        {
            return Name.CompareTo(other.Name);
        }

        static NamedColor()
        {
            List<NamedColor> all = new List<NamedColor>();
            StringBuilder stringBuilder = new StringBuilder(); //helper class from Xamarin

            //will get all the fields defined in this class "Color"
            foreach (FieldInfo fieldInfo in typeof(Color).GetRuntimeFields())
            {
                if(fieldInfo.IsPublic && fieldInfo.IsStatic) //all color attributes are static
                {
                    string name = fieldInfo.Name; //contains the name of the color
                    stringBuilder.Clear();
                    int index = 0;

                    //skip the first letter and find the second cap letter and put a space inbetween--- "DarkGrey" => "Dark Grey" 
                    foreach (char ch in name)
                    {
                        if (index != 0 && char.IsUpper(ch))
                        {
                            stringBuilder.Append(' ');
                        }
                        stringBuilder.Append(ch);
                        index++;
                    }

                    Color color = (Color)fieldInfo.GetValue(null); //have to cast this as "Color"

                    NamedColor namedColor = new NamedColor
                    {
                        Name = name,
                        FriendlyName = stringBuilder.ToString(),
                        Color = color,
                        RgbDisplay = string.Format("{0:X2}-{1:X2}-{2:X2}", 
                                    (int)(255 * color.R), 
                                    (int)(255 * color.G), 
                                    (int)(255 * color.B))
                    };

                    all.Add(namedColor);
                }               
            }
            all.TrimExcess(); //get rid of any extra spaces
            all.Sort(); //sort by name property
            All = all;
        }

        public static  IList<NamedColor> All { private get; set; }
        public static NamedColor Find(string name)
        {
            return ((List<NamedColor>)All).Find(nc=>nc.Name == name);
        }

        public static string GetNearestColorName(Color color)
        {
            double shortestDistance = 1000;
            NamedColor closestColor = null;

            foreach (NamedColor namedColor in All)
            {
                double distance = Math.Sqrt(Math.Pow((color.R - namedColor.Color.R), 2) +
                                            Math.Pow((color.G - namedColor.Color.G), 2) +
                                            Math.Pow((color.B - namedColor.Color.B), 2)
                                            );
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closestColor = namedColor;
                }
            }
            //return closestColor.Name;
            return closestColor.FriendlyName;   //shows a space between the displayed words
            //return closestColor.RgbDisplay;   //shows the color code
        }
    }
}
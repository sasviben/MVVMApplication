namespace WpfApplication.Models
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    #endregion

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks />
    [Serializable]
    public class NutritionModel
    {
        public NutritionModel()
        {
            Foodgroup = new List<NutritionFoodgroup>();
        }

        /// <remarks />
        [XmlElement("food-group")]
        public List<NutritionFoodgroup> Foodgroup { get; set; }
    }

    /// <remarks />
    [Serializable]
    public class NutritionFoodgroup
    {
        public NutritionFoodgroup()
        {
            Food = new List<NutritionFoodgroupFood>();
        }

        /// <remarks />
        [XmlElement("food")]
        public List<NutritionFoodgroupFood> Food { get; set; }

        /// <remarks />
        [XmlAttribute("name")]
        public string Name { get; set; }
    }

    [Serializable]
    public class NutritionFoodgroupFood
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("kalorije")]
        public int Kalorije { get; set; }

        [XmlAttribute("masti")]
        public int Masti { get; set; }

        [XmlAttribute("ugljikohidrati")]
        public int Ugljikohidrati { get; set; }

        [XmlAttribute("bjelancevine")]
        public int Bjelancevine { get; set; }
    }
}
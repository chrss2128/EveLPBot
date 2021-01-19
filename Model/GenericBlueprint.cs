namespace EveLPBot.Model
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class GenericBlueprint
    {
        [JsonProperty("activities")]
        public Activities Activities { get; set; }

        [JsonProperty("blueprintTypeID")]
        public long BlueprintTypeId { get; set; }

        [JsonProperty("maxProductionLimit")]
        public long MaxProductionLimit { get; set; }
    }

    public partial class Activities
    {
        [JsonProperty("copying", NullValueHandling = NullValueHandling.Ignore)]
        public Copying Copying { get; set; }

        [JsonProperty("manufacturing", NullValueHandling = NullValueHandling.Ignore)]
        public Copying Manufacturing { get; set; }

        [JsonProperty("research_material", NullValueHandling = NullValueHandling.Ignore)]
        public Copying ResearchMaterial { get; set; }

        [JsonProperty("research_time", NullValueHandling = NullValueHandling.Ignore)]
        public Copying ResearchTime { get; set; }

        [JsonProperty("invention", NullValueHandling = NullValueHandling.Ignore)]
        public Copying Invention { get; set; }

        [JsonProperty("reaction", NullValueHandling = NullValueHandling.Ignore)]
        public Copying Reaction { get; set; }
    }

    public partial class Copying
    {
        [JsonProperty("time")]
        public long Time { get; set; }

        [JsonProperty("materials", NullValueHandling = NullValueHandling.Ignore)]
        public List<Material> Materials { get; set; }

        [JsonProperty("skills", NullValueHandling = NullValueHandling.Ignore)]
        public List<Skill> Skills { get; set; }

        [JsonProperty("products", NullValueHandling = NullValueHandling.Ignore)]
        public List<Product> Products { get; set; }
    }

    public partial class Material
    {
        [JsonProperty("quantity")]
        public long Quantity { get; set; }

        [JsonProperty("typeID")]
        public long TypeId { get; set; }
    }

    public partial class Product
    {
        [JsonProperty("probability", NullValueHandling = NullValueHandling.Ignore)]
        public double? Probability { get; set; }

        [JsonProperty("quantity")]
        public long Quantity { get; set; }

        [JsonProperty("typeID")]
        public long TypeId { get; set; }
    }

    public partial class Skill
    {
        [JsonProperty("level")]
        public long Level { get; set; }

        [JsonProperty("typeID")]
        public long TypeId { get; set; }
    }
}

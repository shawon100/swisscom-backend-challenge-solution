namespace DemoAPI.Models
{
    public class TargetAsset
    {
        public int id { get; set; }
        public string name { get; set; }
        public int? parentId { get; set; }
        public string? status { get; set; }
        public bool? isStartable { get; set; }
        public int parentTargetAssetCount { get; set; }
    }
}

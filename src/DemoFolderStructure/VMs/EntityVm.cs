namespace DemoFolderStructure.VMs
{
    public class EntityVm
    {
        public string Name { get; set; }
        public string Index { get; set; }
        public EntityType Type { get; set; }
    }

    public enum EntityType
    {
        Unknown = 0,
        Folder = 1,
        File = 2
    }
}
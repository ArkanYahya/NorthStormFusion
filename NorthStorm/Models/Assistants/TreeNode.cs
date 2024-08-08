namespace NorthStorm.Models.Assistants
{
    public class TreeNode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TreeNode> Children { get; set; }
    }
}
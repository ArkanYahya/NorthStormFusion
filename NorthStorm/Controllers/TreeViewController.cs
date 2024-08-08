using Microsoft.AspNetCore.Mvc;
using NorthStorm.Models.Assistants;

namespace NorthStorm.Controllers
{
    public class TreeViewController : Controller
    {
        public IActionResult Index()
        {
            var treeData = GetTreeData();
            return View(treeData);
        }

        private List<TreeNode> GetTreeData()
        {
            // Example tree data
            return new List<TreeNode>
        {
            new TreeNode
            {
                Id = 1,
                Name = "Root",
                Children = new List<TreeNode>
                {
                    new TreeNode
                    {
                        Id = 2,
                        Name = "Child 1",
                        Children = new List<TreeNode>
                        {
                            new TreeNode { Id = 3, Name = "Grandchild 1", Children = new List<TreeNode>() },
                            new TreeNode { Id = 4, Name = "Grandchild 2", Children = new List<TreeNode>() }
                        }
                    },
                    new TreeNode
                    {
                        Id = 5,
                        Name = "Child 2",
                        Children = new List<TreeNode>()
                    }
                }
            }
        };
        }
    }

}

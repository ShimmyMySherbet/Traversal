using System.Collections.Generic;

namespace Traversal.Models.Databasing
{
    public class InventoryPage
    {
        public byte Index;
        public byte Width;
        public byte Height;
        public List<InventoryItem> Items = new List<InventoryItem>();
    }
}
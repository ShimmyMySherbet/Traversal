namespace Traversal.Models.Databasing
{
    public class GlobalClothingData : GlobalDataModel
    {
        public bool isVisible;
        public bool isSkinned;
        public bool isMythic;

        public ushort ShirtID;
        public byte ShirtQuality;

        public ushort PantsID;
        public byte PantsQuality;

        public ushort HatID;
        public byte HatQuality;

        public ushort BackpackID;
        public byte BackpackQuality;

        public ushort VestID;
        public byte VestQuality;

        public ushort MaskID;
        public byte MaskQuality;

        public ushort GlassesID;
        public byte GlassesQuality;

        public byte[] ShirtState;
        public byte[] PantsState;
        public byte[] HatState;
        public byte[] BackpackState;
        public byte[] VestState;
        public byte[] MaskState;
        public byte[] GlassesState;
    }
}
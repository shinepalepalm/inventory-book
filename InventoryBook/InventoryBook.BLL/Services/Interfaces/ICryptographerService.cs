namespace InventoryBook.BLL.Services.Interfaces
{
    public interface ICryptographerService
    {
        public byte[] Encrypt(string input, out byte[] salt);

        public byte[] Encrypt(string input, byte[] salt);
    }
}

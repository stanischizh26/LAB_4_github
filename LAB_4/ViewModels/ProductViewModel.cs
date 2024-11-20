using LAB_4.Models;
using System.Collections.Generic;
using System.Drawing;

namespace LAB_4.ViewModels
{
    public class ProductViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public Image ByteArrayToImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
                throw new ArgumentException("Данные изображения пусты или равны null.");

            using (MemoryStream memoryStream = new MemoryStream(imageData))
            {
                // Создаём объект Image из потока памяти
                return Image.FromStream(memoryStream);
            }
        }
    }
}

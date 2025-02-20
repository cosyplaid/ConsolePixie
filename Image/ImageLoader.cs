using ConsolePixie.Setup;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePixie.Image
{
    public class ImageLoader
    {
        private Bitmap? bitmap = null;

        public Bitmap Image
        {
            get { return bitmap; }
            private set { bitmap = value; }
        }
        public void LoadImage(string path)
        {
            try
            {
                Image = new Bitmap(path);
                Console.WriteLine("Изображение {0} успешно загружено!", path);
                Program.CurrentState = States.ImageLoaded;
                //CheckImage();
            }
            catch (System.ArgumentException ex)
            {
                CommonText.ShowErrorMessage("Ошибка: Неверный формат изображения. " + ex.Message);
                Program.CurrentState = States.WaitForImage;
            }
        }

        private void CheckImage()
        {
            if (Image.Height > 64 || Image.Width > 64)
            {
                CommonText.ShowErrorMessage("Разрешение изображения больше, чем 64x64. Отмена!");
                Image.Dispose();
                return;
            }
        }

        internal void DisposeBitmap()
        {
            if (Image != null)
                Image.Dispose();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePixie.Image
{
    public class ImageProcessor
    {
        private ImageLoader _imageLoader = new();
        private ImageBuilder _imageBuilder = new();
        public string? ImageName;

        public void LoadImage(string path)
        {
            ImageName = path;
            _imageLoader.LoadImage(path);
        }

        public void BuildImage(bool usingCymbols = true)
        {
            _imageBuilder.BuildImage(_imageLoader.Image, usingCymbols);
        }

        public void DisposeImage() => _imageLoader.DisposeBitmap();
    }
}

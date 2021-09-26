using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGalleryDemo1
{
    class ImageItem
    {
        public string Id { get; set; }  // To set and get the id of the image.
        public string Name { get; set; } // To set and get the name of image.
        public byte[] Base64 { get; set; } // To set and get the base64 URI of image.

        public string Format { get; set; } // To set and get the format of the image

    }
}

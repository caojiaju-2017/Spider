using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpiderC
{
    class PublicFunction
    {
        public static Image getImageByFile(String filename)
        {
            string filePath = Path.Combine(Application.StartupPath, "Res");

            string fileTempPath = Path.Combine(filePath, filename);

            try
            {
                Image bmp = Bitmap.FromFile(fileTempPath);

                return bmp;
            }
            catch 
            {
            }

            return null;          
        }

        internal static Image getImageByAbsoluteFile(string fileName)
        {
            try
            {
                Image image = Bitmap.FromFile(fileName);

                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(image);
                image.Dispose();


                return bmp;
            }
            catch
            {
            }

            return null; 
        }

    }
}

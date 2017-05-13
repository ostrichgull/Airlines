using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Airlines.Models;

namespace Airlines.Common
{
    public static class Utility
    {
        public static string GetCompanyTypeName(int? id)
        {
            switch (id)
            {
                case (int)CompanyTypeValue.Airline:
                    return "Airline";

                case (int)CompanyTypeValue.AircraftManufacturer:
                    return "Aircraft Manufacturer";
            }

            return string.Empty;
        }

        public static byte[] GetImage(HttpPostedFileBase image)
        {
            string fileName;
            byte[] bytes;
            int bytesToRead;
            int numBytesRead;

            fileName = Path.GetFileName(image.FileName);
            bytes = new byte[image.ContentLength];

            bytesToRead = (int)image.ContentLength;
            numBytesRead = 0;

            while (bytesToRead > 0)
            {
                int n = image.InputStream.Read(bytes, numBytesRead, bytesToRead);

                if (n == 0) break;

                numBytesRead += n;

                bytesToRead -= n;
            }
                
            return bytes;
        }
    }
}
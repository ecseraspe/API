// ---------------------------------------------------------------------------------------------------
// <copyright file="ImageHelper.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-12-10</date>
// <summary>
//     The ImageHelper class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Framework.Helper
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Net;
    using Youffer.Common.Helper;
    using Youffer.Resources.Constants;
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// The Image Helper Class
    /// </summary>
    public static class ImageHelper
    {
        /// <summary>
        /// Save Image From URL
        /// </summary>
        /// <param name="url"> The Image Url. </param>
        /// <param name="fileName">The File name. </param>
        /// <returns> image path </returns>
        public static string SaveImageFromUrl(string url, string fileName)
        {
            string imageUrl = string.Empty;
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                string defaultPath = AppSettings.Get<string>(ConfigConstants.DefaultImageSavePath);
                using (WebClient client = new WebClient())
                {
                    try
                    {
                        byte[] imageData = client.DownloadData(url);
                        MemoryStream inputStream = new MemoryStream(imageData);
                        fileName = fileName + ".jpg";
                        string imagePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/" + defaultPath), fileName);
                        Image img = Image.FromStream(inputStream);
                        img.Save(imagePath);
                        string defaultUrlPath = AppSettings.Get<string>(ConfigConstants.DefaultImageUrlPath);
                        imageUrl = AppSettings.Get<string>("ApiBaseUrl") + defaultUrlPath + "/" + fileName;
                        inputStream.Dispose();
                    }
                    catch (Exception ex)
                    {
                        var exception = ex.Message;
                    }
                }
            }

            return imageUrl;
        }

        /// <summary>
        /// Saves the message media files.
        /// </summary>
        /// <param name="msgMedia">The MSG media.</param>
        /// <returns>MessageMediaDto object.</returns>
        public static MessageMediaDto SaveMessageMediaFiles(MessageMediaDto msgMedia)
        {
            try
            {
                string defaultPath = AppSettings.Get<string>(ConfigConstants.DefaultMessageMediaSavePath);
                string defaultUrlPath = AppSettings.Get<string>(ConfigConstants.DefaultMessageMediaUrlPath);

                ////var fpath = @"F:\abc.txt";
                ////FileStream stream = File.OpenRead(fpath);
                ////byte[] fileBytes = new byte[stream.Length];

                ////stream.Read(fileBytes, 0, fileBytes.Length);
                ////stream.Close();

                ////Begins the process of writing the byte array back to a file
                byte[] fileBytes = msgMedia.FileBytes;
                var fname = Guid.NewGuid().ToString();
                string fileName = fname + msgMedia.Extension;
                string outputPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/" + defaultPath), fileName);
                using (Stream file = File.OpenWrite(outputPath))
                {
                    file.Write(fileBytes, 0, fileBytes.Length);
                }

                msgMedia.FileName = fname;
                msgMedia.Size = fileBytes.Length + string.Empty;

                byte[] dataArray = fileBytes;
                using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
                {
                    //// Write the data to the file, byte by byte. 
                    for (int i = 0; i < dataArray.Length; i++)
                    {
                        fileStream.WriteByte(dataArray[i]);
                    }

                    //// Set the stream position to the beginning of the file.
                    fileStream.Seek(0, SeekOrigin.Begin);

                    //// Read and verify the data. 
                    for (int i = 0; i < fileStream.Length; i++)
                    {
                        if (dataArray[i] != fileStream.ReadByte())
                        {
                            return new MessageMediaDto();
                        }
                    }
                }

                var url = AppSettings.Get<string>("ApiBaseUrl") + defaultUrlPath + "/" + fileName;
            }
            catch (Exception ex)
            {
                return new MessageMediaDto();
            }

            return msgMedia;
        }

        /// <summary>
        /// Saves the Profile Image.
        /// </summary>
        /// <param name="msgMedia">The MSG media.</param>
        /// <returns>MessageMediaDto object.</returns>
        public static string SaveProfileImage(MessageMediaDto msgMedia)
        {
            string imageUrl = string.Empty;
            try
            {
                string defaultPath = AppSettings.Get<string>(ConfigConstants.DefaultImageSavePath);
                string defaultUrlPath = AppSettings.Get<string>(ConfigConstants.DefaultImageUrlPath);

                byte[] fileBytes = msgMedia.FileBytes;
                var fname = msgMedia.FileName;
                string fileName = fname + msgMedia.Extension;
                string outputPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/" + defaultPath), fileName);
                using (Stream file = File.OpenWrite(outputPath))
                {
                    file.Write(fileBytes, 0, fileBytes.Length);
                } 

                byte[] dataArray = fileBytes;
                using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
                {
                    //// Write the data to the file, byte by byte. 
                    for (int i = 0; i < dataArray.Length; i++)
                    {
                        fileStream.WriteByte(dataArray[i]);
                    }

                    //// Set the stream position to the beginning of the file.
                    fileStream.Seek(0, SeekOrigin.Begin);

                    //// Read and verify the data. 
                    for (int i = 0; i < fileStream.Length; i++)
                    {
                        if (dataArray[i] != fileStream.ReadByte())
                        {
                            return imageUrl;
                        }
                    }
                }

                imageUrl = AppSettings.Get<string>("ApiBaseUrl") + defaultUrlPath + "/" + fileName;
            }
            catch (Exception ex)
            {
                return imageUrl;
            }

            return imageUrl;
        }
    }
}

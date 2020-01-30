using System.Collections.Generic;
using System.IO;
using System;

namespace Globalization.Tools
{
    /// <summary>
    /// Universal module for saving and loading any text information
    /// </summary>
    public static class SaverLoaderModule
    {
        #region public functions

        /// <summary>
        /// Saves data to a file with the specified name. Remember that in this function you must specify the full path to the file.
        /// </summary>
        /// <param name="FullPath">Full path to file</param>
        /// <param name="StringData">Your string data</param>
        public static void SaveMyDataTo(string FullPath, string StringData)
        {
            try
            {
                OverGeneratePath(FullPath);
                FileStream fileStream = new FileStream(FullPath, FileMode.Create);
                StreamWriter MyFile = new StreamWriter(fileStream);
                MyFile.Write(StringData);
                MyFile.Close();
            }
            catch (Exception e)
            {
                Debugger.SetExeption(e.ToString());
            }
        }

        /// <summary>
        /// Load string data from file. Remember that in this function you must specify the full path to the file.
        /// </summary>
        /// <param name="FullPath">Full path to file</param>
        /// <returns>String data or "", if file don`t exist</returns>
        public static string LoadMyDataFrom(string FullPath)
        {
            try
            {
                if (File.Exists(FullPath))
                {
                    StreamReader MyFile = new StreamReader(FullPath);
                    string DString = MyFile.ReadToEnd();
                    MyFile.Close();
                    return DString;
                }
            }
            catch (Exception e)
            {
                Debugger.SetExeption(e.ToString());
            }
            return "";
        }

        #endregion public functions

        #region private functions

        private static string Normalizer(string fileName)
        {
            if (fileName.IndexOf('/') == 0)
            {
                return fileName;
            }
            else
            {
                return "/" + fileName;
            }
        }

        private static string OverGeneratePath(string fileName)
        {
            string[] tempArray = fileName.Split('/');
            List<string> finalListOfPathParts = new List<string>();

            #region delete extra "/"

            for (int i = 0; i < tempArray.Length; i++)
            {
                if (tempArray[i] != "")
                {
                    finalListOfPathParts.Add(tempArray[i]);
                }
            }

            #endregion delete extra "/"

            #region create returned string and overGenerating path

            string returnedPath = "";
            for (int i = 0; i < finalListOfPathParts.Count - 1; i++)
            {
                returnedPath += finalListOfPathParts[i] + "/";
                if (!Directory.Exists(returnedPath))
                {
                    try
                    {
                        Directory.CreateDirectory(returnedPath);
                    }
                    catch (Exception e)
                    {
                        Debugger.SetExeption(e.ToString());
                    }
                }
            }
            returnedPath += finalListOfPathParts[finalListOfPathParts.Count - 1];

            #endregion create returned string and overGenerating path

            return returnedPath;
        }

        #endregion private functions
    }
}
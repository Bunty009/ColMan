using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;
using System.Configuration;
namespace Utilities
{
    /*
     * Created By: Muthukumar Nadar
     * Created Date: 04-Feb-2013
     */

    /// <summary>
    /// This class used to get messages and write exception logs
    /// </summary>

    public static class Exceptions
    {
        /// <summary>
        /// Method to get messages from exceptionmessages.xml. 
        /// </summary>
        /// <param name="Key">Key</param>
        public static string GetException(string Key)
        {
            //try
            //{
            //    string message;
            //    XmlDocument doc = new XmlDocument();
            //    doc.Load(Common.GetPhysicalFolderPath_Web() + "ExceptionMessages.xml");
            //    XmlNodeList docNodeList = doc.SelectNodes("/Exception/" + Key);
            //    message = docNodeList.Item(0).InnerText;
            //    return message;
            //}
            //catch (Exception)
            //{
                return "";
               // throw ex;
            //}            
        }

        #region WRITEEXCEPTIONLOG

        /// <summary>
        /// Method to write application wide exception log. 
        /// </summary>
        /// <param name="ex">Object of Class Exception</param>
        public static void WriteExceptionLog(Exception ex)
        {
            System.Threading.ThreadAbortException exception = ex as System.Threading.ThreadAbortException;
            if (exception == null)
            {
                string ExceptionLogFolderPath = Environment.CurrentDirectory + @"ExceptionLog";
                try
                {
                    if (!Directory.Exists(ExceptionLogFolderPath))
                        Directory.CreateDirectory(ExceptionLogFolderPath);

                    if (Directory.Exists(ExceptionLogFolderPath))
                    {
                        //Create month wise exception log file.
                        string date = string.Format("{0:dd}", DateTime.Now);
                        string month = string.Format("{0:MMM}", DateTime.Now);
                        string year = string.Format("{0:yyyy}", DateTime.Now);

                        string folderName = month + year; //Feb2013
                        string monthFolder = ExceptionLogFolderPath + "\\" + folderName;
                        if (!Directory.Exists(monthFolder))
                            Directory.CreateDirectory(monthFolder);

                        string ExceptionLogFileName = monthFolder +
                            "\\ExceptionLog_" + date + month + ".txt"; //ExceptionLog_04Feb.txt

                        using (System.IO.StreamWriter strmWriter = new System.IO.StreamWriter(ExceptionLogFileName, true))
                        {
                            strmWriter.WriteLine("On " + DateTime.Now.ToString() +
                                ", following error occured in the application:");
                            strmWriter.WriteLine("Message: " + ex.Message);
                            //strmWriter.WriteLine("Inner Exception: " + ex.InnerException.Message);
                            //strmWriter.WriteLine("Inner Exception(2): " + ex.InnerException.InnerException.Message);
                            strmWriter.WriteLine("Source: " + ex.Source);
                            strmWriter.WriteLine("Stack Trace: " + ex.StackTrace);
                            strmWriter.WriteLine("HelpLink: " + ex.HelpLink);
                            strmWriter.WriteLine("-------------------------------------------------------------------------------");
                        }
                    }
                    else
                        throw new DirectoryNotFoundException("Exception log folder not found in the specified path");
                }
                catch
                {
                    
                }
            }
        }

        #endregion WRITEEXCEPTIONLOG
    }
}
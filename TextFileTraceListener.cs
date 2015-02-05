using System;
using System.IO;

using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.WebServices.Authentication
{
  public class TraceListener : ITraceListener
  {
    public void Trace(string traceType, string traceMessage)
    {
      CreateXMLTextFile(traceType, traceMessage);
    }
    
    public string SaveFile {
      get { return _savefile; }
      private set { _savefile = value; }
    }
    public string SaveLocation {
      get { return @"..\\TraceOutput\\"; }
    }

    private void CreateXMLTextFile(string fileName, string traceContent)
    {
      try
      {
        if (!Directory.Exists(@"..\\TraceOutput"))
        {
          Directory.CreateDirectory(@"..\\TraceOutput");
        }

        //        System.IO.File.WriteAllText(@"..\\TraceOutput\\" + fileName + DateTime.Now.Ticks + ".txt", traceContent);
        SaveFile = @"..\\TraceOutput\\" + "EWS_" + Environment.UserName + "_"+
                                     DateTime.Today.ToString("d-MMM-yyyy") + ".txt";
        System.IO.File.AppendAllText(SaveFile,
                                     string.Format("{0}:{2}:  {1}\r\n",DateTime.Now.ToString("HH:mm:ss.ffffzzz"), traceContent, fileName));
      }
      catch (IOException ex)
      {
        Console.WriteLine(ex.Message);
      }
    }
    
    private string _savefile;
  }
}

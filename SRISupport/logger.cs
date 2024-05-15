// Decompiled with JetBrains decompiler
// Type: SRISupport.logger
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SRISupport
{
  public class logger
  {
    public string operationStr = "";
    public string opdescStr;
    public logMsgList logMessages;
    public string LogFileFolder;
    public string LogFileName = (string) null;
    public bool successFlag = true;
    public string errorString;
    internal bool createLogFile = false;
    internal bool xmlFile = true;
    private DateTime startTime;
    private DateTime stopTime;

    private string getFolderPath(string logFolderBase)
    {
      string path = !string.IsNullOrEmpty(logFolderBase) ? logFolderBase + "\\Log_Files" : ".\\Log_Files";
      if (!Directory.Exists(path))
      {
        try
        {
          Directory.CreateDirectory(path);
        }
        catch
        {
        }
      }
      return path;
    }

    public logger(string logFolderBase)
    {
      this.startTime = DateTime.Now;
      this.logMessages = new logMsgList();
      this.LogFileFolder = this.getFolderPath(logFolderBase);
    }

    public logger(string logFolderBase, string opStr)
    {
      this.startTime = DateTime.Now;
      this.logMessages = new logMsgList();
      this.LogFileFolder = this.getFolderPath(logFolderBase);
      this.operationStr = opStr;
    }

    public void addErrorMsg(string msgStr, string errStr) => this.logMessages.Add(new logMessage(msgStr)
    {
      errorFlag = 1U,
      errorMsg = errStr
    });

    private string getLogFile()
    {
      int num = 0;
      string str1 = "Log File";
      string str2 = string.Format("-{0:MdyyyyHmm}", (object) DateTime.Now);
      string str3 = !this.xmlFile ? ".txt" : ".html";
      string str4 = string.Format("{0}{1}{2}{3}", (object) this.LogFileFolder, (object) "\\", (object) str1, (object) str2);
      string path;
      for (path = string.Format("{0}{1}", (object) str4, (object) str3); File.Exists(path); path = string.Format("{0}{1}{2}", (object) str4, (object) num.ToString(), (object) str3))
        ++num;
      return path;
    }

    internal void WritetxtLogInformation()
    {
      this.LogFileName = this.getLogFile();
      StreamWriter streamWriter = new StreamWriter(this.LogFileName);
      using (streamWriter)
      {
        streamWriter.WriteLine(string.Format("File(s) operation: {0}", (object) this.operationStr));
        streamWriter.WriteLine(string.Format("Operation Started: {0}", (object) this.startTime));
        if (this.successFlag)
        {
          foreach (logMessage logMessage in (List<logMessage>) this.logMessages)
            streamWriter.WriteLine(logMessage.msgStr);
        }
        else
          streamWriter.WriteLine(this.errorString);
        streamWriter.WriteLine(string.Format("Operation Finished: {0}", (object) this.stopTime));
      }
    }

    private void WritethtmlHdr(HtmlTextWriter writer)
    {
      writer.WriteFullBeginTag("html lang=\"en_US\"");
      writer.WriteLine();
      writer.WriteFullBeginTag("head");
      writer.WriteFullBeginTag("title");
      writer.Write(string.Format("Logfile for operation: {0}", (object) this.operationStr));
      writer.WriteEndTag("title");
      writer.WriteEndTag("head");
      writer.WriteFullBeginTag("body");
      writer.WriteLine();
      writer.WriteFullBeginTag("table");
      writer.WriteLine();
      writer.WriteFullBeginTag("h2");
      writer.WriteLine(string.Format("Operation {0}", (object) this.operationStr));
      writer.WriteEndTag("h2");
      writer.WriteBreak();
      writer.WriteFullBeginTag("h3");
      writer.WriteLine(this.opdescStr);
      writer.WriteLine();
      writer.WriteBreak();
      writer.WriteLine(string.Format("Operation Started: {0}", (object) this.startTime));
      writer.WriteEndTag("h3");
      writer.WriteLine();
    }

    private void WritethtmlFooter(HtmlTextWriter writer)
    {
      writer.WriteFullBeginTag("h3");
      writer.WriteLine(string.Format("Operation Finished: {0}", (object) this.stopTime));
      writer.WriteEndTag("h3");
      writer.WriteEndTag("table");
      writer.WriteLine();
      writer.WriteEndTag("body");
      writer.WriteLine();
      writer.WriteEndTag("html");
    }

    private void WritethtmlBody(HtmlTextWriter writer)
    {
      Style style1 = new Style();
      Style style2 = new Style();
      style1.ForeColor = Color.Black;
      style2.ForeColor = Color.Red;
      HtmlTextWriterTag tag = HtmlTextWriterTag.P;
      writer.EnterStyle(style1, tag);
      foreach (logMessage logMessage in (List<logMessage>) this.logMessages)
      {
        if (logMessage.errorFlag > 0U)
        {
          writer.EnterStyle(style2, tag);
          if (logMessage.errorMsg != null)
            writer.WriteLine(logMessage.errorMsg);
          writer.WriteLine(logMessage.msgStr);
          writer.ExitStyle(style1, tag);
        }
        else
        {
          writer.WriteBreak();
          writer.WriteLine(logMessage.msgStr);
        }
      }
      writer.ExitStyle(style1, tag);
    }

    private void WriteHTMLLogInformation()
    {
      this.LogFileName = this.getLogFile();
      StringBuilder sb = new StringBuilder();
      using (StringWriter writer1 = new StringWriter(sb))
      {
        using (HtmlTextWriter writer2 = new HtmlTextWriter((TextWriter) writer1))
        {
          this.WritethtmlHdr(writer2);
          if (this.successFlag)
          {
            this.WritethtmlBody(writer2);
          }
          else
          {
            writer2.WriteBreak();
            writer2.WriteLine(this.errorString);
            writer2.WriteBreak();
          }
          this.WritethtmlFooter(writer2);
        }
      }
      using (StreamWriter streamWriter = new StreamWriter(this.LogFileName, true, Encoding.UTF8))
        streamWriter.WriteLine(sb.ToString());
    }

    public void createlog()
    {
      this.stopTime = DateTime.Now;
      if (this.xmlFile)
        this.WriteHTMLLogInformation();
      else
        this.WritetxtLogInformation();
    }

    public void addMsg(string msgStr) => this.logMessages.Add(new logMessage(msgStr));

    public virtual void displayResults()
    {
    }
  }
}

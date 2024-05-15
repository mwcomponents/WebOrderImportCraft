// Decompiled with JetBrains decompiler
// Type: WebOrderImportCraft.Program
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using MsgBox;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using WebOrderImportCraft.Properties;

namespace WebOrderImportCraft
{
  internal static class Program
  {
    [STAThread]
    private static void Main(string[] arg)
    {
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      try
      {
        if (Settings.Default.Properties["ConfigSettingsKey"] != null)
        {
          string str = Settings.Default["ConfigSettingsKey"].ToString();
          if (str == "AUTO")
            str = "MW";
          Settings.Default.SettingsKey = str;
          Settings.Default.Reload();
        }
      }
      catch (Exception ex)
      {
        int num1 = (int) MBox.Show(ex.Message);
        if (ex.InnerException != null)
        {
          int num2 = (int) MBox.Show(ex.InnerException.Message + " (app.config)", nameof (Program));
        }
      }
      if (arg.Length >= 1 && arg[0].ToUpper() == "-B")
      {
        ProcessStartInfo startInfo = new ProcessStartInfo(Process.GetCurrentProcess().MainModule.FileName.Replace(".vshost", ""));
        if (arg.Length > 1)
          startInfo.Arguments = arg[1];
        startInfo.UseShellExecute = false;
        Process.Start(startInfo);
      }
      else
      {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        OrderSelectForm orderSelectForm = new OrderSelectForm();
        DBInfo dbInfo = new DBInfo();
        char[] chArray = new char[1]{ ',' };
        string[] strArray = new string[0];
        if (arg.Length != 0)
          strArray = !arg[0].StartsWith("'") ? arg[0].Split(chArray) : arg[0].Substring(1).Split(chArray);
        if (strArray.Length > 3)
        {
          orderSelectForm.header0.Key = strArray[0];
          orderSelectForm.header0.UserName = strArray[1];
          orderSelectForm.header0.Password = strArray[2];
          int num = (int) orderSelectForm.ShowDialog();
        }
        else
        {
          if (arg.Length != 0 && arg[0].StartsWith("-"))
          {
            string str1 = "";
            string str2 = "";
            string str3 = "";
            foreach (string str4 in arg)
            {
              if (str4.StartsWith("-G"))
                flag2 = true;
              else if (str4.StartsWith("-I"))
                flag1 = true;
              else if (str4.StartsWith("-D"))
                str1 = str4.Substring(2);
              else if (str4.StartsWith("-U"))
                str2 = str4.Substring(2);
              else if (str4.StartsWith("-P"))
                str3 = str4.Substring(2);
              else if (str4.StartsWith("-Q"))
                flag3 = true;
            }
            dbInfo.Database = str1;
            dbInfo.Username = str2;
            dbInfo.Password = str3;
          }
          if (dbInfo.Database != "" && dbInfo.Username != "" && dbInfo.Password != "" && !flag1)
          {
            orderSelectForm.header0.Key = dbInfo.Database;
            orderSelectForm.header0.UserName = dbInfo.Username;
            orderSelectForm.header0.Password = dbInfo.Password;
            orderSelectForm.Debug = flag2;
            int num = (int) orderSelectForm.ShowDialog();
          }
          else if (orderSelectForm.header1.Key != null && !flag1)
          {
            if (orderSelectForm.header0.Key == null)
              orderSelectForm.header0.Key = orderSelectForm.header1.Key;
            orderSelectForm.Debug = flag2;
            if (flag3)
            {
              orderSelectForm.AutoImportQuotes();
            }
            else
            {
              int num = (int) orderSelectForm.ShowDialog();
            }
          }
          else
          {
            DialogResult dialogResult;
            do
            {
              dialogResult = DialogResult.OK;
              if (dbInfo.ShowDialog() == DialogResult.OK)
              {
                if (dbInfo.Database != "" && dbInfo.Username != "" && dbInfo.Password != "")
                {
                  orderSelectForm.header0.Key = dbInfo.Database;
                  orderSelectForm.header0.UserName = dbInfo.Username;
                  orderSelectForm.header0.Password = dbInfo.Password;
                }
                orderSelectForm.Debug = flag2;
                dialogResult = orderSelectForm.ShowDialog();
              }
            }
            while (dialogResult == DialogResult.Retry);
          }
        }
      }
    }
  }
}

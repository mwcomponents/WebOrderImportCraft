// Decompiled with JetBrains decompiler
// Type: WebOrderImportCraft.Downloader
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using System;
using System.Drawing;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebOrderImportCraft.Properties;

namespace WebOrderImportCraft
{
  internal class Downloader
  {
    internal OrderSelectForm _orderSelectForm;

    public Downloader(OrderSelectForm orderSelectForm) => this._orderSelectForm = orderSelectForm;

    public bool Download(string filePath, string fileName)
    {
      bool flag = true;
      this._orderSelectForm.Cursor = Cursors.WaitCursor;
      using (WebClient webClient = new WebClient())
      {
        string apikey = Settings.Default.APIKEY;
        webClient.Headers.Add("Authorization", "Bearer " + apikey);
        using (Task task = webClient.DownloadFileTaskAsync(filePath, fileName))
        {
          this._orderSelectForm.labelFName.Text = fileName;
          Size size1;
          ref Size local1 = ref size1;
          int width = this._orderSelectForm.Size.Width / 2;
          Size size2 = this._orderSelectForm.Size;
          int height = size2.Height / 2;
          local1 = new Size(width, height);
          Point point;
          ref Point local2 = ref point;
          size2 = this._orderSelectForm.Size;
          int x = size2.Width / 2 - size1.Width / 2;
          size2 = this._orderSelectForm.Size;
          int y = size2.Height / 2 - size1.Height / 2;
          local2 = new Point(x, y);
          this._orderSelectForm.panelDownLoad.Location = point;
          this._orderSelectForm.panelDownLoad.Visible = true;
          this._orderSelectForm.panelDownLoad.Focus();
          do
          {
            Application.DoEvents();
          }
          while (!task.IsCompleted);
          if (task.Status == TaskStatus.Faulted)
          {
            int num = (int) MessageBox.Show("File Atttachment Download Failed." + Environment.NewLine + filePath + Environment.NewLine + task.Exception.ToString());
            flag = false;
          }
        }
      }
      this._orderSelectForm.Cursor = Cursors.Default;
      this._orderSelectForm.panelDownLoad.Visible = false;
      return flag;
    }
  }
}

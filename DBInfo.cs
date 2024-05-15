// Decompiled with JetBrains decompiler
// Type: WebOrderImportCraft.DBInfo
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using Synergy.BusinessObjects;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WebOrderImportCraft
{
  public class DBInfo : Form
  {
    private IContainer components = (IContainer) null;
    private TextBox textBoxServer;
    private TextBox textBoxDatabase;
    private TextBox textBoxUsername;
    private TextBox textBoxPassword;
    private Label label2;
    private Label label3;
    private Label label4;
    private Label label5;
    private GroupBox groupBox1;
    private RadioButton radioButtonSQL;
    private RadioButton radioButtonORA;
    private Button buttonOK;
    private Button buttonCancel;

    public DBInfo() => this.InitializeComponent();

    private void DBInfo_Load(object sender, EventArgs e)
    {
      if (IntPtr.Size == 8)
      {
        this.Text += " - 64bit";
      }
      else
      {
        if (IntPtr.Size != 4)
          return;
        this.Text += " - 32bit";
      }
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void radioButtonORA_CheckedChanged(object sender, EventArgs e)
    {
      if (this.radioButtonORA.Checked)
      {
        this.textBoxServer.Enabled = false;
        this.textBoxServer.Text = "";
      }
      else
        this.textBoxServer.Enabled = true;
    }

    public DatabaseEngineType DatabaseType
    {
      get => this.radioButtonORA.Checked ? DatabaseEngineType.Oracle : DatabaseEngineType.SqlServer;
      set
      {
        if (value == DatabaseEngineType.Oracle)
          this.radioButtonORA.Checked = true;
        else
          this.radioButtonSQL.Checked = true;
      }
    }

    public string Server
    {
      get => this.textBoxServer.Text;
      set => this.textBoxServer.Text = value;
    }

    public string Database
    {
      get => this.textBoxDatabase.Text;
      set => this.textBoxDatabase.Text = value;
    }

    public string Username
    {
      get => this.textBoxUsername.Text;
      set => this.textBoxUsername.Text = value;
    }

    public string Password
    {
      get => this.textBoxPassword.Text;
      set => this.textBoxPassword.Text = value;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DBInfo));
      this.textBoxServer = new TextBox();
      this.textBoxDatabase = new TextBox();
      this.textBoxUsername = new TextBox();
      this.textBoxPassword = new TextBox();
      this.label2 = new Label();
      this.label3 = new Label();
      this.label4 = new Label();
      this.label5 = new Label();
      this.groupBox1 = new GroupBox();
      this.radioButtonORA = new RadioButton();
      this.radioButtonSQL = new RadioButton();
      this.buttonOK = new Button();
      this.buttonCancel = new Button();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      this.textBoxServer.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.textBoxServer.Location = new Point(91, 88);
      this.textBoxServer.Name = "textBoxServer";
      this.textBoxServer.Size = new Size(182, 26);
      this.textBoxServer.TabIndex = 0;
      this.textBoxServer.Visible = false;
      this.textBoxDatabase.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.textBoxDatabase.Location = new Point(91, 119);
      this.textBoxDatabase.Name = "textBoxDatabase";
      this.textBoxDatabase.Size = new Size(182, 26);
      this.textBoxDatabase.TabIndex = 1;
      this.textBoxUsername.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.textBoxUsername.Location = new Point(91, 149);
      this.textBoxUsername.Name = "textBoxUsername";
      this.textBoxUsername.Size = new Size(182, 26);
      this.textBoxUsername.TabIndex = 2;
      this.textBoxPassword.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.textBoxPassword.Location = new Point(91, 180);
      this.textBoxPassword.Name = "textBoxPassword";
      this.textBoxPassword.Size = new Size(182, 26);
      this.textBoxPassword.TabIndex = 3;
      this.textBoxPassword.UseSystemPasswordChar = true;
      this.label2.AutoSize = true;
      this.label2.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label2.Location = new Point(34, 92);
      this.label2.Name = "label2";
      this.label2.Size = new Size(51, 18);
      this.label2.TabIndex = 5;
      this.label2.Text = "Server";
      this.label2.Visible = false;
      this.label3.AutoSize = true;
      this.label3.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label3.Location = new Point(14, 123);
      this.label3.Name = "label3";
      this.label3.Size = new Size(71, 18);
      this.label3.TabIndex = 6;
      this.label3.Text = "Database";
      this.label4.AutoSize = true;
      this.label4.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label4.Location = new Point(8, 153);
      this.label4.Name = "label4";
      this.label4.Size = new Size(77, 18);
      this.label4.TabIndex = 7;
      this.label4.Text = "Username";
      this.label5.AutoSize = true;
      this.label5.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label5.Location = new Point(10, 184);
      this.label5.Name = "label5";
      this.label5.Size = new Size(75, 18);
      this.label5.TabIndex = 8;
      this.label5.Text = "Password";
      this.groupBox1.Controls.Add((Control) this.radioButtonORA);
      this.groupBox1.Controls.Add((Control) this.radioButtonSQL);
      this.groupBox1.Location = new Point(9, 13);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(264, 64);
      this.groupBox1.TabIndex = 9;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Database Type";
      this.groupBox1.Visible = false;
      this.radioButtonORA.AutoSize = true;
      this.radioButtonORA.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.radioButtonORA.Location = new Point(37, 31);
      this.radioButtonORA.Name = "radioButtonORA";
      this.radioButtonORA.Size = new Size(70, 22);
      this.radioButtonORA.TabIndex = 1;
      this.radioButtonORA.TabStop = true;
      this.radioButtonORA.Text = "Oracle";
      this.radioButtonORA.UseVisualStyleBackColor = true;
      this.radioButtonORA.CheckedChanged += new EventHandler(this.radioButtonORA_CheckedChanged);
      this.radioButtonSQL.AutoSize = true;
      this.radioButtonSQL.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.radioButtonSQL.Location = new Point(137, 31);
      this.radioButtonSQL.Name = "radioButtonSQL";
      this.radioButtonSQL.Size = new Size(90, 22);
      this.radioButtonSQL.TabIndex = 0;
      this.radioButtonSQL.TabStop = true;
      this.radioButtonSQL.Text = "SqlServer";
      this.radioButtonSQL.UseVisualStyleBackColor = true;
      this.buttonOK.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.buttonOK.Location = new Point(49, 212);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new Size(75, 23);
      this.buttonOK.TabIndex = 10;
      this.buttonOK.Text = "OK";
      this.buttonOK.UseVisualStyleBackColor = true;
      this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
      this.buttonCancel.DialogResult = DialogResult.Cancel;
      this.buttonCancel.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.buttonCancel.Location = new Point(164, 212);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new Size(75, 23);
      this.buttonCancel.TabIndex = 11;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.UseVisualStyleBackColor = true;
      this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
      this.AcceptButton = (IButtonControl) this.buttonOK;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.buttonCancel;
      this.ClientSize = new Size(289, 247);
      this.Controls.Add((Control) this.buttonCancel);
      this.Controls.Add((Control) this.buttonOK);
      this.Controls.Add((Control) this.groupBox1);
      this.Controls.Add((Control) this.label5);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.textBoxPassword);
      this.Controls.Add((Control) this.textBoxUsername);
      this.Controls.Add((Control) this.textBoxDatabase);
      this.Controls.Add((Control) this.textBoxServer);
      this.FormBorderStyle = FormBorderStyle.Fixed3D;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (DBInfo);
      this.Text = "DB Login";
      this.Load += new EventHandler(this.DBInfo_Load);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}

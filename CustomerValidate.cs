// Decompiled with JetBrains decompiler
// Type: WebOrderImportCraft.CustomerValidate
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WebOrderImportCraft
{
  public class CustomerValidate : Form
  {
    private CraftCMS.Customer _webCust = (CraftCMS.Customer) null;
    private Synergy.DistributedERP.Customer _customer = (Synergy.DistributedERP.Customer) null;
    private IContainer components = (IContainer) null;
    private Button buttonOK;
    private Button buttonCancel;
    private Label label1;
    private Label label2;
    private Label label3;
    private TextBox textBoxVisualCustomer;
    private TextBox textBoxWebCustomer;
    private SplitContainer splitContainer1;
    private Label label4;
    private Label label5;
    private Label label6;

    public CustomerValidate() => this.InitializeComponent();

    public CustomerValidate(Synergy.DistributedERP.Customer visualCust, CraftCMS.Customer webCustomer)
    {
      this.InitializeComponent();
      this.customer = visualCust;
      this.webCust = webCustomer;
    }

    private void CustomerValidate_Load(object sender, EventArgs e)
    {
      if (this._customer != null)
      {
        this.textBoxVisualCustomer.Text = this._customer.CustomerID;
        TextBox boxVisualCustomer1 = this.textBoxVisualCustomer;
        boxVisualCustomer1.Text = boxVisualCustomer1.Text + "\r\n" + this._customer.CustomerName;
        TextBox boxVisualCustomer2 = this.textBoxVisualCustomer;
        boxVisualCustomer2.Text = boxVisualCustomer2.Text + "\r\n" + this._customer.ContactFirstName + " " + (string.IsNullOrEmpty(this._customer.ContactMiddleInitial) ? "" : this._customer.ContactMiddleInitial + ". ") + this._customer.ContactLastName;
        TextBox boxVisualCustomer3 = this.textBoxVisualCustomer;
        boxVisualCustomer3.Text = boxVisualCustomer3.Text + "\r\n" + this._customer.ContactEmail;
        TextBox boxVisualCustomer4 = this.textBoxVisualCustomer;
        boxVisualCustomer4.Text = boxVisualCustomer4.Text + "\r\n\r\n" + this._customer.Address1;
        TextBox boxVisualCustomer5 = this.textBoxVisualCustomer;
        boxVisualCustomer5.Text = boxVisualCustomer5.Text + "\r\n" + this._customer.Address3;
        TextBox boxVisualCustomer6 = this.textBoxVisualCustomer;
        boxVisualCustomer6.Text = boxVisualCustomer6.Text + "\r\n" + this._customer.Address2;
        TextBox boxVisualCustomer7 = this.textBoxVisualCustomer;
        boxVisualCustomer7.Text = boxVisualCustomer7.Text + "\r\n" + this._customer.City;
        TextBox boxVisualCustomer8 = this.textBoxVisualCustomer;
        boxVisualCustomer8.Text = boxVisualCustomer8.Text + "\r\n" + this._customer.State;
        TextBox boxVisualCustomer9 = this.textBoxVisualCustomer;
        boxVisualCustomer9.Text = boxVisualCustomer9.Text + "\r\n" + this._customer.ZipCode;
        TextBox boxVisualCustomer10 = this.textBoxVisualCustomer;
        boxVisualCustomer10.Text = boxVisualCustomer10.Text + "\r\n" + this._customer.Country;
      }
      if (this._webCust == null)
        return;
      this.textBoxWebCustomer.Text = "ID :" + this._webCust.id.ToString();
      TextBox textBoxWebCustomer1 = this.textBoxWebCustomer;
      textBoxWebCustomer1.Text = textBoxWebCustomer1.Text + "\r\n\r\nCompany: " + this._webCust.user?.company;
      TextBox textBoxWebCustomer2 = this.textBoxWebCustomer;
      textBoxWebCustomer2.Text = textBoxWebCustomer2.Text + "\r\nTerms Customer: " + this._webCust.user?.termsCustomer.ToString();
      TextBox textBoxWebCustomer3 = this.textBoxWebCustomer;
      textBoxWebCustomer3.Text = textBoxWebCustomer3.Text + "\r\nName: " + this._webCust.user?.fullName;
      TextBox textBoxWebCustomer4 = this.textBoxWebCustomer;
      textBoxWebCustomer4.Text = textBoxWebCustomer4.Text + "\r\neMail: " + this._webCust.user?.email;
      TextBox textBoxWebCustomer5 = this.textBoxWebCustomer;
      textBoxWebCustomer5.Text = textBoxWebCustomer5.Text + "\r\nPhone: " + this._webCust.user?.phone;
      if (this._webCust.user?.pricingTier != null)
      {
        foreach (JToken jtoken in (JArray) this.webCust.user.pricingTier)
        {
          string str = (string) jtoken;
          TextBox textBoxWebCustomer6 = this.textBoxWebCustomer;
          textBoxWebCustomer6.Text = textBoxWebCustomer6.Text + "\r\nPricing Tier: " + str;
        }
      }
      TextBox textBoxWebCustomer7 = this.textBoxWebCustomer;
      textBoxWebCustomer7.Text = textBoxWebCustomer7.Text + "\r\n\r\nVisualCustID: " + this._webCust.user?.visualId;
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    public CraftCMS.Customer webCust
    {
      get => this._webCust;
      set => this._webCust = value;
    }

    public Synergy.DistributedERP.Customer customer
    {
      get => this._customer;
      set => this._customer = value;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CustomerValidate));
      this.buttonOK = new Button();
      this.buttonCancel = new Button();
      this.label1 = new Label();
      this.label2 = new Label();
      this.label3 = new Label();
      this.textBoxVisualCustomer = new TextBox();
      this.textBoxWebCustomer = new TextBox();
      this.splitContainer1 = new SplitContainer();
      this.label4 = new Label();
      this.label5 = new Label();
      this.label6 = new Label();
      this.splitContainer1.BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      this.buttonOK.Anchor = AnchorStyles.Bottom;
      this.buttonOK.Location = new Point(197, 402);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new Size(114, 23);
      this.buttonOK.TabIndex = 0;
      this.buttonOK.Text = "Confirm Match";
      this.buttonOK.UseVisualStyleBackColor = true;
      this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
      this.buttonCancel.Anchor = AnchorStyles.Bottom;
      this.buttonCancel.Location = new Point(349, 402);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new Size(114, 23);
      this.buttonCancel.TabIndex = 1;
      this.buttonCancel.Text = "Cancel Operation";
      this.buttonCancel.UseVisualStyleBackColor = true;
      this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
      this.label1.AutoSize = true;
      this.label1.Location = new Point(12, 48);
      this.label1.Name = "label1";
      this.label1.Size = new Size(65, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Customer ID";
      this.label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.label2.AutoSize = true;
      this.label2.Location = new Point(115, 5);
      this.label2.Name = "label2";
      this.label2.Size = new Size(35, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "Visual";
      this.label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.label3.AutoSize = true;
      this.label3.Location = new Point(125, 5);
      this.label3.Name = "label3";
      this.label3.Size = new Size(30, 13);
      this.label3.TabIndex = 4;
      this.label3.Text = "Web";
      this.textBoxVisualCustomer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.textBoxVisualCustomer.Location = new Point(3, 20);
      this.textBoxVisualCustomer.Multiline = true;
      this.textBoxVisualCustomer.Name = "textBoxVisualCustomer";
      this.textBoxVisualCustomer.Size = new Size(246, 334);
      this.textBoxVisualCustomer.TabIndex = 5;
      this.textBoxWebCustomer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.textBoxWebCustomer.Location = new Point(4, 20);
      this.textBoxWebCustomer.Multiline = true;
      this.textBoxWebCustomer.Name = "textBoxWebCustomer";
      this.textBoxWebCustomer.Size = new Size(258, 334);
      this.textBoxWebCustomer.TabIndex = 6;
      this.splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.splitContainer1.Location = new Point(108, 25);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Panel1.Controls.Add((Control) this.textBoxVisualCustomer);
      this.splitContainer1.Panel1.Controls.Add((Control) this.label2);
      this.splitContainer1.Panel2.Controls.Add((Control) this.textBoxWebCustomer);
      this.splitContainer1.Panel2.Controls.Add((Control) this.label3);
      this.splitContainer1.Size = new Size(523, 357);
      this.splitContainer1.SplitterDistance = 253;
      this.splitContainer1.TabIndex = 7;
      this.label4.AutoSize = true;
      this.label4.Location = new Point(12, 61);
      this.label4.Name = "label4";
      this.label4.Size = new Size(82, 13);
      this.label4.TabIndex = 8;
      this.label4.Text = "Customer Name";
      this.label5.AutoSize = true;
      this.label5.Location = new Point(12, 89);
      this.label5.Name = "label5";
      this.label5.Size = new Size(73, 13);
      this.label5.TabIndex = 9;
      this.label5.Text = "Email Address";
      this.label6.AutoSize = true;
      this.label6.Location = new Point(12, 74);
      this.label6.Name = "label6";
      this.label6.Size = new Size(75, 13);
      this.label6.TabIndex = 10;
      this.label6.Text = "Contact Name";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(660, 448);
      this.Controls.Add((Control) this.label6);
      this.Controls.Add((Control) this.label5);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.splitContainer1);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.buttonCancel);
      this.Controls.Add((Control) this.buttonOK);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (CustomerValidate);
      this.Text = "Customer Match Validation";
      this.Load += new EventHandler(this.CustomerValidate_Load);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.PerformLayout();
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.Panel2.PerformLayout();
      this.splitContainer1.EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: WebOrderImportCraft.CustomerSelector
// Assembly: WebOrderImportCraft, Version=1000.510.8894.24695, Culture=neutral, PublicKeyToken=bf11c4f15ab4e1ef
// MVID: F965BC17-D6AC-43D0-B104-3E53F0C380F8
// Assembly location: P:\Synergy\VMAWS Custom Apps\WebOrderImportCraft\WebOrderImportCraft.exe

using Synergy.DistributedERP;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WebOrderImportCraft
{
  public class CustomerSelector : Form
  {
    private string CustID = (string) null;
    private string searchID = string.Empty;
    private string SiteID = (string) null;
    private DataTable dt = new DataTable();
    private BindingSource bs = new BindingSource();
    private IContainer components = (IContainer) null;
    private DataGridView dataGridView1;
    private Button buttonOK;
    private Button buttonCancel;
    private DataGridViewTextBoxColumn FilterKey;
    private DataGridViewTextBoxColumn CustomerID;
    private DataGridViewTextBoxColumn CustName;
    private DataGridViewTextBoxColumn Address1;
    private DataGridViewTextBoxColumn Address2;
    private DataGridViewTextBoxColumn Address3;
    private DataGridViewTextBoxColumn City;
    private DataGridViewTextBoxColumn State;
    private DataGridViewTextBoxColumn Country;
    private DataGridViewTextBoxColumn Zipcode;
    private Label labelCount;

    public CustomerSelector() => this.InitializeComponent();

    public DialogResult ShowDialog(IWin32Window owner, Header header)
    {
      this.Text = this.Text + " / " + header.Key;
      foreach (DataGridViewColumn column in (BaseCollection) this.dataGridView1.Columns)
        this.dt.Columns.Add(column.Name, typeof (string));
      this.dt.Rows.Add((object) "KEY", (object) string.Empty, (object) string.Empty, (object) string.Empty, (object) string.Empty, (object) string.Empty, (object) string.Empty, (object) string.Empty, (object) string.Empty, (object) string.Empty);
      CustomerService customerService = new CustomerService();
      customerService._Header = header;
      foreach (CustomerListItem customer in customerService.GetCustomerList(this.SiteID, "N", 0, 0, (string) null, (string) null, (string) null, (string) null, (string) null, "Y", (string) null, (string) null).CustomerList)
        this.dt.Rows.Add((object) string.Empty, (object) customer.ID, (object) customer.Customer.CustomerName, (object) customer.Customer.Address1, (object) customer.Customer.Address2, (object) customer.Customer.Address3, (object) customer.Customer.City, (object) customer.Customer.State, (object) customer.Customer.Country, (object) customer.Customer.ZipCode);
      return this.ShowDialog(owner);
    }

    internal void FreezeGrid()
    {
      foreach (DataGridViewRow row in (IEnumerable) this.dataGridView1.Rows)
      {
        if (row.Index == 0)
          row.Frozen = true;
        else
          row.ReadOnly = true;
      }
      this.labelCount.Text = "Records: " + (this.dataGridView1.Rows.Count - 1).ToString();
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
      if (this.dataGridView1.CurrentRow.Index > 0 && this.dataGridView1.CurrentCell.Selected)
      {
        this.CustID = this.dataGridView1.CurrentRow.Cells["CustomerID"].Value.ToString();
        this.DialogResult = DialogResult.OK;
      }
      else
        this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void CustomerSelector_Shown(object sender, EventArgs e)
    {
      using (new HourGlass())
      {
        Application.DoEvents();
        this.dataGridView1.Columns.Clear();
        this.bs.DataSource = (object) this.dt;
        this.dataGridView1.DataSource = (object) this.bs;
        if (this.dataGridView1.EditingControl != null)
          this.dataGridView1.EditingControl.TextChanged += new EventHandler(this.Control_TextChanged);
        if (this.dataGridView1.Rows.Count <= 1)
          return;
        this.dataGridView1.Rows[0].Frozen = true;
        this.dataGridView1.Columns[0].Visible = false;
        this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        this.FreezeGrid();
        if (!string.IsNullOrEmpty(this.searchID))
        {
          this.dataGridView1.CurrentCell = this.dataGridView1.Rows[0].Cells[1];
          this.dataGridView1.CurrentCell.Value = (object) this.searchID;
          this.Control_TextChanged((object) null, (EventArgs) null);
        }
      }
    }

    private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (this.dataGridView1.CurrentRow.Index <= 0)
        return;
      this.buttonOK_Click(sender, (EventArgs) e);
    }

    private void dataGridView1_SelectionChanged(object sender, EventArgs e)
    {
      if (this.dataGridView1.CurrentCell == null || this.dataGridView1.CurrentCell.RowIndex != 0)
        return;
      this.dataGridView1.CurrentCell.Selected = false;
    }

    private void dataGridView1_EditingControlShowing(
      object sender,
      DataGridViewEditingControlShowingEventArgs e)
    {
      e.CellStyle.BackColor = Color.White;
    }

    private void Control_TextChanged(object sender, EventArgs e)
    {
      if (this.dataGridView1.CurrentCell == null || !(this.dataGridView1.CurrentCell.Value.ToString() != this.dataGridView1.CurrentCell.EditedFormattedValue.ToString()))
        return;
      int rowIndex = this.dataGridView1.CurrentCell.RowIndex;
      int columnIndex = this.dataGridView1.CurrentCell.ColumnIndex;
      if (rowIndex == 0)
      {
        if (columnIndex != 1 || string.IsNullOrEmpty(this.searchID))
        {
          this.dataGridView1.CurrentCell.Value = this.dataGridView1.CurrentCell.EditedFormattedValue;
        }
        else
        {
          this.dataGridView1.CurrentCell.Value = (object) this.searchID;
          this.searchID = (string) null;
        }
        StringBuilder stringBuilder = new StringBuilder();
        foreach (DataGridViewCell cell in (BaseCollection) this.dataGridView1.Rows[0].Cells)
        {
          if (cell.ColumnIndex == 0)
          {
            stringBuilder.Append("FilterKey = 'KEY' OR ( ");
            this.dataGridView1.Columns[0].Visible = false;
          }
          else
          {
            if (cell.ColumnIndex > 1)
              stringBuilder.Append(" AND ");
            stringBuilder.AppendFormat(" [{0}] LIKE '{1}%'", (object) this.dataGridView1.Columns[cell.ColumnIndex].Name, (object) Convert.ToString(cell.Value));
          }
        }
        stringBuilder.Append(" ) ");
        this.bs.Filter = stringBuilder.ToString();
        if (this.dataGridView1.CurrentCell.ColumnIndex != columnIndex)
          this.dataGridView1.CurrentCell = this.dataGridView1.Rows[rowIndex].Cells[columnIndex];
        if (this.dataGridView1.EditingControl != null)
        {
          this.dataGridView1.EditingControl.Focus();
          if (this.dataGridView1.EditingControl is DataGridViewTextBoxEditingControl editingControl)
            editingControl.SelectionStart = editingControl.TextLength;
        }
      }
    }

    private void dataGridView1_DataBindingComplete(
      object sender,
      DataGridViewBindingCompleteEventArgs e)
    {
      if (!this.dt.DefaultView.Sort.Contains("FilterKey") && !string.IsNullOrEmpty(this.dt.DefaultView.Sort))
        this.dt.DefaultView.Sort = "[FilterKey] DESC, " + this.dt.DefaultView.Sort;
      this.FreezeGrid();
    }

    public string Customer_ID
    {
      get => this.CustID;
      set => this.searchID = value;
    }

    public string Site_ID
    {
      get => this.SiteID;
      set => this.SiteID = value;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CustomerSelector));
      this.dataGridView1 = new DataGridView();
      this.FilterKey = new DataGridViewTextBoxColumn();
      this.CustomerID = new DataGridViewTextBoxColumn();
      this.CustName = new DataGridViewTextBoxColumn();
      this.Address1 = new DataGridViewTextBoxColumn();
      this.Address2 = new DataGridViewTextBoxColumn();
      this.Address3 = new DataGridViewTextBoxColumn();
      this.City = new DataGridViewTextBoxColumn();
      this.State = new DataGridViewTextBoxColumn();
      this.Country = new DataGridViewTextBoxColumn();
      this.Zipcode = new DataGridViewTextBoxColumn();
      this.buttonOK = new Button();
      this.buttonCancel = new Button();
      this.labelCount = new Label();
      ((ISupportInitialize) this.dataGridView1).BeginInit();
      this.SuspendLayout();
      this.dataGridView1.AllowUserToAddRows = false;
      this.dataGridView1.AllowUserToDeleteRows = false;
      this.dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Columns.AddRange((DataGridViewColumn) this.FilterKey, (DataGridViewColumn) this.CustomerID, (DataGridViewColumn) this.CustName, (DataGridViewColumn) this.Address1, (DataGridViewColumn) this.Address2, (DataGridViewColumn) this.Address3, (DataGridViewColumn) this.City, (DataGridViewColumn) this.State, (DataGridViewColumn) this.Country, (DataGridViewColumn) this.Zipcode);
      this.dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
      this.dataGridView1.Location = new Point(12, 12);
      this.dataGridView1.MultiSelect = false;
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.RowHeadersVisible = false;
      this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dataGridView1.Size = new Size(954, 358);
      this.dataGridView1.TabIndex = 0;
      this.dataGridView1.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
      this.dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
      this.dataGridView1.SelectionChanged += new EventHandler(this.dataGridView1_SelectionChanged);
      this.dataGridView1.MouseDoubleClick += new MouseEventHandler(this.dataGridView1_MouseDoubleClick);
      this.FilterKey.HeaderText = "FilterKey";
      this.FilterKey.Name = "FilterKey";
      this.FilterKey.Visible = false;
      this.CustomerID.HeaderText = "Customer ID";
      this.CustomerID.Name = "CustomerID";
      this.CustName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.CustName.HeaderText = "Name";
      this.CustName.Name = "CustName";
      this.Address1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.Address1.HeaderText = "Address1";
      this.Address1.Name = "Address1";
      this.Address2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.Address2.HeaderText = "Address2";
      this.Address2.Name = "Address2";
      this.Address3.HeaderText = "Address3";
      this.Address3.Name = "Address3";
      this.City.HeaderText = "City";
      this.City.Name = "City";
      this.State.HeaderText = "State";
      this.State.Name = "State";
      this.Country.HeaderText = "Country";
      this.Country.Name = "Country";
      this.Zipcode.HeaderText = "Zipcode";
      this.Zipcode.Name = "Zipcode";
      this.buttonOK.Anchor = AnchorStyles.Bottom;
      this.buttonOK.Location = new Point(402, 376);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new Size(75, 23);
      this.buttonOK.TabIndex = 1;
      this.buttonOK.Text = "OK";
      this.buttonOK.UseVisualStyleBackColor = true;
      this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
      this.buttonCancel.Anchor = AnchorStyles.Bottom;
      this.buttonCancel.Location = new Point(502, 376);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new Size(75, 23);
      this.buttonCancel.TabIndex = 2;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.UseVisualStyleBackColor = true;
      this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
      this.labelCount.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.labelCount.AutoSize = true;
      this.labelCount.Location = new Point(0, 393);
      this.labelCount.Name = "labelCount";
      this.labelCount.Size = new Size(71, 13);
      this.labelCount.TabIndex = 3;
      this.labelCount.Text = "Records: 000";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(978, 408);
      this.Controls.Add((Control) this.labelCount);
      this.Controls.Add((Control) this.buttonCancel);
      this.Controls.Add((Control) this.buttonOK);
      this.Controls.Add((Control) this.dataGridView1);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (CustomerSelector);
      this.Text = "Customer Selector";
      this.Shown += new EventHandler(this.CustomerSelector_Shown);
      ((ISupportInitialize) this.dataGridView1).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}

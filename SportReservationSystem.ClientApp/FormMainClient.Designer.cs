using MetroFramework.Controls;

namespace SportReservationSystem.ClientApp;

partial class FormMainClient
{
    private MetroTabControl _tabs = null!;
    private MetroTabPage _tabTerrains = null!;
    private MetroTabPage _tabCreneaux = null!;
    private MetroTabPage _tabReservations = null!;
    private MetroGrid _gridTerrains = null!;
    private MetroGrid _gridCreneaux = null!;
    private MetroGrid _gridReservations = null!;
    private MetroDateTime _dateFilter = null!;
    private MetroButton _btnSearch = null!;
    private MetroButton _btnBook = null!;
    private MetroButton _btnRefreshReservations = null!;
    private MetroButton _btnCancelReservation = null!;
    private Panel _pnlSearch = null!;
    private Panel _pnlReservations = null!;

    private void InitializeComponent()
    {
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
        _tabs = new MetroTabControl();
        _tabTerrains = new MetroTabPage();
        _gridTerrains = new MetroGrid();
        _tabCreneaux = new MetroTabPage();
        _gridCreneaux = new MetroGrid();
        _pnlSearch = new Panel();
        _dateFilter = new MetroDateTime();
        _btnSearch = new MetroButton();
        _btnBook = new MetroButton();
        _tabReservations = new MetroTabPage();
        _gridReservations = new MetroGrid();
        _pnlReservations = new Panel();
        _btnRefreshReservations = new MetroButton();
        _btnCancelReservation = new MetroButton();
        _tabs.SuspendLayout();
        _tabTerrains.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_gridTerrains).BeginInit();
        _tabCreneaux.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_gridCreneaux).BeginInit();
        _pnlSearch.SuspendLayout();
        _tabReservations.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_gridReservations).BeginInit();
        _pnlReservations.SuspendLayout();
        SuspendLayout();
        // 
        // _tabs
        // 
        _tabs.Controls.Add(_tabTerrains);
        _tabs.Controls.Add(_tabCreneaux);
        _tabs.Controls.Add(_tabReservations);
        _tabs.Dock = DockStyle.Fill;
        _tabs.Location = new Point(20, 60);
        _tabs.Name = "_tabs";
        _tabs.Padding = new Point(6, 8);
        _tabs.SelectedIndex = 0;
        _tabs.Size = new Size(960, 620);
        _tabs.TabIndex = 0;
        _tabs.UseSelectable = true;
        _tabs.SelectedIndexChanged += _tabs_SelectedIndexChanged;
        // 
        // _tabTerrains
        // 
        _tabTerrains.Controls.Add(_gridTerrains);
        _tabTerrains.HorizontalScrollbarBarColor = true;
        _tabTerrains.HorizontalScrollbarHighlightOnWheel = false;
        _tabTerrains.HorizontalScrollbarSize = 10;
        _tabTerrains.Location = new Point(4, 38);
        _tabTerrains.Name = "_tabTerrains";
        _tabTerrains.Size = new Size(952, 578);
        _tabTerrains.TabIndex = 0;
        _tabTerrains.Text = "Terrains";
        _tabTerrains.VerticalScrollbarBarColor = true;
        _tabTerrains.VerticalScrollbarHighlightOnWheel = false;
        _tabTerrains.VerticalScrollbarSize = 10;
        // 
        // _gridTerrains
        // 
        _gridTerrains.AllowUserToResizeRows = false;
        _gridTerrains.BackgroundColor = Color.FromArgb(255, 255, 255);
        _gridTerrains.CellBorderStyle = DataGridViewCellBorderStyle.None;
        _gridTerrains.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle1.BackColor = Color.FromArgb(0, 174, 219);
        dataGridViewCellStyle1.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Pixel);
        dataGridViewCellStyle1.ForeColor = Color.FromArgb(255, 255, 255);
        dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(0, 198, 247);
        dataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(17, 17, 17);
        dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
        _gridTerrains.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = Color.FromArgb(255, 255, 255);
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Pixel);
        dataGridViewCellStyle2.ForeColor = Color.FromArgb(136, 136, 136);
        dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(0, 198, 247);
        dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(17, 17, 17);
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
        _gridTerrains.DefaultCellStyle = dataGridViewCellStyle2;
        _gridTerrains.Dock = DockStyle.Fill;
        _gridTerrains.EnableHeadersVisualStyles = false;
        _gridTerrains.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Pixel);
        _gridTerrains.GridColor = Color.FromArgb(255, 255, 255);
        _gridTerrains.Location = new Point(0, 0);
        _gridTerrains.Name = "_gridTerrains";
        _gridTerrains.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle3.BackColor = Color.FromArgb(0, 174, 219);
        dataGridViewCellStyle3.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Pixel);
        dataGridViewCellStyle3.ForeColor = Color.FromArgb(255, 255, 255);
        dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(0, 198, 247);
        dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(17, 17, 17);
        dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
        _gridTerrains.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
        _gridTerrains.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
        _gridTerrains.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _gridTerrains.Size = new Size(952, 578);
        _gridTerrains.TabIndex = 2;
        // 
        // _tabCreneaux
        // 
        _tabCreneaux.Controls.Add(_gridCreneaux);
        _tabCreneaux.Controls.Add(_pnlSearch);
        _tabCreneaux.HorizontalScrollbarBarColor = true;
        _tabCreneaux.HorizontalScrollbarHighlightOnWheel = false;
        _tabCreneaux.HorizontalScrollbarSize = 10;
        _tabCreneaux.Location = new Point(4, 39);
        _tabCreneaux.Name = "_tabCreneaux";
        _tabCreneaux.Size = new Size(192, 57);
        _tabCreneaux.TabIndex = 1;
        _tabCreneaux.Text = "Creneaux";
        _tabCreneaux.VerticalScrollbarBarColor = true;
        _tabCreneaux.VerticalScrollbarHighlightOnWheel = false;
        _tabCreneaux.VerticalScrollbarSize = 10;
        // 
        // _gridCreneaux
        // 
        _gridCreneaux.AllowUserToResizeRows = false;
        _gridCreneaux.BackgroundColor = Color.FromArgb(255, 255, 255);
        _gridCreneaux.CellBorderStyle = DataGridViewCellBorderStyle.None;
        _gridCreneaux.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle4.BackColor = Color.FromArgb(0, 174, 219);
        dataGridViewCellStyle4.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Pixel);
        dataGridViewCellStyle4.ForeColor = Color.FromArgb(255, 255, 255);
        dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(0, 198, 247);
        dataGridViewCellStyle4.SelectionForeColor = Color.FromArgb(17, 17, 17);
        dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
        _gridCreneaux.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
        dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle5.BackColor = Color.FromArgb(255, 255, 255);
        dataGridViewCellStyle5.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Pixel);
        dataGridViewCellStyle5.ForeColor = Color.FromArgb(136, 136, 136);
        dataGridViewCellStyle5.SelectionBackColor = Color.FromArgb(0, 198, 247);
        dataGridViewCellStyle5.SelectionForeColor = Color.FromArgb(17, 17, 17);
        dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
        _gridCreneaux.DefaultCellStyle = dataGridViewCellStyle5;
        _gridCreneaux.Dock = DockStyle.Fill;
        _gridCreneaux.EnableHeadersVisualStyles = false;
        _gridCreneaux.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Pixel);
        _gridCreneaux.GridColor = Color.FromArgb(255, 255, 255);
        _gridCreneaux.Location = new Point(0, 50);
        _gridCreneaux.Name = "_gridCreneaux";
        _gridCreneaux.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle6.BackColor = Color.FromArgb(0, 174, 219);
        dataGridViewCellStyle6.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Pixel);
        dataGridViewCellStyle6.ForeColor = Color.FromArgb(255, 255, 255);
        dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(0, 198, 247);
        dataGridViewCellStyle6.SelectionForeColor = Color.FromArgb(17, 17, 17);
        dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
        _gridCreneaux.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
        _gridCreneaux.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
        _gridCreneaux.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _gridCreneaux.Size = new Size(192, 7);
        _gridCreneaux.TabIndex = 2;
        // 
        // _pnlSearch
        // 
        _pnlSearch.Controls.Add(_dateFilter);
        _pnlSearch.Controls.Add(_btnSearch);
        _pnlSearch.Controls.Add(_btnBook);
        _pnlSearch.Dock = DockStyle.Top;
        _pnlSearch.Location = new Point(0, 0);
        _pnlSearch.Name = "_pnlSearch";
        _pnlSearch.Size = new Size(192, 50);
        _pnlSearch.TabIndex = 3;
        // 
        // _dateFilter
        // 
        _dateFilter.Location = new Point(10, 10);
        _dateFilter.Name = "_dateFilter";
        _dateFilter.Size = new Size(220, 23);
        _dateFilter.TabIndex = 0;
        // 
        // _btnSearch
        // 
        _btnSearch.Location = new Point(240, 10);
        _btnSearch.Name = "_btnSearch";
        _btnSearch.Size = new Size(220, 29);
        _btnSearch.TabIndex = 1;
        _btnSearch.Text = "Rechercher disponibilites";
        _btnSearch.UseSelectable = true;
        // 
        // _btnBook
        // 
        _btnBook.Location = new Point(480, 10);
        _btnBook.Name = "_btnBook";
        _btnBook.Size = new Size(170, 29);
        _btnBook.TabIndex = 2;
        _btnBook.Text = "Reserver selection";
        _btnBook.UseSelectable = true;
        // 
        // _tabReservations
        // 
        _tabReservations.Controls.Add(_gridReservations);
        _tabReservations.Controls.Add(_pnlReservations);
        _tabReservations.HorizontalScrollbarBarColor = true;
        _tabReservations.HorizontalScrollbarHighlightOnWheel = false;
        _tabReservations.HorizontalScrollbarSize = 10;
        _tabReservations.Location = new Point(4, 39);
        _tabReservations.Name = "_tabReservations";
        _tabReservations.Size = new Size(192, 57);
        _tabReservations.TabIndex = 2;
        _tabReservations.Text = "Reservations";
        _tabReservations.VerticalScrollbarBarColor = true;
        _tabReservations.VerticalScrollbarHighlightOnWheel = false;
        _tabReservations.VerticalScrollbarSize = 10;
        // 
        // _gridReservations
        // 
        _gridReservations.AllowUserToResizeRows = false;
        _gridReservations.BackgroundColor = Color.FromArgb(255, 255, 255);
        _gridReservations.CellBorderStyle = DataGridViewCellBorderStyle.None;
        _gridReservations.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle7.BackColor = Color.FromArgb(0, 174, 219);
        dataGridViewCellStyle7.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Pixel);
        dataGridViewCellStyle7.ForeColor = Color.FromArgb(255, 255, 255);
        dataGridViewCellStyle7.SelectionBackColor = Color.FromArgb(0, 198, 247);
        dataGridViewCellStyle7.SelectionForeColor = Color.FromArgb(17, 17, 17);
        dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
        _gridReservations.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
        dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle8.BackColor = Color.FromArgb(255, 255, 255);
        dataGridViewCellStyle8.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Pixel);
        dataGridViewCellStyle8.ForeColor = Color.FromArgb(136, 136, 136);
        dataGridViewCellStyle8.SelectionBackColor = Color.FromArgb(0, 198, 247);
        dataGridViewCellStyle8.SelectionForeColor = Color.FromArgb(17, 17, 17);
        dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
        _gridReservations.DefaultCellStyle = dataGridViewCellStyle8;
        _gridReservations.Dock = DockStyle.Fill;
        _gridReservations.EnableHeadersVisualStyles = false;
        _gridReservations.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Pixel);
        _gridReservations.GridColor = Color.FromArgb(255, 255, 255);
        _gridReservations.Location = new Point(0, 50);
        _gridReservations.Name = "_gridReservations";
        _gridReservations.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle9.BackColor = Color.FromArgb(0, 174, 219);
        dataGridViewCellStyle9.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Pixel);
        dataGridViewCellStyle9.ForeColor = Color.FromArgb(255, 255, 255);
        dataGridViewCellStyle9.SelectionBackColor = Color.FromArgb(0, 198, 247);
        dataGridViewCellStyle9.SelectionForeColor = Color.FromArgb(17, 17, 17);
        dataGridViewCellStyle9.WrapMode = DataGridViewTriState.True;
        _gridReservations.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
        _gridReservations.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
        _gridReservations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _gridReservations.Size = new Size(192, 7);
        _gridReservations.TabIndex = 2;
        // 
        // _pnlReservations
        // 
        _pnlReservations.Controls.Add(_btnRefreshReservations);
        _pnlReservations.Controls.Add(_btnCancelReservation);
        _pnlReservations.Dock = DockStyle.Top;
        _pnlReservations.Location = new Point(0, 0);
        _pnlReservations.Name = "_pnlReservations";
        _pnlReservations.Size = new Size(192, 50);
        _pnlReservations.TabIndex = 3;
        // 
        // _btnRefreshReservations
        // 
        _btnRefreshReservations.Location = new Point(20, 10);
        _btnRefreshReservations.Name = "_btnRefreshReservations";
        _btnRefreshReservations.Size = new Size(120, 30);
        _btnRefreshReservations.TabIndex = 0;
        _btnRefreshReservations.Text = "Actualiser";
        _btnRefreshReservations.UseSelectable = true;
        // 
        // _btnCancelReservation
        // 
        _btnCancelReservation.Location = new Point(160, 10);
        _btnCancelReservation.Name = "_btnCancelReservation";
        _btnCancelReservation.Size = new Size(170, 30);
        _btnCancelReservation.TabIndex = 1;
        _btnCancelReservation.Text = "Annuler selection";
        _btnCancelReservation.UseSelectable = true;
        // 
        // FormMainClient
        // 
        ClientSize = new Size(1000, 700);
        Controls.Add(_tabs);
        Name = "FormMainClient";
        Text = "Client - Sport Reservation System";
        _tabs.ResumeLayout(false);
        _tabTerrains.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_gridTerrains).EndInit();
        _tabCreneaux.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_gridCreneaux).EndInit();
        _pnlSearch.ResumeLayout(false);
        _tabReservations.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_gridReservations).EndInit();
        _pnlReservations.ResumeLayout(false);
        ResumeLayout(false);
    }
}

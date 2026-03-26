using MetroFramework.Controls;

namespace SportReservationSystem.GestionnaireApp;

partial class FormMainGestionnaire
{
    private MetroTabControl _tabs = null!;
    private MetroTabPage _tabDashboard = null!;
    private MetroTabPage _tabValidations = null!;
    private Panel _pnlDashboard = null!;
    private Panel _pnlValidations = null!;
    private MetroGrid _gridDashboard = null!;
    private MetroGrid _gridValidations = null!;
    private MetroButton _btnRefreshDashboard = null!;
    private MetroButton _btnRefresh = null!;
    private MetroButton _btnValidate = null!;
    private MetroButton _btnCancel = null!;

    private void InitializeComponent()
    {
        _tabs = new MetroTabControl();
        _tabDashboard = new MetroTabPage();
        _tabValidations = new MetroTabPage();
        _pnlDashboard = new Panel();
        _pnlValidations = new Panel();
        _gridDashboard = new MetroGrid();
        _gridValidations = new MetroGrid();
        _btnRefreshDashboard = new MetroButton();
        _btnRefresh = new MetroButton();
        _btnValidate = new MetroButton();
        _btnCancel = new MetroButton();
        ((System.ComponentModel.ISupportInitialize)_gridDashboard).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_gridValidations).BeginInit();
        _tabs.SuspendLayout();
        _tabDashboard.SuspendLayout();
        _tabValidations.SuspendLayout();
        _pnlDashboard.SuspendLayout();
        _pnlValidations.SuspendLayout();
        SuspendLayout();
        _tabs.Dock = DockStyle.Fill;
        _tabs.Controls.Add(_tabDashboard);
        _tabs.Controls.Add(_tabValidations);
        _tabDashboard.Text = "Dashboard / Planning";
        _tabDashboard.Controls.Add(_gridDashboard);
        _tabDashboard.Controls.Add(_pnlDashboard);
        _tabValidations.Text = "Validations / Annulations";
        _tabValidations.Controls.Add(_gridValidations);
        _tabValidations.Controls.Add(_pnlValidations);
        _pnlDashboard.Dock = DockStyle.Top;
        _pnlDashboard.Height = 50;
        _pnlDashboard.Controls.Add(_btnRefreshDashboard);
        _btnRefreshDashboard.Location = new Point(20, 10);
        _btnRefreshDashboard.Size = new Size(180, 30);
        _btnRefreshDashboard.Text = "Actualiser planning";
        _btnRefreshDashboard.UseSelectable = true;
        _gridDashboard.Dock = DockStyle.Fill;
        _pnlValidations.Dock = DockStyle.Top;
        _pnlValidations.Height = 50;
        _pnlValidations.Controls.Add(_btnRefresh);
        _pnlValidations.Controls.Add(_btnValidate);
        _pnlValidations.Controls.Add(_btnCancel);
        _btnRefresh.Location = new Point(20, 10);
        _btnRefresh.Size = new Size(120, 30);
        _btnRefresh.Text = "Actualiser";
        _btnRefresh.UseSelectable = true;
        _btnValidate.Location = new Point(160, 10);
        _btnValidate.Size = new Size(120, 30);
        _btnValidate.Text = "Valider";
        _btnValidate.UseSelectable = true;
        _btnCancel.Location = new Point(300, 10);
        _btnCancel.Size = new Size(120, 30);
        _btnCancel.Text = "Annuler";
        _btnCancel.UseSelectable = true;
        _gridValidations.Dock = DockStyle.Fill;
        ClientSize = new Size(1000, 700);
        Controls.Add(_tabs);
        Name = "FormMainGestionnaire";
        Text = "Gestionnaire - Sport Reservation System";
        ((System.ComponentModel.ISupportInitialize)_gridDashboard).EndInit();
        ((System.ComponentModel.ISupportInitialize)_gridValidations).EndInit();
        _tabs.ResumeLayout(false);
        _tabDashboard.ResumeLayout(false);
        _tabValidations.ResumeLayout(false);
        _pnlDashboard.ResumeLayout(false);
        _pnlValidations.ResumeLayout(false);
        ResumeLayout(false);
    }
}

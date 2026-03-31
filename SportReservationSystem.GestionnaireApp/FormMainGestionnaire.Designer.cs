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
    
    // NOUVEAUX BOUTONS
    private MetroButton _btnValidation = null!;
    private MetroButton _btnPlanning = null!;
    private MetroButton _btnStatistiques = null!;
    private Panel _pnlTop = null!;

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
        
        // NOUVEAUX COMPOSANTS
        _btnValidation = new MetroButton();
        _btnPlanning = new MetroButton();
        _btnStatistiques = new MetroButton();
        _pnlTop = new Panel();
        
        ((System.ComponentModel.ISupportInitialize)_gridDashboard).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_gridValidations).BeginInit();
        _tabs.SuspendLayout();
        _tabDashboard.SuspendLayout();
        _tabValidations.SuspendLayout();
        _pnlDashboard.SuspendLayout();
        _pnlValidations.SuspendLayout();
        _pnlTop.SuspendLayout();
        SuspendLayout();
        
        // ============================================
        // Panel Top (pour les boutons de navigation)
        // ============================================
        _pnlTop.Dock = DockStyle.Top;
        _pnlTop.Height = 50;
        _pnlTop.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
        
        // Bouton Validation
        _btnValidation.Location = new Point(20, 10);
        _btnValidation.Size = new Size(120, 30);
        _btnValidation.Text = "Validation";
        _btnValidation.UseSelectable = true;
        
        // Bouton Planning
        _btnPlanning.Location = new Point(150, 10);
        _btnPlanning.Size = new Size(120, 30);
        _btnPlanning.Text = "Planning";
        _btnPlanning.UseSelectable = true;
        
        // Bouton Statistiques
        _btnStatistiques.Location = new Point(280, 10);
        _btnStatistiques.Size = new Size(120, 30);
        _btnStatistiques.Text = "Statistiques";
        _btnStatistiques.UseSelectable = true;
        
        _pnlTop.Controls.Add(_btnValidation);
        _pnlTop.Controls.Add(_btnPlanning);
        _pnlTop.Controls.Add(_btnStatistiques);
        
        // ============================================
        // TabControl
        // ============================================
        _tabs.Dock = DockStyle.Fill;
        _tabs.Controls.Add(_tabDashboard);
        _tabs.Controls.Add(_tabValidations);
        
        _tabDashboard.Text = "Dashboard / Planning";
        _tabDashboard.Controls.Add(_gridDashboard);
        _tabDashboard.Controls.Add(_pnlDashboard);
        
        _tabValidations.Text = "Validations / Annulations";
        _tabValidations.Controls.Add(_gridValidations);
        _tabValidations.Controls.Add(_pnlValidations);
        
        // ============================================
        // Panel Dashboard
        // ============================================
        _pnlDashboard.Dock = DockStyle.Top;
        _pnlDashboard.Height = 50;
        _pnlDashboard.Controls.Add(_btnRefreshDashboard);
        
        _btnRefreshDashboard.Location = new Point(20, 10);
        _btnRefreshDashboard.Size = new Size(180, 30);
        _btnRefreshDashboard.Text = "Actualiser planning";
        _btnRefreshDashboard.UseSelectable = true;
        
        _gridDashboard.Dock = DockStyle.Fill;
        
        // ============================================
        // Panel Validations
        // ============================================
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
        
        // ============================================
        // Formulaire principal
        // ============================================
        ClientSize = new Size(1000, 700);
        Controls.Add(_tabs);
        Controls.Add(_pnlTop);
        
        Name = "FormMainGestionnaire";
        Text = "Gestionnaire - Sport Reservation System";
        
        ((System.ComponentModel.ISupportInitialize)_gridDashboard).EndInit();
        ((System.ComponentModel.ISupportInitialize)_gridValidations).EndInit();
        _tabs.ResumeLayout(false);
        _tabDashboard.ResumeLayout(false);
        _tabValidations.ResumeLayout(false);
        _pnlDashboard.ResumeLayout(false);
        _pnlValidations.ResumeLayout(false);
        _pnlTop.ResumeLayout(false);
        ResumeLayout(false);
    }
}
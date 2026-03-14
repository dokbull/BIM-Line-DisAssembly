partial class DirectoryTreeView
{
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DirectoryTreeView));
        this.dirTreeView = new System.Windows.Forms.TreeView();
        this.dirTreeViewImageList = new System.Windows.Forms.ImageList(this.components);
        this.SuspendLayout();
        // 
        // dirTreeView
        // 
        this.dirTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
        this.dirTreeView.ImageIndex = 0;
        this.dirTreeView.ImageList = this.dirTreeViewImageList;
        this.dirTreeView.Location = new System.Drawing.Point(0, 0);
        this.dirTreeView.Name = "dirTreeView";
        this.dirTreeView.SelectedImageIndex = 1;
        this.dirTreeView.ShowPlusMinus = false;
        this.dirTreeView.Size = new System.Drawing.Size(389, 300);
        this.dirTreeView.TabIndex = 0;
        this.dirTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.dirTreeView_AfterSelect);
        // 
        // dirTreeViewImageList
        // 
        this.dirTreeViewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("dirTreeViewImageList.ImageStream")));
        this.dirTreeViewImageList.TransparentColor = System.Drawing.Color.Transparent;
        this.dirTreeViewImageList.Images.SetKeyName(0, "close_folder_icon.jpg");
        this.dirTreeViewImageList.Images.SetKeyName(1, "open_folder_icon.jpg");
        // 
        // DirectoryTreeView
        // 
        this.Controls.Add(this.dirTreeView);
        this.Name = "DirectoryTreeView";
        this.Size = new System.Drawing.Size(389, 300);
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TreeView dirTreeView;
    private System.Windows.Forms.ImageList dirTreeViewImageList;
}

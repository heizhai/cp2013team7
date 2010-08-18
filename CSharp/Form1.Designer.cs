namespace TrafficLightSim2
{
    partial class TrafficLightSimulator
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runOnceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runMultipleCyclesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runSimulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setNoHorizontalLanesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setNoVerticalLanesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setHorizontalCarRegularityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setVerticalCarRegularityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runButton = new System.Windows.Forms.Button();
            this.cycleLightsButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.setNoHorizontalLanesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(684, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runOnceToolStripMenuItem,
            this.runMultipleCyclesToolStripMenuItem,
            this.runSimulationToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // runOnceToolStripMenuItem
            // 
            this.runOnceToolStripMenuItem.Name = "runOnceToolStripMenuItem";
            this.runOnceToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.runOnceToolStripMenuItem.Text = "Run Once";
            // 
            // runMultipleCyclesToolStripMenuItem
            // 
            this.runMultipleCyclesToolStripMenuItem.Name = "runMultipleCyclesToolStripMenuItem";
            this.runMultipleCyclesToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.runMultipleCyclesToolStripMenuItem.Text = "Run Multiple Cycles";
            // 
            // runSimulationToolStripMenuItem
            // 
            this.runSimulationToolStripMenuItem.Name = "runSimulationToolStripMenuItem";
            this.runSimulationToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.runSimulationToolStripMenuItem.Text = "Run Simulation";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // setNoHorizontalLanesToolStripMenuItem
            // 
            this.setNoHorizontalLanesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setNoVerticalLanesToolStripMenuItem,
            this.setHorizontalCarRegularityToolStripMenuItem,
            this.aToolStripMenuItem,
            this.setVerticalCarRegularityToolStripMenuItem});
            this.setNoHorizontalLanesToolStripMenuItem.Name = "setNoHorizontalLanesToolStripMenuItem";
            this.setNoHorizontalLanesToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.setNoHorizontalLanesToolStripMenuItem.Text = "Settings";
            // 
            // setNoVerticalLanesToolStripMenuItem
            // 
            this.setNoVerticalLanesToolStripMenuItem.Name = "setNoVerticalLanesToolStripMenuItem";
            this.setNoVerticalLanesToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.setNoVerticalLanesToolStripMenuItem.Text = "Set No. Horizontal Lanes";
            // 
            // setHorizontalCarRegularityToolStripMenuItem
            // 
            this.setHorizontalCarRegularityToolStripMenuItem.Name = "setHorizontalCarRegularityToolStripMenuItem";
            this.setHorizontalCarRegularityToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.setHorizontalCarRegularityToolStripMenuItem.Text = "Set No. Vertical Lanes";
            // 
            // aToolStripMenuItem
            // 
            this.aToolStripMenuItem.Name = "aToolStripMenuItem";
            this.aToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.aToolStripMenuItem.Text = "Set Horizontal Car Regularity";
            // 
            // setVerticalCarRegularityToolStripMenuItem
            // 
            this.setVerticalCarRegularityToolStripMenuItem.Name = "setVerticalCarRegularityToolStripMenuItem";
            this.setVerticalCarRegularityToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.setVerticalCarRegularityToolStripMenuItem.Text = "Set Vertical Car Regularity";
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(234, 616);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(105, 36);
            this.runButton.TabIndex = 1;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            // 
            // cycleLightsButton
            // 
            this.cycleLightsButton.Location = new System.Drawing.Point(345, 616);
            this.cycleLightsButton.Name = "cycleLightsButton";
            this.cycleLightsButton.Size = new System.Drawing.Size(105, 36);
            this.cycleLightsButton.TabIndex = 2;
            this.cycleLightsButton.Text = "Cycle Lights";
            this.cycleLightsButton.UseVisualStyleBackColor = true;
            // 
            // TrafficLightSimulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lime;
            this.ClientSize = new System.Drawing.Size(684, 664);
            this.Controls.Add(this.cycleLightsButton);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.menuStrip1);
            this.Name = "TrafficLightSimulator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Traffic Light Simulator";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runOnceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runMultipleCyclesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runSimulationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setNoHorizontalLanesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setNoVerticalLanesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setHorizontalCarRegularityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setVerticalCarRegularityToolStripMenuItem;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.Button cycleLightsButton;

    }
}


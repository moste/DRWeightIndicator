namespace DRWeightIndicator
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerDelay = new System.Windows.Forms.Timer(this.components);
            this.labelDebug = new System.Windows.Forms.Label();
            this.gLatIndicator = new System.Windows.Forms.Panel();
            this.panelBrakeLightLeft = new System.Windows.Forms.Panel();
            this.panelBrakeLightRight = new System.Windows.Forms.Panel();
            this.panelSteer = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // timerDelay
            // 
            this.timerDelay.Enabled = true;
            this.timerDelay.Interval = 200;
            this.timerDelay.Tick += new System.EventHandler(this.timerDelay_Tick);
            // 
            // labelDebug
            // 
            this.labelDebug.AutoSize = true;
            this.labelDebug.ForeColor = System.Drawing.SystemColors.Control;
            this.labelDebug.Location = new System.Drawing.Point(67, 34);
            this.labelDebug.Name = "labelDebug";
            this.labelDebug.Size = new System.Drawing.Size(0, 12);
            this.labelDebug.TabIndex = 0;
            // 
            // gLatIndicator
            // 
            this.gLatIndicator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.gLatIndicator.Location = new System.Drawing.Point(454, 26);
            this.gLatIndicator.Name = "gLatIndicator";
            this.gLatIndicator.Size = new System.Drawing.Size(15, 59);
            this.gLatIndicator.TabIndex = 1;
            // 
            // panelBrakeLightLeft
            // 
            this.panelBrakeLightLeft.BackColor = System.Drawing.Color.DarkRed;
            this.panelBrakeLightLeft.Location = new System.Drawing.Point(12, 12);
            this.panelBrakeLightLeft.Name = "panelBrakeLightLeft";
            this.panelBrakeLightLeft.Size = new System.Drawing.Size(42, 35);
            this.panelBrakeLightLeft.TabIndex = 3;
            // 
            // panelBrakeLightRight
            // 
            this.panelBrakeLightRight.BackColor = System.Drawing.Color.DarkRed;
            this.panelBrakeLightRight.Location = new System.Drawing.Point(946, 12);
            this.panelBrakeLightRight.Name = "panelBrakeLightRight";
            this.panelBrakeLightRight.Size = new System.Drawing.Size(42, 35);
            this.panelBrakeLightRight.TabIndex = 4;
            // 
            // panelSteer
            // 
            this.panelSteer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.panelSteer.Location = new System.Drawing.Point(470, 24);
            this.panelSteer.Name = "panelSteer";
            this.panelSteer.Size = new System.Drawing.Size(5, 60);
            this.panelSteer.TabIndex = 5;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(1000, 100);
            this.Controls.Add(this.panelSteer);
            this.Controls.Add(this.gLatIndicator);
            this.Controls.Add(this.panelBrakeLightRight);
            this.Controls.Add(this.panelBrakeLightLeft);
            this.Controls.Add(this.labelDebug);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.Opacity = 0.9D;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerDelay;
        private System.Windows.Forms.Label labelDebug;
        private System.Windows.Forms.Panel gLatIndicator;
        private System.Windows.Forms.Panel panelBrakeLightLeft;
        private System.Windows.Forms.Panel panelBrakeLightRight;
        private System.Windows.Forms.Panel panelSteer;
    }
}


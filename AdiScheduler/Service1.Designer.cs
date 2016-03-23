namespace WebSpiderScheduler
{
    partial class Service1
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
            this.TimerAlert = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.TimerAlert)).BeginInit();
            // 
            // TimerAlert
            // 
            this.TimerAlert.Enabled = true;
            this.TimerAlert.Interval = 60000D;
            this.TimerAlert.Elapsed += new System.Timers.ElapsedEventHandler(this.TimerAlert_Elapsed);
            // 
            // Service1
            // 
            this.ServiceName = "Service1";
            ((System.ComponentModel.ISupportInitialize)(this.TimerAlert)).EndInit();

        }

        #endregion

        private System.Timers.Timer TimerAlert;
    }
}

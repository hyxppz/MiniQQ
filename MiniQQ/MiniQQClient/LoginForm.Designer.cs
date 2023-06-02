namespace MiniQQClient
{
    partial class LoginForm
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
            Loginbutton = new Button();
            Registerbutton = new Button();
            SuspendLayout();
            // 
            // Loginbutton
            // 
            Loginbutton.Location = new Point(56, 396);
            Loginbutton.Name = "Loginbutton";
            Loginbutton.Size = new Size(75, 35);
            Loginbutton.TabIndex = 0;
            Loginbutton.Text = "登录";
            Loginbutton.UseVisualStyleBackColor = true;
            Loginbutton.Click += Loginbutton_Click;
            // 
            // Registerbutton
            // 
            Registerbutton.Location = new Point(376, 40);
            Registerbutton.Name = "Registerbutton";
            Registerbutton.Size = new Size(70, 32);
            Registerbutton.TabIndex = 1;
            Registerbutton.Text = "用户注册";
            Registerbutton.UseVisualStyleBackColor = true;
            Registerbutton.Click += Registerbutton_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(510, 457);
            Controls.Add(Registerbutton);
            Controls.Add(Loginbutton);
            Name = "LoginForm";
            Text = "MiniQQ登录";
            ResumeLayout(false);
        }

        #endregion

        private Button Loginbutton;
        private Button Registerbutton;
    }
}
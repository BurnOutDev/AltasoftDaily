using AltasoftDaily.Core;
using AltasoftDaily.Domain.POCO;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AltasoftDaily.UserInterface.WindowsForms.Forms
{
    public partial class ChangePasswordForm : MetroForm
    {
        private AltasoftDailyContext _db;
        public AltasoftDailyContext db
        {
            get
            {
                if (_db == null)
                    _db = new AltasoftDailyContext();
                return _db;
            }
        }
        public User User { get; set; }

        public ChangePasswordForm(User user, bool forced = false)
        {
            InitializeComponent();

            User = user;

            if (forced)
                btnCancel.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (EncryptionManagement.Validate(User.Password, ctrlChangePassword.PasswordOld, User.Salt))
            {
                if (ctrlChangePassword.PasswordNew != ctrlChangePassword.PasswordOld)
                {
                    if (ctrlChangePassword.PasswordNew == ctrlChangePassword.PasswordNewRe)
                    {
                        using (var db = new AltasoftDailyContext())
                        {
                            var dbUser = db.Users.FirstOrDefault(x => x.UserID == User.UserID);
                            var salt = EncryptionManagement.CreateSalt();
                            dbUser.Salt = salt;
                            dbUser.Password = EncryptionManagement.Encrypt(ctrlChangePassword.PasswordNewRe, salt);
                            dbUser.LastPasswordChange = DateTime.Now;
                            dbUser.ForceUserToChangePassword = false;
                            
                            db.SaveChanges();
                        }

                        MessageBox.Show("პაროლი წარმატებით შეიცვალა!");
                        DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                    else { MessageBox.Show("გთხოვთ სწორად გაიმეოროთ პაროლი!"); }
                }
                else { MessageBox.Show("იგივე პაროლის გამოყენება აკრძალულია!"); }
            }
            else { MessageBox.Show("ძველი პაროლი არასწორია!"); }
        }

        private void ChangePasswordForm_Load(object sender, EventArgs e)
        {

        }
    }
}

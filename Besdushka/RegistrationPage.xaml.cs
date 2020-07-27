using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Besdushka
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void okButton_Clicked(object sender, EventArgs e)
        {
            if (PasswordInput.Text != RepeatPasswordInput.Text)
            {
                DisplayAlert("Уведомление", "Пароли не совпадают", "сука");
                return;
            }

            //ну тут ещё кароч

            if (!checkUniqueLogin(LoginInput.Text))
                return;

            DBConnect db = new DBConnect();
            MySqlCommand command = new MySqlCommand("INSERT INTO users (login, password, name) VALUES (@login, @password, @name)", db.getConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = LoginInput.Text;
            command.Parameters.Add("@password", MySqlDbType.VarChar).Value = PasswordInput.Text;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = NameInput.Text;

            db.openConnection();
            if (command.ExecuteNonQuery() == 1)
                DisplayAlert("Уведомление", "Вы восхитительны", "ОK");
            else
                DisplayAlert("Плохие новости", "опять на серверах сосиськи жарят", "ПРОИЗОШЁЛ ПОДРЫВ ЖЁПЫ");

            db.closeConnection();
        }

        public Boolean checkUniqueLogin(string lgn)
        {
            DBConnect db = new DBConnect();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM users WHERE login = @lgn", db.getConnection());
            command.Parameters.Add("@lgn", MySqlDbType.VarChar).Value = lgn;
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                DisplayAlert("Уведомление", "Логин занят", "Напишу флявель через русскую о");
                return false;
            }
            else
                return true;

        }
    
    }
   }

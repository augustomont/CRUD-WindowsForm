using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;

namespace crudWindowsForm
{
    public partial class Form1 : Form
    {
        //criar uma propriedade
        MySqlConnection Conexao;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                //Criar variavel de fonte de dados para inserir no MySqlConnection()
                string data_source = "datasource=localhost;username=root;password=1234;database=db_agenda";
               
                //Criar conexao com mysql
                Conexao = new MySqlConnection(data_source);

                string sql = "INSERT INTO contatos (nome, email, telefone) " +
                             "VALUES " + "('" + txtNome.Text + "', '" + txtEmail.Text + "', '" + txtTelefone.Text + "')";
                
                //Executar comando insert
                MySqlCommand comando = new MySqlCommand(sql, Conexao);//construtor da classe, recebe o comando a ser executado, e a conexao com o banco de dados

                Conexao.Open();//abrir a conexao

                comando.ExecuteReader();

                MessageBox.Show("Inserido com sucesso!");
                    

            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } finally
            {
                Conexao.Close();
            }
        }
    }
}

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
        private MySqlConnection Conexao; //private para ser visivel apenas dentro da classe
        //Criar variavel de fonte de dados para inserir no MySqlConnection()
        private string data_source = "datasource=localhost;username=root;password=1234;database=db_agenda";


        public Form1()
        {
            InitializeComponent();

            //Todos esses componentes podem ser alterados direto pelas propriedades
            /*lstContatos.View = View.Details;
            lstContatos.LabelEdit = true;
            lstContatos.AllowColumnReorder = true;
            lstContatos.FullRowSelect = true;
            lstContatos.GridLines = true;

            lstContatos.Columns.Add("ID");
            lstContatos.Columns.Add("Nome");
            lstContatos.Columns.Add("E-Mail");
            lstContatos.Columns.Add("Telefone");
            */
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
               
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
                txtNome.Text = "";//limpar os campos depois de adcionado
                txtEmail.Text = "";
                txtTelefone.Text = "";
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                Conexao = new MySqlConnection(data_source);//criar conexao ao banco

                string q = "'%" + txtBuscar.Text + "%'";//variavel debusca
                string sql = "SELECT * " + "FROM contatos " + "WHERE nome LIKE " + q + " OR email LIKE " + q;//linha de comando

                Conexao.Open();//abre a conexao

                MySqlCommand comando = new MySqlCommand(sql, Conexao);//Cria o comando


                MySqlDataReader reader = comando.ExecuteReader();

                lstContatos.Items.Clear();//limpar lista antes de exibir busca

                while(reader.Read())//acessa o banco de dados, e salva dentro de row os resultados
                {
                    string[] row =
                    {
                        reader.GetString(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                    };

                    var linha_listview = new ListViewItem(row);//salva a busca em uma variavel dinamica

                    lstContatos.Items.Add(linha_listview);//escreve uma linha com o resultado da busca
                }

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            } finally
            {
                Conexao.Close();
            }
        }
    }
}

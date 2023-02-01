using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using System.Data.SqlTypes;
using System.Security.AccessControl;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing.Text;
//using System.Data.SqlClient.SqlException;


namespace PRUEBA01022023
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            LlenarLista();
            LlenarLista2();
            LlenarLista3();

            
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Indicamos que usaremos la conexion y arrojamos un mensaje. 
            STORE.Conectar();
            MessageBox.Show("Conexion exitosa");
        }

        //Id_Store
        public void LlenarLista()
        {
            STORE.Conectar();
            DataTable dt = new DataTable();
            {
                const string sql = "Select MAX(id_store)+1 as d from T_Store";
                using (SqlCommand cmd = new SqlCommand(sql, STORE.Conectar()))

                {
                    SqlDataReader rd;
                    rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        comboBox1.Items.Add(rd[0].ToString());

                    }
                }
            }
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        //Llenado de id_state 
        public void LlenarLista2()
        {
            STORE.Conectar();
            DataTable dt = new DataTable();
            {
                const string sql = "Select desc_state from T_State order by desc_state asc";
                using (SqlCommand cmd = new SqlCommand(sql, STORE.Conectar()))
                {
                    SqlDataReader rd;
                    rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        comboBox2.Items.Add(rd[0].ToString());
                    }
                }
            }
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        //Llenado de id_acc
        public void LlenarLista3()
        {
            STORE.Conectar();
            DataTable dt = new DataTable();
            {
                const string sql = "Select nam_acc from T_Account order by nam_acc asc";
                using (SqlCommand cmd = new SqlCommand(sql, STORE.Conectar()))
                {
                    SqlDataReader rd;
                    rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        comboBox3.Items.Add(rd[0].ToString());

                    }
                }
            }
            //Bloquea el igreso de escritura. 
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            
        }

        public async void button1_Click(object sender, EventArgs e)
        {
            
            //Instaciar el formulario hijo
            //Consulta y parametro para id_acc, id_state e id_format
            string sql = "Select id_acc from T_Account where nam_acc = " + "'" + comboBox3.Text + "'";
            string sql4 = "Select id_state from T_State where desc_state= " + "'" + comboBox2.Text + "'";
            string sql5 = "Select nam_format from T_Formatos inner join T_Account on T_Formatos.id_acc = T_Account.id_acc where nam_acc = '" + comboBox3.Text + "'";

            //Para id_format nam_format
            using (SqlCommand cmd = new SqlCommand(sql5, STORE.Conectar()))
            {
                SqlDataReader rd;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    comboBox4.Text = rd[0].ToString();
                }
            }

            //id_Account
            using (SqlCommand cmd = new SqlCommand(sql, STORE.Conectar()))
            {
                SqlDataReader rd;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    textBox4.Text = rd[0].ToString();
                }
            }
            //Consulta para mandar dato de id_state
            using (SqlCommand cmd4 = new SqlCommand(sql4, STORE.Conectar()))
            {
                SqlDataReader rd;
                rd = cmd4.ExecuteReader();
                while (rd.Read())
                {
                    textBox3.Text = rd[0].ToString();
                }
            }
        }

    }
}

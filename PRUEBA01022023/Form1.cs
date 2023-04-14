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
using System.Security.Cryptography.X509Certificates;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using System.Security.Principal;
using static System.Net.Mime.MediaTypeNames;
using System.Collections;
using System.Data.OleDb;
using System.Runtime.InteropServices;
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


        }

        //Id_Store
        public void LlenarLista()
        {
            STORE.Conectar();
            DataTable dt = new DataTable();
            {
                //Modificar para la tabla correspondiente de la base de datos
                // T_Store 
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
            comboBox6.DropDownStyle = ComboBoxStyle.DropDownList;

            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;

        }


        public void comboBox3_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            STORE.Conectar();
            
            //string da2 = "Select nam_format from T_Formatos inner join T_Account on T_Formatos.id_acc= T_Account.id_acc where nam_acc = '" + comboBox3.Text + "'";
            string da = "Declare @nam_format nvarchar(50)" +
                "if @nam_format = (Select nam_format from T_Formatos inner join " +
                "T_Account on T_Formatos.id_acc = T_Account.id_acc where nam_format = '" + comboBox3.Text + "') Select @nam_format from T_Formatos inner join " +
                "T_Account on T_Formatos.id_acc = T_Account.id_acc where nam_acc = @nam_format " +
                "else Select nam_format from T_Formatos inner join T_Account on T_Formatos.id_acc = T_Account.id_acc where nam_acc like '" + comboBox3.Text + "'";

            /*var da2 = int.Parse(da);
            var da3 = int.TryParse(da, out da2);*/
           

            using (SqlCommand cmd = new SqlCommand(da, STORE.Conectar())) 
            {
                SqlDataReader rd;
                SqlDataReader sqlDataReader;
                try
                {

                    
                    sqlDataReader = cmd.ExecuteReader();
                    rd = sqlDataReader;
                    
                }catch (System.Data.SqlClient.SqlException)
                {
                    //rd = "SAM''S";
                    MessageBox.Show("si entro", "miki");
                    sqlDataReader = cmd.ExecuteReader();
                    rd = sqlDataReader;
                }
                while (rd.Read()) 
                {
                    comboBox6.Items.Add(rd[0].ToString());
                }
                if (da.Equals(rd))
                {
                    comboBox6.DisplayMember = "@nam_format";
                }
            }
            comboBox6.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void button1_Click_1(object sender, EventArgs e)
        {
            
            this.Hide();

            MessageBox.Show("si entro", "prueba");

            //Instaciar el formulario hijo
            //Consulta y parametro para id_acc, id_state e id_formato   

            string sql = "Select nam_acc from T_Account where nam_acc = " + "'" + comboBox3.Text + "'";
            string sql2 = "Select id_state from T_State where desc_state= " + "'" + comboBox2.Text + "'";
            string sql3 = "Select id_format from T_Formatos inner join T_Account on T_Formatos.id_acc = T_Account.id_acc where nam_format = '" + comboBox6.Text + "'";


            //Para id_format nam_format
            using (SqlCommand cmd = new SqlCommand(sql3, STORE.Conectar()))
            {
                SqlDataReader rd;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    textBox5.Text = rd[0].ToString();
                }
            }
            //id_Account
            DataTable dt = new DataTable();
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
            using (SqlCommand cmd4 = new SqlCommand(sql2, STORE.Conectar()))
            {
                SqlDataReader rd;
                rd = cmd4.ExecuteReader();
                while (rd.Read())
                {
                    textBox3.Text = rd[0].ToString();
                }
            }
            //Variable para colocar mayusculas 
            string sr2 = textBox1.Text.ToUpper();
            
            //Pasa la imformacin guardada al formulario 2
            Form2 FHPP = new Form2();
            AddOwnedForm(FHPP);
            FHPP.textBox2.Text = this.comboBox1.Text;
            FHPP.textBox3.Text = this.textBox3.Text;
            FHPP.textBox4.Text = this.textBox4.Text;
            FHPP.textBox5.Text =  sr2;
            FHPP.textBox6.Text = this.comboBox6.Text + ":" + " " + sr2 + "_";
            //id_format
            
            FHPP.textBox7.Text = this.textBox5.Text;
            

            FHPP.Show();
        }

        //
        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3 == null) 
            {
                comboBox6.Items.Clear();
            }
            comboBox3.Enabled = false;
            comboBox6.Enabled = false;
            
        }

        //Boton que limpia los combobox de nam_acc y nam_format
        private void button2_Click(object sender, EventArgs e)
        {
            //comboBox1.
            
            comboBox6.Items.Clear();
            comboBox3.Enabled=true;
            comboBox6.Enabled = true;

          
        }

        private void comboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            comboBox3.Enabled = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

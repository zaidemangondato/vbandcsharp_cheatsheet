

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient; //a package used for connecting visual c# application to MySQL database.
namespace DevTestProj_Csharp
{
        public partial class Form1 : Form
        {
            //to Connect to MySQL Database URL
            MySqlConnection con = null;
            MySqlCommand cmd = null;
            String constr = "data source=localhost;database=my_db;user id=root;password=;"; //The MySQL Data source or MySQL Connection
            public Form1() //Do not delete this default Constructor otherwise the Program will not run or could encounter errors.
            {
                InitializeComponent();//Initialize Components from the Form1 Designer.
            }
            public sub showForm2() // Method for showing another form
            {
                    Form2 f2 = new Form2();
                    f2.show(); // you can do f2.ShowDialog(); if you want.
            }
            public void toInsertData() //Source Codes for Inserting Data from textboxes to MySQL Database
            {   //You can Add Trappings like: if/else or try/catch if you want
                    con = new MySqlConnection(constr);
                    con.Open();
                    String query = "  insert into tbl_records(id,name,age)values('"+ txt_id.Text +"','"+ txt_name.Text +"','"+ txt_age.Text +"')";//Insert Query
                    cmd = new MySqlCommand(query);
                    cmd.Connection = con;
                    cmd.ExecuteReader();
                    con.Close();
                    MessageBox.Show("Successfully Saved.");
            }

            public void toUpdateData()//Source codes for Updating Data
            {   //You can Add Trappings like: if/else or try/catch if you want
                    con = new MySqlConnection(constr);
                    con.Open();
                    String query = "  update tbl_records set address = '"+ txt_address.Text +"' where id = '"+ txt_id.Text +"' "; // Update Query Statement
                    cmd = new MySqlCommand(query);
                    cmd.Connection = con;
                    cmd.ExecuteReader();
                    con.Close();
                    MessageBox.Show("Successfully Updated.");
            }
            
            public void loadDatagrid() // Source codes for Displaying data from Database Table to Datagridview
            {
                try
                {
                    con = new MySqlConnection(constr);
                    con.Open();
                    cmd = new MySqlCommand("Select * from tbl_products ", con); // Select Query Statement
                    rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dataGridView1.Rows.Clear();
                    while (rdr.Read() == true)
                    {
                        dataGridView1.Rows.Add(rdr[0],rdr[1],rdr[2],rdr[3],rdr[4],rdr[5],rdr[6]);//number of Rows in your datagridview1
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    con.Close();

                }
            }
            
            public void clickDatagridView() // Source Codes for Passing the Values from datagridview to textboxes and other controls.
            {   //You can Add Trappings like: if/else or try/catch if you want
                int indexRow;

                indexRow = e.RowIndex;
                DataGridViewRow row = dataGridView1.Rows[indexRow];

                txt_code.Text = row.Cells[0].Value.ToString();//convert current row values into string and pass it to the textbox
                txt_name.Text = row.Cells[1].Value.ToString();
                txt_desc.Text = row.Cells[2].Value.ToString();
                txt_supplier.Text = row.Cells[3].Value.ToString();
                txt_price.Text = row.Cells[4].Value.ToString();
                nud_stocks.Text = row.Cells[5].Value.ToString();
                dtp_expiry.Text = row.Cells[6].Value.ToString();
            }

            public void datagridHeaderName() // Source Codes for Naming the Column Header of the Datagridview
            {
                if (dgv_rsl.Columns.Count > 2)
                {
                    //your code
                    dgv_rsl.Columns[0].HeaderText = "Transaction ID";
                    dgv_rsl.Columns[1].HeaderText = "Customer ID";
                    dgv_rsl.Columns[2].HeaderText = "Customer Name";
                    dgv_rsl.Columns[3].HeaderText = "Hardware Code";
                    dgv_rsl.Columns[4].HeaderText = "Hardware Name";
                    dgv_rsl.Columns[5].HeaderText = "Serial Code";
                    dgv_rsl.Columns[6].HeaderText = "Unit Name";
                    dgv_rsl.Columns[7].HeaderText = "Problem";
                    dgv_rsl.Columns[8].HeaderText = "Status";
                    dgv_rsl.Columns[9].HeaderText = "Date & Time of Transaction";
                }
            }

            public void loadgrid() // Source codes for Displaying data from Database Table to Datagridview
            {   //You can Add Trappings like: if/else or try/catch if you want
                con = new MySqlConnection(constr);
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter();
                string sql = "SELECT * from tbl_records";                    // Select Query Statement
                da.SelectCommand = new MySqlCommand(sql, con);
                DataTable table = new DataTable();
                da.Fill(table);
                BindingSource bSource = new BindingSource();
                bSource.DataSource = table;
                dataGridView1.DataSource = bSource; //Binds the Source to datagridview1
            }
            public void fromComboBoxToTextBox() // Source Codes for Displaying Values from Database Table to Textboxes.
            {   //You can Add Trappings like: if/else or try/catch if you want
                con = new MySqlConnection(constr);
                con.Open();
                string query = "select * from tblvehiclecar where car_model = '" + comboBox2.Text + "' "; // Select Statement with where clauses
                cmd = new MySqlCommand(query);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read() == true)
                {
                    string car_id = rdr.GetString("car_id");                // Declaring values of the Field name to Textboxes 
                    string car_brand = rdr.GetString("car_brand");  
                    string car_color = rdr.GetString("car_color");
                    string car_year = rdr.GetString("car_year");
                    string car_passenger = rdr.GetString("car_passenger");
                    string car_price = rdr.GetString("car_price");
                    string car_plate_no = rdr.GetString("car_plate_no");
                    txtcarid.Text = car_id;
                    txtbrand.Text = car_brand;
                    txtcolor.Text = car_color;
                    txtyear.Text = car_year;
                    txtpass.Text = car_passenger;
                    txtplate.Text = car_plate_no;
                    txtcarprice.Text = car_price;
                    
                 
                }     
            }
            public void columnValuesToComboBox() // Source Codes for adding item of the comboBox from Columns Values
            {   //You can Add Trappings like: if/else or try/catch if you want
                
                    con = new MySqlConnection(constr);
                    con.Open();
                    string query = "select client_name from tbl_client"; // Select Query Statement with a specified field name
                    cmd = new MySqlCommand(query);
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read() == true)
                    {
                        string cname = rdr.GetString("client_name"); // Declaring field name values as String
                  
                   
                        comboBox1.Items.Add(cname); // Adds comboBox item from its field's Values 
                       
                    }
                    
                
            }
        }
}
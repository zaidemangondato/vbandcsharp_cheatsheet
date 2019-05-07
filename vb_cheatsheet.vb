Imports MySql.Data.MySqlClient

Dim constr As String = "Data source=localhost;database=my_db;user id=root;password=;"
Dim con As MySqlConnection = new MySqlConnection(constr)
Dim query As String
Dim da As MySqlDataAdapter = new MySqlDataAdapter
'--------------------------to insert----------------------------------------------------------
private sub toInsert()
    dim cmd as mysqlcommand = new mysqlcommand(query, con)
    con.open()
    query = "insert into tbl_name(id,name,age)values('"& id.text &"','"& name.text &"','"& age.text &"')"
    cmd = new mysqlcommand(query, con)
    dim i as integer = cmd.executenonquery()
    msgbox("successfully saved")
    con.close()
end Sub
'--------------------------to update----------------------------------------------------------
private sub toUpdate()
    dim cmd as mysqlcommand = new mysqlcommand(query, con)
    con.open()
    query = "update tblname set name = '"& name.text &"', age = '"& age.text &"' where id = '"& id.text &"'    "
    cmd = new mysqlcommand(query, con)
    dim i as integer = cmd.executenonquery()
    msgbox("Successfully updated!")
    con.close()
End sub
'--------------------------to display table in a datagridview----------------------------------------------------------
Public Sub loadgrid()
    dim sql as mysqlcommand = new mysqlcommand("select * from tblname", con)
    dim da as mysqldataadapter = new mysqldataadapter
    dim ds as dataset = new dataset
    da.selectcommand = sql
    da.fill(ds, "rec")
    datagridview1.datasource = ds
    datagridview1.datamember = "rec"
End Sub
'--------------------------to display data from DGV to textbox----------------------------------------------------------
Public Sub datagridToTextBox()
    dim i as integer
    i = datagridview.currentrow.index
    id.text = datagridview.item(0, i).value
End Sub
'--------------------------to display column on combobox---------------------------------------------------------------
Public Sub loadvaluesToComboBox()
        Try
            con.Open()
        Catch ex As MySqlException
            MsgBox(ex.Message)
        End Try
        Dim quer5 As String = "SELECT order_no FROM tbl_saved_order"
        Dim cmd5 As New MySqlCommand(quer5, con)
        Dim da5 As MySqlDataAdapter = New MySqlDataAdapter(cmd5)
        Dim dt5 As New DataTable("order_no")
        da5.Fill(dt5)
        If dt5.Rows.Count > 0 Then
            cbx_search_order_no.DataSource = dt5
            cbx_search_order_no.DisplayMember = "order_no"
        End If
        con.Close()
End Sub
'-------------------------to display data on a textbox when combobox is selected------------------------------------
Public Sub comboBoxToTextBox()
        da = (New MySqlDataAdapter)
        Dim con As MySqlConnection = New MySqlConnection(constr)
        Dim cmd As MySqlCommand = New MySqlCommand
        Dim datardr As MySqlDataReader
        Dim query As String
        
        con.Open()
        query = "select * from oes_client where client_name =  '" & cbx_client_name.Text & "'  "
        cmd.CommandText = query
        cmd.Connection = con
        da.SelectCommand = cmd
        datardr = cmd.ExecuteReader
        If datardr.HasRows Then
            datardr.Read()
            txt_client_id.Text = datardr("client_id")
        End If
End Sub

'Note that code here are not in proper cases so during your Development. Please Check the Case of each Code.
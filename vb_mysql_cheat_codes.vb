'***********************************************************
'Visual Basic - MySql Integration. Cheat Sheet Source Codes
'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

Imports MySql.Data.MySqlClient
'======= create a class named dbconnect.vb and use it for global mysql connection =========

Public Class dbconnect
	Public Shared constr As String = "data source=localhost;database=tutorials;user id=root;password=;"
End Class
'********************************
Public class Form1
'Declare Contant values
	Dim con As MySqlConnection = New MySqlConnection(dbconnect.constr)
    Dim query As String
    Dim da As MySqlDataAdapter
    Dim cmd As MySqlCommand
    Dim datardr As MySqlDataReader
'Source Codes for Inserting Data from textboxes to MySql Database tables

	Public Sub toInsertData()
        Dim cmd As MySqlCommand
        con.Open()
        Dim query As String = "insert into tutorials_tbl(tutorial_title,tutorial_author,submission_date)values
                                ('" & txttitle.Text & "','" & txtauthor.Text & "',now())"
        cmd = New MySqlCommand(query, con)
        Dim i As Integer = cmd.ExecuteNonQuery
        MsgBox("Successfully Added")
        con.Close()
	End Sub

'Source Codes for Updating Data

	Public Sub toUpdateData()
		Dim cmd As MySqlCommand
		con.Open()
		Dim query As String = "update tutorials_tbl set
								tutorial_title = '" & txttitle.Text & "',
								tutorial_author = '" & txtauthor.Text & "'
								where tutorial_id = '" & txtid.Text & "'"
		cmd = New MySqlCommand(query, con)
		Dim i As Integer = cmd.ExecuteNonQuery
		MsgBox("Successfully Updated!")
		con.Close()
	End Sub

'Source Codes for Displaying Data from Database Table to DataGridView

	Public Sub LoadGrid()
        Dim sql As MySqlCommand = New MySqlCommand("select * from tutorials_tbl", con)
        da = New MySqlDataAdapter
        Dim ds As DataSet = New DataSet
        da.SelectCommand = sql
        da.Fill(ds, "rec")
        DataGridView1.DataSource = ds
        DataGridView1.DataMember = "rec"
	End Sub

'Source Codes for Passing Values from DataGridView to TextBoxes and other controls.
	Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim i As Integer
        i = DataGridView1.CurrentRow.Index
        txtid.Text = DataGridView1.Item(0, i).Value
        txttitle.Text = DataGridView1.Item(1, i).Value
        txtauthor.Text = DataGridView1.Item(2, i).Value
    End Sub

'Source Codes for Naming the Column Header for the DataGridView
	Public Sub dataGridHeaderName()
        DataGridView1.Columns(0).HeaderText = "ID"
        DataGridView1.Columns(1).HeaderText = "Title"
        DataGridView1.Columns(2).HeaderText = "Author"
        DataGridView1.Columns(3).HeaderText = "Submission Date"
	End Sub

'Source Code for Displaying Items Selection for ComboBox from Column Values (from Database Table)

	Public Sub loadCbxAuthor()
        Try
            con.Open()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        query = "select tutorial_title from tutorials_tbl"
        cmd = new MySqlCommand(query, con)
        da = New MySqlDataAdapter(cmd)
        Dim dt As New DataTable("tutorial_title")
        da.Fill(dt)
        If dt.Rows.Count > 0 Then
            cbxtitle.DataSource = dt
            cbxtitle.DisplayMember = "tutorial_title"
        End If
        cbxtitle.Text = ""
        con.Close()
    End Sub

'Source Codes for Displaying Values from ComboBox to TextBoxes - with a specified “where” clause(s) from database

	Private Sub cbxtitle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxtitle.SelectedIndexChanged
        con = New MySqlConnection(dbconnect.constr)
        da = New MySqlDataAdapter
        cmd = New MySqlCommand
        con.Open()
        query = "select * from tutorials_tbl where tutorial_title = '" & cbxtitle.Text & "' "
        cmd.CommandText = query
        cmd.Connection = con
        da.SelectCommand = cmd
        datardr = cmd.ExecuteReader
        If datardr.HasRows Then
            datardr.Read()
            txtid.Text = datardr("tutorial_id")
            txttitle.Text = datardr("tutorial_title")
            txtauthor.Text = datardr("tutorial_author")
        End If
        con.Close()
    End Sub

'

End Class
'************************

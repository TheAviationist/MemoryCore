Imports System.Data.OleDb
Public Class Diary
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles DiaryTxt.TextChanged

    End Sub

    Private Sub BackBtn_Click(sender As Object, e As EventArgs) Handles BackBtn.Click
        NotesMenu.Show()
        Me.Close()
    End Sub
    Private Sub Diary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim diarycontents As String = ""
        GLOBALS.ConnToDb()
        GLOBALS.myqry = "SELECT Diary FROM Student WHERE StudentUsername = '" & Login.username & "'"
        GLOBALS.mycmd = New OleDbCommand(GLOBALS.myqry, GLOBALS.conn)
        GLOBALS.mydr = GLOBALS.mycmd.ExecuteReader
        ' If GLOBALS.mydr.HasRows = True Then
        While GLOBALS.mydr.Read()
            diarycontents = GLOBALS.mydr("Diary")
            DiaryTxt.Text = diarycontents
        End While
        'End If
    End Sub
    Private Sub SaveBtn_Click(sender As Object, e As EventArgs) Handles SaveBtn.Click
        Try
            With GLOBALS.conn
                If .State = ConnectionState.Open Then .Close()
                .ConnectionString = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\PrototypeDB1.accdb")
                .Open()
            End With
            GLOBALS.myqry = "UPDATE student SET Diary = '" & DiaryTxt.Text & "' WHERE studentusername = '" & Login.username & "'"
            MessageBox.Show("Diary Edited Successfully")
            With GLOBALS.mycmd
                .CommandText = GLOBALS.myqry
                .Connection = GLOBALS.conn
                .ExecuteNonQuery()
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
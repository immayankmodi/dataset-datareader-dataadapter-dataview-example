Imports System.Data.SqlClient

Public Class DataViewExample
    Inherits System.Web.UI.Page

    'specify your connection string here..
    Public Shared strConn As String = "Data Source=datasource;Integrated Security=true;Initial Catalog=yourDB"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindGridviewFileData()
        End If
    End Sub

    'bind subject details to gridview
    Private Sub BindGridviewFileData()
        Try
            Using sqlConn As New SqlConnection(strConn)
                Using sqlCmd As New SqlCommand()
                    sqlCmd.CommandText = "SELECT * FROM SubjectDetails"
                    sqlCmd.Connection = sqlConn
                    sqlConn.Open()

                    'SqlDataAdapter is used with SqlCommand as i described in my tutorial
                    Dim objDataAdapter As New SqlDataAdapter(sqlCmd)

                    'Declare DataSet object and fill data using Fill() method
                    Dim objDataTable As New DataTable()
                    objDataAdapter.Fill(objDataTable)

                    'Declare DataView object and creating vertual view for DataTable resultset
                    Dim objDataView As New DataView(objDataTable)
                    'We are now filter out the required subjects to display in gridview
                    'It'll filter "SubjectName" column and fetch only filtered subjects that are listed in condition
                    'You can use as many condition as you want, same as we use Where clause in SQL Server with SELECT statement
                    objDataView.RowFilter = "SubjectName = 'jQuery' OR SubjectName = 'JavaScript' OR SubjectName LIKE '%.net%'"

                    'Now if you want to "Sort" the resultset, use following statement with columnname and display order
                    objDataView.Sort = "SubjectName DESC"

                    'Assigning DataView object to DataSource and then bind gridview
                    gvSubjectDetails.DataSource = objDataView
                    gvSubjectDetails.DataBind()
                    sqlConn.Close()
                End Using
            End Using
        Catch
        End Try
    End Sub

End Class
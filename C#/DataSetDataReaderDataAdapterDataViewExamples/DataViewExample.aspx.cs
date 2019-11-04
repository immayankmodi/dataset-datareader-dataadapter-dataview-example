using System;
using System.Data;
using System.Data.SqlClient;

namespace DataSetDataReaderDataAdapterDataViewExamples {

    public partial class DataViewExample : System.Web.UI.Page {

        //specify your connection string here..
        public static string strConn = @"Data Source=datasource;Integrated Security=true;Initial Catalog=yourDB";

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack)
                BindGridviewFileData();
        }

        //bind subject details to gridview
        private void BindGridviewFileData() {
            try {
                using (SqlConnection sqlConn = new SqlConnection(strConn)) {
                    using (SqlCommand sqlCmd = new SqlCommand()) {
                        sqlCmd.CommandText = "SELECT * FROM SubjectDetails";
                        sqlCmd.Connection = sqlConn;
                        sqlConn.Open();

                        //SqlDataAdapter is used with SqlCommand as i described in my tutorial
                        SqlDataAdapter objDataAdapter = new SqlDataAdapter(sqlCmd);

                        //Declare DataSet object and fill data using Fill() method
                        DataTable objDataTable = new DataTable();
                        objDataAdapter.Fill(objDataTable);

                        //Declare DataView object and creating vertual view for DataTable resultset
                        DataView objDataView = new DataView(objDataTable);
                        //We are now filter out the required subjects to display in gridview
                        //It'll filter "SubjectName" column and fetch only filtered subjects that are listed in condition
                        //You can use as many condition as you want, same as we use Where clause in SQL Server with SELECT statement
                        objDataView.RowFilter = "SubjectName = 'jQuery' OR SubjectName = 'JavaScript' OR SubjectName LIKE '%.net%'";

                        //Now if you want to "Sort" the resultset, use following statement with columnname and display order
                        objDataView.Sort = "SubjectName DESC";

                        //Assigning DataView object to DataSource and then bind gridview
                        gvSubjectDetails.DataSource = objDataView;
                        gvSubjectDetails.DataBind();
                        sqlConn.Close();
                    }
                }
            } catch { }
        }
    }
}
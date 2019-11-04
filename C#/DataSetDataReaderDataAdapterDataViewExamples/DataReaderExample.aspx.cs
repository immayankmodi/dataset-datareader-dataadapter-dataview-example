using System;
using System.Data.SqlClient;

namespace DataSetDataReaderDataAdapterDataViewExamples {

    public partial class DataReaderExample : System.Web.UI.Page {

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

                        //Declare DataReader object and execute SqlCommand using ExecuteReader() method
                        SqlDataReader objDataReader = sqlCmd.ExecuteReader();

                        //Assign DataReader object to DataSource and bind it to gridview
                        gvSubjectDetails.DataSource = objDataReader;
                        gvSubjectDetails.DataBind();
                        sqlConn.Close();
                    }
                }
            } catch { }
        }
    }
}
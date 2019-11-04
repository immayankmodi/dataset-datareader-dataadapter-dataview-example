using System;
using System.Data;
using System.Data.SqlClient;

namespace DataSetDataReaderDataAdapterDataViewExamples {

    public partial class DataSetExample : System.Web.UI.Page {

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
                        DataSet objDataSet = new DataSet();
                        objDataAdapter.Fill(objDataSet);

                        //Assigning DataSet object to DataSource and then bind grid
                        gvSubjectDetails.DataSource = objDataSet;
                        gvSubjectDetails.DataBind();
                        sqlConn.Close();
                    }
                }
            } catch { }
        }
    }
}